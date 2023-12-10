#include "utils.h"
#include "pinger.h"

#if defined(_MSC_VER) && !defined(WIN32) 
#define WIN32_LEAN_AND_MEAN
#define NOMINMAX
#include <windows.h>
#include <timeapi.h>
#endif

#include <ws2tcpip.h>
#include <assert.h>
#include <exception>
#include <sstream>
#include <algorithm>
#include <cstdarg> // Include for std::snprintf
#include <memory>  // Include for std::unique_ptr


#if defined(_MSC_VER)
#define WIN32_LEAN_AND_MEAN
#define NOMINMAX
#pragma comment(lib,"Ws2_32.lib")
#include <winsock2.h>
#endif

bool ntp::utils::is_socket_lib_initialized() {
#ifdef WIN32
	SOCKET s = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (s == INVALID_SOCKET) {
		return false;
	}
	closesocket(s);
#endif//WIN32
	return true;
}

bool ntp::utils::socket_lib_init()
{
	if (is_socket_lib_initialized()) return true;
	WSADATA wsa{ 0 };
	return 0 == (WSAStartup(MAKEWORD(2, 2), &wsa));
}


void ntp::utils::switch_os_accurate_clock(bool switch_on)
{
#ifdef WIN32
#pragma comment(lib, "Winmm.lib")//to call timeGetDevCaps
	TIMECAPS cp = { 0 };
	if (MMSYSERR_NOERROR != timeGetDevCaps(&cp, sizeof(cp))) {
		assert(0);
		throw std::exception();
	}
	if (switch_on)
		timeBeginPeriod(cp.wPeriodMin);//setting the resolution for std::this_thread::sleep
	else
		timeEndPeriod(cp.wPeriodMin);//setting the resolution for std::this_thread::sleep
#endif//WIN32
}

bool ntp::utils::ping(std::string hostname, uint16_t nb_pings, ntp::utils::ping_result& out_res, std::chrono::milliseconds timeout, std::chrono::nanoseconds requests_intervals)
{
	ntp::init();
	Pinger ping;
	if (!ping.Init(hostname, 255, timeout))
		return false;

	auto res= ping.Start(nb_pings, requests_intervals);
	if (res.empty())
		return false;
	
	out_res.max_roundtrip = std::chrono::nanoseconds::min();
	out_res.min_roundtrip = std::chrono::nanoseconds::max();
	std::chrono::nanoseconds sum(0);
	std::vector<std::chrono::nanoseconds> sorted;
	for (auto& r : res) {
		if (!r.success) 
			continue;
		out_res.roundtrip.push_back(r.round_trip);
		sorted.push_back(r.round_trip);
		sum += r.round_trip;
		out_res.min_roundtrip = std::min(out_res.min_roundtrip, r.round_trip); out_res.max_roundtrip = std::max(out_res.max_roundtrip, r.round_trip);
	}
	
	if (out_res.roundtrip.empty()) 
		return false;

	std::sort(sorted.begin(), sorted.end());

	out_res.avg_roundtrip = sum / out_res.roundtrip.size();

	if (2 >= sorted.size())
		out_res.median_roundtrip = out_res.avg_roundtrip;
	else 
	{
		if (0 != (sorted.size() % 2))//odd amount, take the middle
			out_res.median_roundtrip = sorted[sorted.size()/2];
		else//even amount take the avarage to the two in the middle:
			out_res.median_roundtrip = (sorted[sorted.size()/2] + sorted[sorted.size()/2 - 1])/2;
	}

	out_res.sucess = true;

	

	return true;
}


static bool isSubnet(const sockaddr_in& address) {
	// Extract the address from sockaddr_in6
	const in_addr& addressIP = address.sin_addr;
	// Check if the 7th and 8th bytes are not zero
	return ((uint8_t*)&addressIP)[3] ==0;
}

static bool isIPv6Subnet(const std::string& ipv6Address) {
	std::vector<std::string> tokens;
	std::istringstream iss(ipv6Address);
	std::string token;

	while (std::getline(iss, token, ':'))
		tokens.push_back(token);

	// Check if the last token is empty, indicating double colons (::)
	if (tokens.back().empty()) {
		// If it is empty, it's likely a subnet
		return true;
	}
	else {
		// If the last token is not empty, it's likely a specific host
		return false;
	}
}

std::string ntp::utils::host_name_to_ip(std::string hostname, addr_type &out_addr_type, host_name_resolving_filter filter)
{
	enum class resolving_preference {
		ignore_subnets,
		prefer_hosts,
	}const preference = resolving_preference::prefer_hosts;


	ntp::init();	
	out_addr_type = addr_type::invalid;

	{
		sockaddr_in addr_v4{0};	addr_v4.sin_family = AF_INET;	addr_v4.sin_port = 1234;//dummy port
		if (1 == inet_pton(addr_v4.sin_family, hostname.c_str(), &addr_v4.sin_addr)) {
			//already ip v4.
			if (host_name_resolving_filter::ip_v6_only == filter)
				return std::string();

			out_addr_type = addr_type::ipv4;
			return hostname;
		}

		sockaddr_in6 addr_v6{0}; addr_v6.sin6_family = AF_INET6; addr_v6.sin6_port = 1234;//dummy port
		if (1 == inet_pton(addr_v6.sin6_family, hostname.c_str(), &addr_v6.sin6_addr)) {
			//already ip v6.
			if (host_name_resolving_filter::ip_v4_only == filter)
				return std::string();

			out_addr_type = addr_type::ipv6;
			return hostname;
		}
	}

	addrinfo hints;
	memset(&hints, 0, sizeof(hints));
	switch (filter)	
	{
	case ntp::utils::host_name_resolving_filter::prefer_ip_v6:
	case ntp::utils::host_name_resolving_filter::prefer_ip_v4:
	case ntp::utils::host_name_resolving_filter::any:
		hints.ai_family = AF_UNSPEC;
		break;
	case ntp::utils::host_name_resolving_filter::ip_v4_only:
		hints.ai_family = AF_INET;// AF_INET6
		break;
	case ntp::utils::host_name_resolving_filter::ip_v6_only:
		hints.ai_family = AF_INET6;
		break;
	default:
		throw std::exception();
		break;
	}

	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_UDP;
	addrinfo *result = nullptr;
	if (0 != getaddrinfo(hostname.c_str(), NULL, &hints, &result))
		return std::string();

	std::string first_found_ip_v6, first_found_ip_v4, first_found_ip_v6_subnet, first_found_ip_v4_subnet;

	// Retrieve each address and take the 1st one.
	for (auto ptr = result; ptr != NULL; ptr = ptr->ai_next)
	{
		switch (ptr->ai_family) 
		{
		case AF_INET:
		{
			if (!first_found_ip_v4.empty()) continue;

			sockaddr_in  *sockaddr_ipv4 = (sockaddr_in *) ptr->ai_addr;
			//Determine whether the address is a subnet address or a host address
			const bool is_subnet = isSubnet(*sockaddr_ipv4);
			if (is_subnet) {
				if(resolving_preference::ignore_subnets == preference) 	continue;
				if (!first_found_ip_v4_subnet.empty()) continue;
			}
			
			char ip_str[256] = { '\0' };
			inet_ntop(ptr->ai_addr->sa_family, &sockaddr_ipv4->sin_addr, ip_str, sizeof(ip_str));

			if (is_subnet)
				first_found_ip_v4_subnet = ip_str;
			else
				first_found_ip_v4 = ip_str;
			break;
		}
		case AF_INET6:

			if (!first_found_ip_v6.empty()) continue;

			sockaddr_in6  *sockaddr_ipv6 = (struct sockaddr_in6 *) ptr->ai_addr;
			char ip_str[256] = { '\0' };
			inet_ntop(ptr->ai_addr->sa_family, &sockaddr_ipv6->sin6_addr, ip_str, sizeof(ip_str));
			//Determine whether the address is a subnet address or a host address

			const bool is_subnet = isIPv6Subnet(ip_str);
			if (is_subnet) {
				if (resolving_preference::ignore_subnets == preference)	continue;
				if (!first_found_ip_v6_subnet.empty()) continue;
			}

			if (is_subnet)
				first_found_ip_v6_subnet = ip_str;
			else
				first_found_ip_v6 = ip_str;
			break;
		}//end of switch				

		if (!first_found_ip_v4.empty() && !first_found_ip_v6.empty()) break;//found one of each, break the loop

	}//end of for loop
	freeaddrinfo(result);

	if (first_found_ip_v4.empty())
		first_found_ip_v4 = first_found_ip_v4_subnet;

	if (first_found_ip_v6.empty())
		first_found_ip_v6 = first_found_ip_v6_subnet;

	switch (filter)
	{
		case ntp::utils::host_name_resolving_filter::ip_v4_only: {
			out_addr_type = addr_type::ipv4;
			return first_found_ip_v4;
		}
		case ntp::utils::host_name_resolving_filter::ip_v6_only: {
			out_addr_type = addr_type::ipv6;
			return first_found_ip_v6;
		}
		case ntp::utils::host_name_resolving_filter::prefer_ip_v4:
			if (!first_found_ip_v4.empty()) {
				out_addr_type = addr_type::ipv4;
				return first_found_ip_v4;
			}
			break;
		case ntp::utils::host_name_resolving_filter::prefer_ip_v6:
			if (!first_found_ip_v6.empty()) {
				out_addr_type = addr_type::ipv6;
				return first_found_ip_v6;
			}
			break;
	}
	//so far the prefered/requested  where not returned
	
	if (!first_found_ip_v4.empty()) {
		out_addr_type = addr_type::ipv4;
		return first_found_ip_v4;
	}

	if (!first_found_ip_v6.empty()) {
		out_addr_type = addr_type::ipv6;
		return first_found_ip_v6;
	}

	return std::string();
}

std::string ntp::utils::format_string(const char* format, ...) {
	// Determine the required buffer size
	va_list args;
	va_start(args, format);
	int size = std::vsnprintf(nullptr, 0, format, args);
	va_end(args);

	// Allocate memory for the formatted string
	std::unique_ptr<char[]> buffer(new char[size + 1]);

	// Format the string using std::snprintf
	va_start(args, format);
	std::vsnprintf(buffer.get(), size + 1, format, args);
	va_end(args);

	// Convert the C-style string to a std::string
	return std::string(buffer.get());
}

ntp::utils::SetSystemClockResult ntp::utils::set_system_clock(const ntp::time_point_t & tm)
{
#ifdef WIN32
	SYSTEMTIME st;//on Windows OS system time is stored as loacal time.
	int y, m, d;
	std::chrono::hours h; std::chrono::minutes mm; std::chrono::seconds sec;
	std::chrono::milliseconds ms; std::chrono::microseconds us; std::chrono::nanoseconds ns;
	extract_time_point(true,tm, y, m, d, h, mm, sec, ms, us, ns);

	st.wYear = (WORD)y;
	st.wMonth = (WORD)m;
	st.wDay = (WORD)d;
	st.wHour = (WORD)h.count();
	st.wMinute = (WORD)mm.count();
	st.wSecond = (WORD)sec.count();
	st.wMilliseconds = (WORD)ms.count();

	auto ret = SetLocalTime(&st);
	if (ret) return SetSystemClockResult::Success;

	auto err = GetLastError();
	if (err == 1314)
		return SetSystemClockResult::Failed_AdminRightsNeeded;
	else
		return SetSystemClockResult::Failed;
#else //linux:
todo: code should be compleated
#include <chrono>
#include <ctime>
#include <iostream>
#include <sys/time.h>
	auto now = std::chrono::system_clock::now();
	auto now_c = std::chrono::system_clock::to_time_t(now);
	struct timeval tv;
	tv.tv_sec = now_c;
	tv.tv_usec = 0;

	return (-1 == settimeofday(&tv, nullptr)) ? SetSystemClockResult::Failed : SetSystemClockResult::Success;

#endif				
}

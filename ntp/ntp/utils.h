#ifndef __ntp_utils_h__
#define __ntp_utils_h__

#include <chrono>
#include <vector>
#include <string>
#include <stdint.h>
#include "ntp.h"

namespace ntp {
namespace utils {

	//Turns on/off accurate time mechanizem for the process (needs to be turned off after).
	void switch_os_accurate_clock(bool switch_on);

	//returns true if the sockets library is initialized
	bool is_socket_lib_initialized();
	//initializes the sockets library
	bool socket_lib_init();

	struct ping_result {
		std::vector<std::chrono::nanoseconds> roundtrip;
		std::chrono::nanoseconds median_roundtrip;
		std::chrono::nanoseconds max_roundtrip;
		std::chrono::nanoseconds min_roundtrip;
		std::chrono::nanoseconds avg_roundtrip;
		bool sucess = false;
	};

	//attempts ro perform ping on the give host/url
	bool ping(std::string hostname, uint16_t nb_pings, ping_result& out_res, std::chrono::milliseconds timeout = std::chrono::seconds(3), std::chrono::nanoseconds requests_intervals = std::chrono::nanoseconds(0));

	enum class addr_type {invalid,ipv4,ipv6 };

	enum class host_name_resolving_filter {
		any,
		prefer_ip_v4,
		prefer_ip_v6,
		ip_v4_only,
		ip_v6_only,
	};
	//Attempts to resolve the given host name, however if the string is an explicit valid IP, it will be returned as is.
	//On success returns string representation of the ip, and the type of the ip, oherwise returns empty string.
	//filter: controls the type of IP address that will be returned.
	//  -note, if [hostname] is an explicit valid IP formatted string, the "host_name_resolving_filter::prefer_ip_" values
	//   will not have any effect.
	std::string host_name_to_ip(std::string hostname, addr_type &out_addr_type, host_name_resolving_filter filter);

	//On success returns string representation of the ip, and the type of the ip, oherwise returns empty string.
	static inline std::string host_name_to_ip(std::string hostname, addr_type &out_addr_type) {
		return host_name_to_ip(hostname, out_addr_type, host_name_resolving_filter::any);
	}

	//customized version of sprintf that dynamicly alocates the required buffers.
	std::string format_string(const char* format, ...);

	//sets the system clock
	enum class SetSystemClockResult {Success, Failed, Failed_AdminRightsNeeded};
	SetSystemClockResult set_system_clock(const ntp::time_point_t& tm);
	static inline SetSystemClockResult set_system_clock(const ntp::DateTime& t) {return set_system_clock(t.time());}
	static inline SetSystemClockResult set_system_clock(const ntp::ManagedClockPtr mc) {
		return (!mc || !mc->has_time()) ? SetSystemClockResult::Failed : set_system_clock(mc->now());
	}
	//returns the accumilated steady_clock drift given a duration mesured with [std::chrono::steady_clock] and a drift rate.
	//Note that the returned drift and be nagative
	static inline std::chrono::nanoseconds steady_clock_accumulated_drift(double drift_rate, const std::chrono::nanoseconds& steady_clock_duration) {
		return std::chrono::nanoseconds(static_cast<int64_t>(steady_clock_duration.count()*drift_rate));
	}

	//returns the accumilated steady_clock drift given a duration mesured with [std::chrono::steady_clock] and a drift rate.
	//Note that the returned drift and be nagative
	static inline nanoseconds_fp steady_clock_accumulated_drift(double drift_rate, const nanoseconds_fp& steady_clock_duration) {
		return nanoseconds_fp(steady_clock_duration.count()*drift_rate);
	}

	//returns the accumilated steady_clock drift of (std::chrono::steady_clock::now() - ref_time) and a drift rate.
	//Note that the returned drift and be nagative
	static inline std::chrono::nanoseconds steady_clock_accumulated_drift(double drift_rate, const std::chrono::steady_clock::time_point& ref_time) {
		return steady_clock_accumulated_drift(drift_rate, std::chrono::steady_clock::now() - ref_time);
	}
}}

#endif// __ntp_utils_h__
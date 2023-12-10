#include "ntp.h"
#include "utils.h"
#include <chrono>
#include <sstream>
#include <mutex>

#define USE_CHRONO_POSIX

#ifndef USE_CHRONO_POSIX
	#if defined(_MSC_VER) && !defined(WIN32) 
		#define WIN32_LEAN_AND_MEAN
		#define NOMINMAX
		#include <windows.h>
	#endif
#endif//USE_CHRONO_POSIX

void ntp::init()
{
	static std::once_flag _init_once;
	std::call_once(_init_once,[](){ntp::utils::socket_lib_init();}	);
}

int64_t ntp::get_posix_time_microseconds()
{
#ifdef USE_CHRONO_POSIX
	//posix_us
	return std::chrono::time_point_cast<std::chrono::microseconds>(std::chrono::system_clock::now()).time_since_epoch().count();
#else

#if defined(WIN32) || defined(_MSC_VER)
	FILETIME ft; int64_t t;
	GetSystemTimeAsFileTime(&ft);
	t = (int64_t)ft.dwHighDateTime << 32 | ft.dwLowDateTime;
	return t / 10 - 11644473600000000; /* Jan 1, 1601 */
#else 
	struct timeval tv;
	gettimeofday(&tv, NULL);
	return (int64_t)tv.tv_sec * 1000000 + tv.tv_usec;
#endif//#if defined(WIN32) || defined(_MSC_VER)

#endif//USE_CHRONO_POSIX
}

std::string ntp::parse_reference_identifier(const ntp::NtpPacket::ReferenceIdentifier& refid, ntp::StratumLevel stratum, ntp::ProtocolVersion ver, ntp::AssociationModes assoc_mode)
{
	// In the case of NTP Version 3 & 4, for stratum-0 (unspecified)
	// or stratum-1 (primary) servers, this is a four-character ASCII
	// string, left justified and zero padded to 32 bits.
	if (StratumLevel::Unspecified == stratum || 
		StratumLevel::PrimaryReference == stratum ||
		ntp::AssociationModes::ControlMessage == assoc_mode
		) 
	{
		//For stratum-0 this is a KISS code for debugging and monitoring (in that case the ntp::AssociationModes should be a ControlMessage
		//For stratum-1 this is a identifier of the clock.
		char refid_str[sizeof(refid.ascii) + 1] = {'\0'};//+ null-terminating
		memcpy(refid_str, refid.ascii, sizeof(refid));
		return std::string(refid_str);
	}

	//TODO: add the option to parse error codes of the refid, when provided by the server.

	switch (ver)
	{
	case ntp::ProtocolVersion::V3:
	case ntp::ProtocolVersion::V4:
	{
		const char zeros[sizeof(refid)] = {0};
		if(0 == memcmp(refid.bytes, zeros, sizeof(refid)))
			return std::string();//not an IP.

		//for ntp v3 & v4 should by IPv4 address
		//In NTP Version 3 & 4 secondary servers, this is the 32-bit IPv4 address of the reference source.
		std::ostringstream oss; oss << static_cast<short>(refid.bytes[0]) << '.' << static_cast<short>(refid.bytes[1]) << '.' << static_cast<short>(refid.bytes[2]) << '.' << static_cast<short>(refid.bytes[3]);
		return oss.str();
	}	
	default:
		return std::string();
	}
}




/*** TIME CLASS ****/

ntp::DateTime::DateTime() :
	m_time(ntp::time_point_t::min()) {}

ntp::DateTime::DateTime(const ntp::time_point_t &time) : m_time(time) {}

ntp::DateTimePt ntp::DateTime::as_simple_time_local() const
{
	DateTimePt out;
	if (empty()) {
		memset(&out, 0, sizeof(out));
		out.tm_kind = DateTimePt::Kind::Invalid;
		return out;
	}

	time_pt_class_to_struct(true, m_time, out);
	return out;
}

ntp::DateTimePt ntp::DateTime::as_simple_time_utc() const
{
	DateTimePt out;
	if (empty()) {
		memset(&out, 0, sizeof(out));
		out.tm_kind = DateTimePt::Kind::Invalid;
		return out;
	}

	time_pt_class_to_struct(false, m_time, out);
	return out;
}

int64_t ntp::DateTime::as_posix_time_microsec() const {
	return empty() ? -1 : std::chrono::duration_cast<std::chrono::microseconds>(m_time.time_since_epoch()).count();
}

int64_t ntp::DateTime::as_posix_time_nanosec() const {
	return empty() ? -1 : std::chrono::nanoseconds(m_time.time_since_epoch()).count();
}

void ntp::DateTime::as_ntp_time(uint32_t & seconds, uint32_t & fractions) const {
	ntp_time_from_posix(seconds, fractions, std::chrono::nanoseconds(m_time.time_since_epoch()));
}

std::string ntp::DateTimePt::as_string() const
{
	if (DateTimePt::Kind::Invalid == tm_kind) return "N/A";
	const char* kind_str = "";
	switch (tm_kind) {
	case DateTimePt::Kind::UTC: kind_str = " utc"; break;
	case DateTimePt::Kind::Local: kind_str = " loc"; break;
	default: kind_str = ""; 
		assert(0);
		break;
	}

	// Convert time components to std::chrono::nanoseconds
	const std::chrono::nanoseconds time_ns = msec + usec + nsec;
	const ntp::seconds_fp sec_fractions(time_ns);// Convert to fractional seconds (double)
	// Convert to string and extract fractional part
	std::string fractionalString = (sec_fractions.count() == 0) ? "" : std::to_string(sec_fractions.count()).substr(1);
	const size_t MAX_AFTER_DECIMALS_POINTS = 3;
	fractionalString = fractionalString.substr(0, MAX_AFTER_DECIMALS_POINTS+1);
	return ntp::utils::format_string("%u/%02u/%02u %02u:%02u:%02u%s%s", mday, mon, year, (int)hour.count(), (int)min.count(), (int)sec.count(), fractionalString.c_str(), kind_str);
}

std::string ntp::DateTimePt::as_date_string() const 
{
	if (DateTimePt::Kind::Invalid == tm_kind) return "N/A";
	const char* kind_str = "";
	switch (tm_kind) {
	case DateTimePt::Kind::UTC: kind_str = " utc"; break;
	case DateTimePt::Kind::Local: kind_str = " loc"; break;
	default: kind_str = "";
		assert(0);
		break;
	}

	return ntp::utils::format_string("%u/%02u/%02u", mday, mon, year, kind_str);
}

std::string ntp::DateTimePt::as_time_of_day_string() const {
	if (DateTimePt::Kind::Invalid == tm_kind) return "N/A";
	const char* kind_str = "";
	switch (tm_kind) {
	case DateTimePt::Kind::UTC: kind_str = " utc"; break;
	case DateTimePt::Kind::Local: kind_str = " loc"; break;
	default: kind_str = "";
		assert(0);
		break;
	}

	// Convert time components to std::chrono::nanoseconds
	const std::chrono::nanoseconds time_ns = msec + usec + nsec;
	const ntp::seconds_fp sec_fractions(time_ns);// Convert to fractional seconds (double)
	// Convert to string and extract fractional part
	std::string fractionalString = (sec_fractions.count() == 0) ? "" : std::to_string(sec_fractions.count()).substr(1);
	const size_t MAX_AFTER_DECIMALS_POINTS = 3;
	fractionalString = fractionalString.substr(0, MAX_AFTER_DECIMALS_POINTS + 1);
	return ntp::utils::format_string("%02u:%02u:%02u%s%s", (int)hour.count(), (int)min.count(), (int)sec.count(), fractionalString.c_str(), kind_str);
}

std::string ntp::DateTime::as_local_time_string() const
{
	if (empty()) return "N/A";
	DateTimePt tp;
	time_pt_class_to_struct(true, m_time, tp);
	return tp.as_string();	
}

std::string ntp::DateTime::as_utc_time_string() const
{
	if (empty()) return "N/A";
	DateTimePt tp;
	time_pt_class_to_struct(false, m_time, tp);
	return tp.as_string();
}

std::string ntp::DateTime::as_local_time_of_day_string() const {
	if (empty()) return "N/A";
	DateTimePt tp;
	time_pt_class_to_struct(true, m_time, tp);
	return tp.as_time_of_day_string();
}

std::string ntp::DateTime::as_utc_time_of_day_string() const {
	if (empty()) return "N/A";
	DateTimePt tp;
	time_pt_class_to_struct(false, m_time, tp);
	return tp.as_time_of_day_string();
}

std::string ntp::DateTime::as_local_date_string() const {
	if (empty()) return "N/A";
	DateTimePt tp;
	time_pt_class_to_struct(true, m_time, tp);
	return tp.as_date_string();
}

std::string ntp::DateTime::as_utc_date_string() const {
	if (empty()) return "N/A";
	DateTimePt tp;
	time_pt_class_to_struct(false, m_time, tp);
	return tp.as_date_string();
}

//////////////////

/*** MANAGED TIME CLASS ****/
ntp::ManagedClock::ManagedClock() :
	m_extraction_time(std::chrono::steady_clock::time_point::min())
	, m_time(ntp::time_point_t::min()), m_drift_rate(0)
{}

ntp::ManagedClock::ManagedClock(const ntp::time_point_t &time, const std::chrono::steady_clock::time_point& extraction_time, double drift_rate) :
	m_extraction_time(extraction_time)
	, m_time(time),m_drift_rate(drift_rate)
{}

std::chrono::nanoseconds ntp::ManagedClock::duration_from_reference_time() const {
	const std::chrono::nanoseconds dur_from_ref(std::chrono::steady_clock::now() - m_extraction_time);
	return dur_from_ref + ntp::utils::steady_clock_accumulated_drift(m_drift_rate, dur_from_ref);
}

ntp::ManagedClockPtr ntp::ManagedClock::add_Offset(const std::chrono::nanoseconds& offset) const
{
	return !has_time() ?
		ManagedClockPtr(new ManagedClock()):
	    ManagedClockPtr(new ManagedClock(m_time + offset, m_extraction_time, m_drift_rate));
}

//////////////////

ntp::ServerInfo::ServerInfo() :
	stratum({ StratumLevel::Unspecified })
	, version(ProtocolVersion::V3)
	, root_dispersion(std::chrono::nanoseconds(0))
	, root_delay(std::chrono::nanoseconds(0))
	, min_query_intervals(std::chrono::seconds(0))
{}

std::string ntp::ServerInfo::as_string() const
{
	std::ostringstream oss; oss << server_ep->as_string();
	if (!ref_id.empty()) oss << '(' << ref_id << ')';
	oss << "|ver " << static_cast<short>(version) << "|stratum " << stratum.as_string()
		<< "|root-dispersion " << ntp::seconds_fp(root_dispersion).count()
		<< "|root-delay " << ntp::seconds_fp(root_delay).count()
		<< "|min-polls " << min_query_intervals.count()
		<< "|ref-time " << server_reference_time.as_local_time_string();

	return oss.str();

}

std::string ntp::ServerInfo::Stratum::as_string() const {
	switch (value)
	{
		case StratumLevel::Unspecified : return "unspecified";
		case StratumLevel::PrimaryReference : return "primary reference";
		case StratumLevel::Unsynchronized : return "unsynchronized";
		default:
			if (StratumLevel::Reserved > value)
					return (std::string("secondaryReference(") + std::to_string(static_cast<uint16_t>(value)) + ")");

		return "reserved";
	}
}

//////////////////
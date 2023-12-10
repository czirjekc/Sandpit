#ifndef __ntp_server_h__
#define __ntp_server_h__

#include <string>
#include <thread>
#include <shared_mutex>
#include <mutex>
#include <memory>
#include <queue>
#include "ntp.h"
#include "include/intp_server.h"
#include "semaphore.h"
#include "manual_event.h"

namespace ntp
{
	struct client_req;
	class Server : public ntp::IServer
	{
	public:

		Server();
		virtual ~Server();

		//Init the server
		virtual bool init(const ServerSettings& settings, const EndPointUDP& listen_ep) override;

		//returns the clock used by the server;
		virtual ManagedClockPtr inner_clock() const override;

	private:
		void cleanup_listening_socket();
		void clients_response_work();
		void clients_request_listening_work();
		void clients_responded_post_process_work();
		void clock_update_work();
		NtpPacket::ReferenceIdentifier calc_server_refid(ntp::ProtocolVersion ntp_ver, ntp::StratumLevel stratum, ntp::ServerInfoPtr reference_server) const;

		inline void set_clock(ManagedClockPtr new_clock, ServerInfoPtr server = nullptr) {
			{
				std::unique_lock<std::shared_mutex> lock(m_clock_lock);
				m_clock = new_clock;
				m_clock_src_info = server;
			}
		}
		inline std::pair<ManagedClockPtr,ServerInfoPtr> clock() const{
			ManagedClockPtr clock;
			ServerInfoPtr sip;
			{
				std::shared_lock<std::shared_mutex> lock(m_clock_lock);
				clock = m_clock; sip = m_clock_src_info;
			}
			return std::make_pair(clock, sip);
		}

		Server(const Server&);				//disabled
		Server& operator = (const Server&);	//disabled

		ServerSettings m_settings;

		void* m_p_si_addr;
		size_t m_socket;
		int m_p_si_addr_size;
		int m_address_family;
		std::string m_lastErrorMsg;

		manual_event m_disposing_evt;

		std::thread m_clients_response_thread;
		std::thread m_clients_request_listening_thread;
		std::thread m_clients_post_process_thread;
		std::thread m_clock_update_thread;

		semaphore m_requestsQueueSem;
		std::mutex m_requestsQueueLock;
				
		std::queue<std::shared_ptr<struct client_req>> m_requestsQueue;

		semaphore m_clientsRespondedPostProcessQueueSem;
		std::mutex m_clientsRespondedPostProcessQueueLock;
		std::queue<std::shared_ptr<struct client_req>> m_clientsRespondedPostProcessQueue;

		mutable std::shared_mutex m_clock_lock;

		//Source of time that the server keeps publishing;
		ntp::ManagedClockPtr m_clock;
		//Info on the source of the clock (can be null)
		ntp::ServerInfoPtr   m_clock_src_info;
	};
}

#endif//__ntp_server_h__

#ifndef __NTP_TESTS_H__
#define	__NTP_TESTS_H__
#include <list>
#include "../../ntp/include/intp.h"
#include "../../ntp/include/intp_manager.h"


std::list<ntp::EndPointPtr> compile_ntp_endpoints(bool include_local_ntp);

//Initializes and run, ntp client that will connect and repetitively query specific ntp server.
void execute_ntp_client_query_loop(ntp::EndPointPtr ep);

//Initializes client manager to manage set of ntp clients, select the best one, and providing clock info,
//with ability to reevaluate servers.
ntp::ClientsListManagerPtr start_init_clients_manager(bool include_local_ntp, bool prefer_speed_over_quality_evaluation);

void test_clients();

void test_clients_manager( bool prefer_speed_over_quality_evaluation);

//Initialize ntp server that will listen to requests from clients and will serve them with time info.
//Depends on the given argument it will construct ntp::IManager and will use it's clock as the time source.
bool test_server(bool prefer_speed_over_quality_evaluation, bool use_external_time_source, bool listen_on_ipv6);

//executes groups ntp clients connecting to different servers and waiting for their clock's fine tuning.
//once finetuned, the group's clocks are compared.
void test_servers_diff();

#endif // __NTP_TESTS_H__


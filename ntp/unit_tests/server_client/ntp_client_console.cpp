
#include <iostream>
#include <thread>
#include <list>

#include "tests.h"

int main(int argc, char** argv)  
{
	//test_servers_diff();
	//test_server(false, true, true);//test server on ipv6
	test_server(true, true, false);//test server on ipv4
	//test_clients();
	//test_clients_manager(false);


	return 0;
}


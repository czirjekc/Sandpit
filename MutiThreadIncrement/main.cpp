#include <string>
#include <iostream>
#include <thread>
#include <vector>
#include <mutex>

void increment(std::mutex& m) {
    static int val = 0;
    m.lock();
    val++;
    std::cout << val << "\n";
    m.unlock();
}

int main() {
    std::vector<std::thread> v;
    std::mutex m;
    for (int i = 0; i < 20; i++) {

        v.push_back(std::move(std::thread(increment, ref(m))));
        
    }

    for (auto& t : v) {
        t.join();
    }
}

#include "PCH.h"


std::atomic<int> a{ 0 };
std::atomic<int> b{ 0 };

int valueset() {
    int t = 1;
    a.store(t, std::memory_order_relaxed);
    b.store(2, std::memory_order_release);    // 本原子操作前所有的写原子操作必须完成
    return 0;
}

int observer() {
    while (b.load(std::memory_order_acquire) != 2) // 本原子操作必须完成才能执行之后所有的
        ;
    std::cout << a.load(std::memory_order_relaxed) << std::endl; // 必然会打印 1
    return 0;
}

TEST(Learn_MemoryOrder, Case1)
{
    std::thread t1(valueset);
    std::thread t2(observer);

    t1.join();
    t2.join();
}


int data = 42;
std::atomic_bool data_ready(false);
int disorder = 0;

void writer_thread()  // 线程1
{
    data = 10;        //#1
    data_ready.store(true, std::memory_order_relaxed);   //#2
}

void reader_thread()  // 线程2
{
    // while (!data_ready.load(std::memory_order_relaxed)) {}      // #3：对data_ready的读操作
    if (data != 10)  //#4
    {
        disorder++;
    }
}

TEST(Learn_MemoryOrder, Case2)
{
    for (int i = 0; i < 1000; ++i)
    {
        std::thread t1(writer_thread);
        std::thread t2(reader_thread);

        t1.join();
        t2.join();
        std::cout << disorder << std::endl;
    }
}

#include <PCH.h>

// 声明汇编函数
extern "C" void AddAndPrint(int A, int B);
extern "C" void Test_Mov(int A);
extern "C" void Test_POPCNT(int A);


TEST(Test_Assembly, ACall)
{
    GTEST_SKIP();

    int A = 5;
    int B = 10;
    AddAndPrint(A, B);  // 调用汇编函数
}
TEST(Test_Assembly, Case_2)
{
    Test_Mov(100);
    __debugbreak();
}
TEST(Test_Assembly, POPCNT)
{
    Test_POPCNT(100);
}


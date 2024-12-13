#include <PCH.h>

// 声明汇编函数
extern "C" void AddAndPrint(int A, int B);


TEST(Test_Assembly, ACall)
{
    GTEST_SKIP();

    int A = 5;
    int B = 10;
    AddAndPrint(A, B);  // 调用汇编函数
}
#include "PCH.h"


class TestClass
{
public:
    int IntVal;
    void * PtrVal;
    std::string StrVal;
};

__declspec(noinline)
static void Test_Case()
{
    TestClass t {};
    __debugbreak();
}

struct TestStruct
{
    std::string Test;
    int Val1;
    int Val2;
};

TEST(Learn_ClassConstruct, Case1)
{
    TestStruct t2 = {
        .Val1 = 100,
    };

    __debugbreak();

    // Test_Case();
    ///std::cout << "TestStruct1 t: InVal=" << t.IntVal << ", PtrVal=" << t.PtrVal;
}
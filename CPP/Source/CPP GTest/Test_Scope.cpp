#include "PCH.h"

class Scope
{
public:
    Scope()
    {
        std::cout << "Scope()" << std::endl;
    }
    ~Scope()
    {
        std::cout << "~Scope()" << std::endl;
    }
};

TEST(Test_Scope, DirectConstruct_WithoutName)
{
    GTEST_SKIP();

    Scope();
    std::cout << "Do Things" << std::endl;
}

TEST(Test_Scope, DirectConstruct_WithName)
{
    GTEST_SKIP();

    Scope _();
    std::cout << "Do Things" << std::endl;
}

TEST(Test_Scope, ConstrcutByClassInit_WithoutName)
{
    GTEST_SKIP();

    Scope{};
    std::cout << "Do Things" << std::endl;
}

TEST(Test_Scope, ConstrcutByClassInit_WithName)
{
    GTEST_SKIP();

    Scope _{};
    std::cout << "Do Things" << std::endl;
}


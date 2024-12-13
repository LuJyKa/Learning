#include "PCH.h"


class Environment : public ::testing::Environment
{
public:
    virtual ~Environment() = default;

    // Override this to define how to set up the environment.
    virtual void SetUp()
    {

    }

    // Override this to define how to tear down the environment.
    virtual void TearDown()
    {


    }
};

int main(int argc, char * argv[])
{
    // --- Set Encoding
    SetConsoleOutputCP(CP_UTF8);
    /// SetConsoleCP(CP_UTF8);

    //std::setlocale(LC_ALL, ".UTF8");

    /// std::setlocale(LC_ALL, ".936");
    /// std::setlocale(LC_ALL, "Chinese-simplified");//设置中文环境
    ///SetConsoleOutputCP(936);

    //std::setlocale(LC_ALL, ".936");

    // --- GTest
    ::testing::InitGoogleTest(&argc, argv);
    ::testing::AddGlobalTestEnvironment(new Environment());
    return RUN_ALL_TESTS();
}
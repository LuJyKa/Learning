#include "PCH.h"

struct Base
{
    Base()
    {
        std::cout << "[Base] Construct" << std::endl;
        CallPureVirtualFunc();
    }

    virtual void Foo() = 0;

    void CallPureVirtualFunc()
    {
        std::cout << "[CallPureVirtualFunc] Call" << std::endl;
        Foo();
    }
};

struct Derive : public Base
{
    Derive()
    {
        std::cout << "[Derive] Construct" << std::endl;
    }
    void Foo()
    {
        /// std::cout << "Call Here!" << std::endl;
    }
};

void myPurecallHandler(void)
{
    printf("In _purecall_handler.");
}

TEST(��O����, ���z�֐���O)
{
    _set_purecall_handler(myPurecallHandler);

    try
    {
        Base * b = new Derive();
        // Just to silence the warning C4101:
        //    'VirtualFunctionCallCrash::B::Foo' : unreferenced local variable
        b->Foo();
    }
    catch (...)
    {
        std::cout << "Try Catch!" << std::endl;
    }
}


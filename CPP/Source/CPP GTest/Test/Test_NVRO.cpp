#include <iostream>
#include <gtest/gtest.h>


struct Noisy
{
    Noisy() { std::cout << "[Noisy] Constructed at " << this << '\n'; }
    Noisy(const Noisy &) { std::cout << "[Noisy] Copy-Constructed\n"; }
    Noisy(Noisy && pRHS) noexcept { std::cout << "[Noisy] Move-Constructed at " << this << ", RHS=" << &pRHS << std::endl; }
    ~Noisy() { std::cout << "[Noisy] Destructed at " << this << '\n'; }
};

struct Wrap
{
    Wrap() { std::cout << "[Wrap] Constructed at " << this << '\n'; }
    Wrap(Noisy && pNoisy)
    : _Ref(std::move(pNoisy))
    {
        std::cout << "[Wrap] Constructed at " << this << '\n';
    }
    Noisy _Ref;
    Wrap(const Wrap & pRHS) : _Ref(pRHS._Ref) { std::cout << "[Wrap] Copy-Constructed\n "; }
    Wrap(Wrap && pRHS) noexcept : _Ref(pRHS._Ref) { std::cout << "[Wrap] Move-Constructed at " << this << ", RHS=" << &pRHS << std::endl; }
    ~Wrap() { std::cout << "[Wrap] Destructed at " << this << '\n'; }
};

Noisy f()
{
    Noisy v = Noisy(); // (until C++17) copy elision initializing v from a temporary;
    //               the move constructor may be called
    // (since C++17) "guaranteed copy elision"
    return v; // copy elision ("NRVO") from v to the result object;
    // the move constructor may be called
}

Wrap TestWrap()
{
    return Noisy();
}

void g(Noisy arg)
{
    std::cout << "&arg = " << &arg << '\n';
}

TEST(Test_NVRO, Case1)
{
    Wrap t = TestWrap();
    std::cout << "-------" << std::endl;

    auto v = f(); // (until C++17) copy elision initializing v from the result of f()
    // (since C++17) "guaranteed copy elision"

    std::cout << "&v = " << &v << '\n';

    g(f()); // (until C++17) copy elision initializing arg from the result of f()
    // (since C++17) "guaranteed copy elision"


}
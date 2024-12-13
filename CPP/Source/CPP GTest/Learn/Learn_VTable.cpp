#include <PCH.h>

// g++ virtual_funcs.cpp -o virtual_funcs
// ./virtual_funcs
using namespace std;


namespace Learn_VTable
{

    typedef void(*Func)(void); //这是在声明一个void(*)() 类型的函数指针Func


    class Base {
    public:
        virtual void f(void) { cout << "Base::f" << endl; }
        virtual void g(void) { cout << "Base::g" << endl; }
        virtual void h(void) { cout << "Base::h" << endl; }
    };

    // 注意这里的继承是有覆盖的（将f覆盖）
    class Derive1 : public Base {
    public:
        virtual void f(void) { cout << "in Derive1::f, this = " << this << endl; }
        virtual void g1(void) { cout << "Derive::g1" << endl; }
        virtual void h1(void) { cout << "Derive::h1" << endl; }
    };


}

using namespace Learn_VTable;

TEST(VTable, Main)
{
    Base * b = new Derive1();
    // Derive1 d1;
    Func pFunc = NULL;
    cout << "Derive1 虚函数表地址：" << (uint64_t *)(b) << endl;
    cout << "Derive1 虚函数表 — 第一个函数地址：" << (uint64_t *)*(uint64_t *)(b) << endl;

    // 调用Derive1::f()，Base::g()， Base::h()，Derive1::g1()，Derive1::h1()
    ((Func) * ((uint64_t *)*(uint64_t *)(b)+0))();  // Derive1::f()
    ((Func) * ((uint64_t *)*(uint64_t *)(b)+1))();  // Base::g()
    ((Func) * ((uint64_t *)*(uint64_t *)(b)+2))();  // Base::h()
    ((Func) * ((uint64_t *)*(uint64_t *)(b)+3))();  // Derive1::g1()
    ((Func) * ((uint64_t *)*(uint64_t *)(b)+4))();  // Derive1::h1()


    ((Derive1 *)b)->g1();
    ((Derive1 *)b)->h1();
    b->f();
}
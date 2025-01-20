#include <PCH.h>


TEST(Learn_STD_Variant, Case1)
{
    std::variant<int> t = 50;
    std::get<int>(t) = 100;
    __debugbreak();
}
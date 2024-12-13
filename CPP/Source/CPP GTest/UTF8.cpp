#include "PCH.h"

TEST(UTF8, STD_Error_Code)
{
    GTEST_SKIP();

    std::setlocale(LC_ALL, ".932");
    auto t = strerror(1);
    __debugbreak();

    std::setlocale(LC_ALL, "zh_cn.UTF8");

    // 使用 std::errc 枚举来创建 std::error_code
    std::error_code ec = std::make_error_code(std::errc::io_error);

    std::cout << "Error code: " << ec.value() << std::endl;
    std::cout << "Error message: " << ec.message() << std::endl;


}
TEST(UTF8, Encoding)
{
    GTEST_SKIP();

    // Default is JP

    std::setlocale(LC_ALL, ".UTF8");


    WCHAR   wszMsgBuff[512];  // Buffer for text.

    DWORD   dwChars;  // Number of chars returned.
    DWORD dwErr = 100;
    // Try to get the message from the system errors.
    dwChars = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM |
        FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL,
        dwErr,
        0,
        wszMsgBuff,
        512,
        NULL);
    auto t = std::wstring(wszMsgBuff, dwChars);

    std::wcout << L"テスト：" << t << std::endl;

    //auto t = 
    //auto t2 = reinterpret_cast<const char8_t *>(t);
    //std::cout << t << std::endl;
}
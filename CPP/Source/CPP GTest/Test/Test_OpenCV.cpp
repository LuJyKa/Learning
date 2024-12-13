#include "PCH.h"

bool IsMemoryAccessible(void * address, size_t size)
{
    MEMORY_BASIC_INFORMATION mbi;
    if (VirtualQuery(address, &mbi, sizeof(mbi))) {
        return (mbi.State == MEM_COMMIT) &&
            (mbi.Protect & (PAGE_READONLY | PAGE_READWRITE | PAGE_EXECUTE_READ | PAGE_EXECUTE_READWRITE));
    }
    return false;
}

TEST(OpenCV, ZeroMat)
{
    void * tImageBytesPtr = new char[100 * 50 * 3];
    auto tMat = cv::Mat(100, 50, CV_8UC3, tImageBytesPtr);
    auto tSize1 = tMat.step[0] * tMat.rows;
    auto tSize2 = tMat.dataend - tMat.data;

    bool tIsOK = IsMemoryAccessible(tMat.data, tMat.dataend - tMat.data);
    __debugbreak();
}
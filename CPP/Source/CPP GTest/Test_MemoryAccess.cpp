#include "PCH.h"

void printMemoryLayout()
{
    SYSTEM_INFO si;
    GetSystemInfo(&si);

    MEMORY_BASIC_INFORMATION mbi;
    PVOID address = 0;

    while (address < si.lpMaximumApplicationAddress) {
        if (VirtualQuery(address, &mbi, sizeof(mbi))) {
            std::cout << "Base Address: " << mbi.BaseAddress
                << ", Region Size: " << mbi.RegionSize
                << ", State: " << std::hex << mbi.State
                << ", Protect: " << mbi.Protect
                << ", Type: " << mbi.Type << std::dec << std::endl;

            address = static_cast<PBYTE>(mbi.BaseAddress) + mbi.RegionSize;
        }
        else {
            break;
        }
    }
}


TEST(Test_MemoryAccess, GetMemoryInfo)
{
    printMemoryLayout();
}
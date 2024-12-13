#include "PCH.h"

template<typename T>
struct custom_heap_allocator
{
    using value_type = T;
    using pointer = T *;
    using const_pointer = const T *;
    using reference = T &;
    using const_reference = const T &;

    // No default constructor
    custom_heap_allocator() = delete;

    // An instance is tied to a heap handle.
    explicit custom_heap_allocator(HANDLE h)
        : heap{ h }
    {
    }

    // Instances are copy-constructable and copy-assignable.
    custom_heap_allocator(const custom_heap_allocator &) = default;
    custom_heap_allocator & operator=(const custom_heap_allocator &) = default;

    // All related allocators share the same heap, regardless of type.
    template<class U>
    custom_heap_allocator(const custom_heap_allocator<U> & other)
        : heap{ other.heap }
    {
    }

    // Allocate and deallocate space for objects using the heap.
    T * allocate(int n) {
        return static_cast<T *>(HeapAlloc(heap, 0, sizeof(T) * n));
    }
    void deallocate(T * ptr, int n) {
        HeapFree(heap, 0, ptr);
    }

    // Construct and destroy objects in previously allocated space.
    // This *should* be optional and provided by std::allocator_traits,
    // but it looks like some std containers don't use the traits.
    template< class U, class... Args >
    void construct(U * p, Args&&... args) {
        ::new((void *)p) U(std::forward<Args>(args)...);
    }
    template< class U >
    void destroy(U * p) {
        p->~U();
    }

    // Template for related allocators of different types.
    template<class U>
    struct rebind {
        typedef custom_heap_allocator<U> other;
    };

private:
    // Heap used for all allocations/deallocations.
    HANDLE heap;

    // Allow all related types to access our private heap.
    template<typename> friend struct custom_heap_allocator;
};


#include <gtest/gtest.h>
TEST(Test_CustomStdAllocator, DirectConstruct_WithoutName)
{
    GTEST_SKIP();

    HANDLE testHeap = ::HeapCreate(HEAP_NO_SERIALIZE, 0, 16384);
    EXPECT_NE((HANDLE)0, testHeap);
    EXPECT_NE(INVALID_HANDLE_VALUE, testHeap);

    using DoubleAllocator = custom_heap_allocator<double>;
    using DoubleVector = std::vector<double, DoubleAllocator>;
    using IntOrDoubleOrVectorOfDoubles = std::variant<int64_t, double, DoubleVector >;
    using IODOVDAllocator = custom_heap_allocator<IntOrDoubleOrVectorOfDoubles>;
    using VariantVector = std::vector<IntOrDoubleOrVectorOfDoubles, IODOVDAllocator>;

    try
    {
        { // extra scope to prevent exception shown below...
            DoubleAllocator dblAlloc{ testHeap };
            IODOVDAllocator variantAlloc{ testHeap };

            VariantVector vv(variantAlloc);

            vv.push_back(IntOrDoubleOrVectorOfDoubles(42LL));
            vv.push_back(IntOrDoubleOrVectorOfDoubles(42.42));
            vv.push_back(IntOrDoubleOrVectorOfDoubles(DoubleVector({ 1.0,2.0,3.0 }, dblAlloc)));

            EXPECT_EQ(3ULL, vv.size());
            EXPECT_EQ(0ULL, vv[0].index());
            EXPECT_EQ(1ULL, vv[1].index());
            EXPECT_EQ(2ULL, vv[2].index());
        } // end of extra scope.
        ::HeapDestroy(testHeap);
    }
    catch (...)
    {
        ::HeapDestroy(testHeap);
        throw;
    }
}
# CMakeList.txt : CMake project for CMake_Case1, include source and define
# project specific logic here.
#

# Add source to this project's executable.
add_executable (CMake_Case1 "CMake_Case1.cpp" "CMake_Case1.h")

if (CMAKE_VERSION VERSION_GREATER 3.12)
  set_property(TARGET CMake_Case1 PROPERTY CXX_STANDARD 20)
endif()

# TODO: Add tests and install targets if needed.

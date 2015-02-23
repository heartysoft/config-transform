@echo off

#might be a tad unsafe, but checking for the package directories turns out to be faster.

cls

IF NOT EXIST "src\config-transform\packages" (
    mkdir "src\config-transform\packages"
)

IF NOT EXIST "src\config-transform\packages\FAKE" (
    ".nuget\NuGet.exe" "Install" "FAKE" "-OutputDirectory" "src\config-transform\packages" "-ExcludeVersion"
)

IF NOT EXIST "src\config-transform\packages\FSharp.Configuration" (
    ".nuget\NuGet.exe" "Install" "FSharp.Configuration" "-OutputDirectory" "src\config-transform\packages" "-ExcludeVersion"
)

"src\config-transform\packages\FAKE\tools\Fake.exe" "build\build.fsx" %*


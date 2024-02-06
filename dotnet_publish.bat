@REM This will build the project for windows and linux
dotnet publish -c Release --self-contained -o release_build
dotnet publish -c Release --os linux --self-contained -o linux_release_build
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/Example.Service.UnitTest/Example.Service.UnitTest.csproj", "test/Example.Service.UnitTest/"]
COPY ["src/Example.Service/Example.Service.csproj", "src/Example.Service/"]
RUN dotnet restore "test/Example.Service.UnitTest/Example.Service.UnitTest.csproj"
COPY . .
WORKDIR "/src/test/Example.Service.UnitTest"
# run tests on docker run
ENTRYPOINT ["dotnet", "test"]
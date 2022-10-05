#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/Example.Service.ComponentTest/Example.Service.ComponentTest.csproj", "test/Example.Service.ComponentTest/"]
RUN dotnet restore "test/Example.Service.ComponentTest/Example.Service.ComponentTest.csproj"
COPY . .
WORKDIR "/src/test/Example.Service.ComponentTest"

# run tests on docker run
ENTRYPOINT ["dotnet", "test"]
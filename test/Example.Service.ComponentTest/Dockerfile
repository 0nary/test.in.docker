#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["test/Example.Service.ComponentTest/Example.Service.ComponentTest.csproj", "test/Example.Service.ComponentTest/"]
RUN dotnet restore "test/Example.Service.ComponentTest/Example.Service.ComponentTest.csproj"
COPY . .
WORKDIR "/src/test/Example.Service.ComponentTest"
RUN dotnet build "Example.Service.ComponentTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.Service.ComponentTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Service.ComponentTest.dll"]
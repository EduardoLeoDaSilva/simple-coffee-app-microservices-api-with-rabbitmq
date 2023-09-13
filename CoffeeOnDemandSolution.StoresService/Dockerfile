#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoffeeOnDemandSolution.StoresService/CoffeeOnDemandSolution.StoresService.csproj", "CoffeeOnDemandSolution.StoresService/"]
COPY ["CoffeeOnDemandSolution.Common/CoffeeOnDemandSolution.Common.csproj", "CoffeeOnDemandSolution.Common/"]
RUN dotnet restore "CoffeeOnDemandSolution.StoresService/CoffeeOnDemandSolution.StoresService.csproj"
COPY . .
WORKDIR "/src/CoffeeOnDemandSolution.StoresService"
RUN dotnet build "CoffeeOnDemandSolution.StoresService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoffeeOnDemandSolution.StoresService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeOnDemandSolution.StoresService.dll"]
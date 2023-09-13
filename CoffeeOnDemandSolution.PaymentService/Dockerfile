#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoffeeOnDemandSolution.PaymentService/CoffeeOnDemandSolution.PaymentService.csproj", "CoffeeOnDemandSolution.PaymentService/"]
COPY ["CoffeeOnDemandSolution.Common/CoffeeOnDemandSolution.Common.csproj", "CoffeeOnDemandSolution.Common/"]
RUN dotnet restore "CoffeeOnDemandSolution.PaymentService/CoffeeOnDemandSolution.PaymentService.csproj"
COPY . .
WORKDIR "/src/CoffeeOnDemandSolution.PaymentService"
RUN dotnet build "CoffeeOnDemandSolution.PaymentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoffeeOnDemandSolution.PaymentService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoffeeOnDemandSolution.PaymentService.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SalaryService.Api/SalaryService.Api.csproj", "SalaryService.Api/"]
COPY ["SalaryService.Application/SalaryService.Application.csproj", "SalaryService.Application/"]
COPY ["SalaryService.DataAccess/SalaryService.DataAccess.csproj", "SalaryService.DataAccess/"]
COPY ["SalaryService.Domain/SalaryService.Domain.csproj", "SalaryService.Domain/"]
RUN dotnet restore "SalaryService.Api/SalaryService.Api.csproj"
COPY . .
WORKDIR "/src/SalaryService.Api"
RUN dotnet build "SalaryService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalaryService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalaryService.Api.dll"]
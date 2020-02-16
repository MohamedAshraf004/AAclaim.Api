#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Services/Acclaim/Acclaim.Api/Acclaim.Api.csproj", "Services/Acclaim/Acclaim.Api/"]
RUN dotnet restore "Services/Acclaim/Acclaim.Api/Acclaim.Api.csproj"
COPY . .
WORKDIR "/src/Services/Acclaim/Acclaim.Api"
RUN dotnet build "Acclaim.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Acclaim.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acclaim.Api.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN mkdir files
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Egress.API/Egress.API.csproj", "Egress.API/"]
COPY ["src/Egress.Infra/Egress.Infra.CrossCutting.IoC/Egress.Infra.CrossCutting.IoC.csproj", "Egress.Infra/Egress.Infra.CrossCutting.IoC/"]
COPY ["src/Egress.Application/Egress.Application.csproj", "Egress.Application/"]
COPY ["src/Egress.Infra/Egress.Infra.Data/Egress.Infra.Data.csproj", "Egress.Infra/Egress.Infra.Data/"]
COPY ["src/Egress.Domain/Egress.Domain.csproj", "Egress.Domain/"]
COPY ["src/Egress.Infra/Egress.Infra.CrossCutting.Resource/Egress.Infra.CrossCutting.Resource.csproj", "Egress.Infra/Egress.Infra.CrossCutting.Resource/"]
RUN dotnet restore "Egress.API/Egress.API.csproj"
COPY src .
WORKDIR "/src/Egress.API"
RUN dotnet build "Egress.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Egress.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egress.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Egress.API/Egress.API.csproj", "Egress.API/"]
COPY ["Egress.Infra/Egress.Infra.CrossCutting.IoC/Egress.Infra.CrossCutting.IoC.csproj", "Egress.Infra/Egress.Infra.CrossCutting.IoC/"]
COPY ["Egress.Application/Egress.Application.csproj", "Egress.Application/"]
COPY ["Egress.Infra/Egress.Infra.Data/Egress.Infra.Data.csproj", "Egress.Infra/Egress.Infra.Data/"]
COPY ["Egress.Domain/Egress.Domain.csproj", "Egress.Domain/"]
COPY ["Egress.Infra/Egress.Infra.CrossCutting.Resource/Egress.Infra.CrossCutting.Resource.csproj", "Egress.Infra/Egress.Infra.CrossCutting.Resource/"]
RUN dotnet restore "Egress.API/Egress.API.csproj"
COPY . .
WORKDIR "/src/Egress.API"
RUN dotnet build "Egress.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Egress.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN mkdir files
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egress.API.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Presentation/UniSpace.Api.csproj", "src/Presentation/"]
COPY ["src/Application/UniSpace.Application.csproj", "src/Application/"]
COPY ["src/Domain/UniSpace.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/UniSpace.Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/Presentation/UniSpace.Api.csproj"

COPY . .
WORKDIR "/src/src/Presentation"
RUN dotnet publish "UniSpace.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UniSpace.Api.dll"]

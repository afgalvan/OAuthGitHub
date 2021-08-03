FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/OAuthGitHub.Api/OAuthGitHub.Api.csproj", "OAuthGitHub.Api/"]
COPY ["src/OAuthGitHub.Core/OAuthGitHub.Core.csproj", "OAuthGitHub.Core/"]
COPY ["src/OAuthGitHub.Shared/OAuthGitHub.Shared.csproj", "OAuthGitHub.Shared/"]
RUN dotnet restore "/src/OAuthGitHub.Api/OAuthGitHub.Api.csproj"
COPY ./src .
WORKDIR "/src/OAuthGitHub.Api"
RUN dotnet build "OAuthGitHub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OAuthGitHub.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OAuthGitHub.Api.dll"]

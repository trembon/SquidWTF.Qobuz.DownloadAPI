
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release
COPY . .
WORKDIR /
RUN dotnet restore SquidWTF.Qobuz.DownloadAPI/SquidWTF.Qobuz.DownloadAPI.csproj -a $TARGETARCH
RUN dotnet publish SquidWTF.Qobuz.DownloadAPI/SquidWTF.Qobuz.DownloadAPI.csproj -a $TARGETARCH --no-restore -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/playwright/dotnet:v1.58.0-noble
WORKDIR /app 
EXPOSE 8080

RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --install-dir /usr/share/dotnet --channel 10.0

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SquidWTF.Qobuz.DownloadAPI.dll"]
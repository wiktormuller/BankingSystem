FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY . .
RUN dotnet publish BankingSystem -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URL http://*:80
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT [ "dotnet", "BankingSystem.dll" ]
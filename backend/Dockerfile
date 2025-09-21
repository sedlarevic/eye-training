FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY eye-training.API/*.csproj eye-training.API/

RUN dotnet restore "./eye-training.API/eye-training.API.csproj"

COPY . .

RUN dotnet publish eye-training.API/eye-training.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "eye-training.API.dll"]


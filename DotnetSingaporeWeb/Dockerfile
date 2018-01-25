FROM gcr.io/google-appengine/aspnetcore:2.0.3 AS base
WORKDIR /app
EXPOSE 8080

FROM gcr.io/google-appengine/aspnetcore:2.0.3 AS build
WORKDIR /src
COPY *.sln ./
COPY DotnetSingaporeWeb/DotnetSingaporeWeb.csproj DotnetSingaporeWeb/
RUN dotnet restore
COPY . .
WORKDIR /src/DotnetSingaporeWeb
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DotnetSingaporeWeb.dll"]

FROM gcr.io/google-appengine/aspnetcore:2.0.3 AS base
WORKDIR /app
EXPOSE 5000

FROM gcr.io/google-appengine/aspnetcore:2.0.3 AS build
WORKDIR /src
COPY *.sln ./
COPY SocialFeed/SocialFeed.csproj SocialFeed/
RUN dotnet restore
COPY . .
WORKDIR /src/SocialFeed
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SocialFeed.dll"]

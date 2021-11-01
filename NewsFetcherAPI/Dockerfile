FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NewsFetcherAPI/NewsFetcherAPI.csproj", "NewsFetcherAPI/"]
RUN dotnet restore "NewsFetcherAPI/NewsFetcherAPI.csproj"
COPY . .
WORKDIR "/src/NewsFetcherAPI"
RUN dotnet build "NewsFetcherAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsFetcherAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsFetcherAPI.dll"]

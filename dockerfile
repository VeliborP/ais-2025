FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["WebShop.MVC/WebShop.MVC.csproj", "WebShop.MVC/"]
RUN dotnet restore "WebShop.MVC/WebShop.MVC.csproj"

COPY . .
WORKDIR "/src/WebShop.MVC"
RUN dotnet publish "WebShop.MVC.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# For Render to work with PORT
ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "WebShop.MVC.dll"]

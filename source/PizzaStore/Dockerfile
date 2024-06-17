FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PizzaStore/PizzaStore.csproj", "PizzaStore/"]
RUN dotnet restore "PizzaStore/PizzaStore.csproj"
COPY . .
WORKDIR "/src/PizzaStore"
RUN dotnet build "PizzaStore.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PizzaStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM build AS database
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef database update --project PizzaStore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=database /src/PizzaStore/Pizzas.db .
CMD ["chmod", "775", "Pizzas.db"]
ENTRYPOINT ["dotnet", "PizzaStore.dll"]
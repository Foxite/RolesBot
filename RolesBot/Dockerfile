FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RolesBot/RolesBot.csproj", "RolesBot/"]
RUN dotnet restore "RolesBot/RolesBot.csproj"
COPY . .
WORKDIR "/src/RolesBot"
RUN dotnet build "RolesBot.csproj" -c Release -o /app/build
RUN dotnet publish "RolesBot.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./RolesBot"]

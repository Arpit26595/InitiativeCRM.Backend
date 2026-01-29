# ---------- BUILD STAGE ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution file
COPY *.sln ./

# Copy project files
COPY Lead.API/Lead.API.csproj Lead.API/
COPY Lead.Application/Lead.Application.csproj Lead.Application/
COPY Lead.Infrastructure/Lead.Infrastructure.csproj Lead.Infrastructure/
COPY Lead.Domain/Lead.Domain.csproj Lead.Domain/
COPY Lead.Database/Lead.Database.csproj Lead.Database/

# Restore using solution
RUN dotnet restore 

# Copy everything else
COPY . .

# Publish API
RUN dotnet publish Lead.API/Lead.API.csproj -c Release -o /app/publish

# ---------- RUNTIME STAGE ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Lead.API.dll"]

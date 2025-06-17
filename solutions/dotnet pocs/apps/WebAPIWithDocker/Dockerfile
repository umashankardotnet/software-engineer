#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# To work swagger need to set Development environment , If you comment below line swaggwe will not work because of If condition in program file
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose the port the app runs on
EXPOSE 8080
EXPOSE 8081
# Use the official .NET Core SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy the project files into the container
COPY ["WebAPIWithDocker.csproj", "./"]
RUN dotnet restore "WebAPIWithDocker.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebAPIWithDocker.csproj" -c Release -o /app/build

# Build the application
FROM build AS publish
RUN dotnet publish "WebAPIWithDocker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Define the startup command
ENTRYPOINT ["dotnet", "WebAPIWithDocker.dll"]
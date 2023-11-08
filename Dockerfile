# Use the official .NET SDK image as the base image for building and running
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

# Build and run the application
RUN dotnet restore
RUN dotnet build
EXPOSE 5189
CMD ["dotnet", "run"]

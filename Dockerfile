# Use the official .NET SDK image as the base image for building and running
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

# Build and run the application
RUN dotnet restore
RUN dotnet build

# Install EF Core Tools
RUN dotnet tool install --global dotnet-ef --version 7.0.13
ENV PATH="${PATH}:/root/.dotnet/tools"

# Run database migrations and start the application
CMD ["sh", "-c", "dotnet ef database update && dotnet run --urls \"http://0.0.0.0:5189\""]

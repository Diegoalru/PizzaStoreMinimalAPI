#!/bin/bash

# Move to the source directory
cd /workspaces/MinimalApiEfCore/source

# Install the EF Core tool
dotnet tool install --global dotnet-ef

# Restore the NuGet packages
dotnet restore

# Build the project
dotnet build

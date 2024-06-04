#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 443

COPY . . 

FROM base AS final
WORKDIR /app
ENTRYPOINT ["dotnet", "QuickFireApi.dll"]
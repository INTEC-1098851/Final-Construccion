#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install -y libgdiplus libc6-dev

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlazorAppOtus/Server/BlazorAppOtus.Server.csproj", "BlazorAppOtus/Server/"]
COPY ["BlazorAppOtus/Client/BlazorAppOtus.Client.csproj", "BlazorAppOtus/Client/"]
COPY ["BlazorAppOtus/Shared/BlazorAppOtus.Shared.csproj", "BlazorAppOtus/Shared/"]
COPY ["BlazorAppOtus/GenericRepository/GenericRepository.csproj", "BlazorAppOtus/GenericRepository/"]
COPY ["BlazorAppOtus/GenericHttpRequestHandler/GenericHttpRequestHandler.csproj", "BlazorAppOtus/GenericHttpRequestHandler/"]
COPY ["BlazorAppOtus/SharedModels/SharedModels.csproj", "BlazorAppOtus/SharedModels/"]
COPY ["BlazorAppOtus/MailService/MailService.csproj", "BlazorAppOtus/MailService/"]
COPY ["BlazorAppOtus/SharedResources/SharedResources.csproj", "BlazorAppOtus/SharedResources/"]
COPY ["BlazorAppOtus/GenericHttpResponseHandler/GenericHttpResponseHandler.csproj", "BlazorAppOtus/GenericHttpResponseHandler/"]
COPY ["BlazorAppOtus/LoggerService/LoggerService.csproj", "BlazorAppOtus/LoggerService/"]



RUN dotnet restore "BlazorAppOtus/Server/BlazorAppOtus.Server.csproj"
COPY . .
WORKDIR "/src/BlazorAppOtus/Server"
RUN dotnet build "BlazorAppOtus.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorAppOtus.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorAppOtus.Server.dll"]
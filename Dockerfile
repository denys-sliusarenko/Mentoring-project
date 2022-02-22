#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-buster-slim AS build
WORKDIR /src
COPY ["src/Mentoring project.Web/MentoringProject.Web.csproj", "src/Mentoring project.Web/"]
COPY ["src/MentoringProject.Infrastructure/MentoringProject.Infrastructure.csproj", "src/MentoringProject.Infrastructure/"]
COPY ["src/MentoringProject.Domain/MentoringProject.Domain.csproj", "src/MentoringProject.Domain/"]
COPY ["src/MentoringProject.Application/MentoringProject.Application.csproj", "src/MentoringProject.Application/"]
RUN dotnet restore "src/Mentoring project.Web/MentoringProject.Web.csproj"
COPY . .
WORKDIR "/src/src/Mentoring project.Web"
RUN dotnet build "MentoringProject.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MentoringProject.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MentoringProject.Web.dll"]
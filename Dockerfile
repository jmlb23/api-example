FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine  AS build-env
WORKDIR /app

COPY [".", "./"]
RUN dotnet tool restore ; dotnet restore Project.slnx
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
EXPOSE 8080
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build-env /app/out .
RUN chmod +x ./api.dll
ENTRYPOINT ["dotnet"]
CMD ["./api.dll"]

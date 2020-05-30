# this goes to Microsoft's own Docker Repository to get the runtime on some version of Linux (not sure which one!)
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
WORKDIR /app

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT 0
RUN  mount -v 34.75.156.77:/image /app/wwwroot/image
RUN  mount -v 34.75.156.77:/resumes /app/wwwroot/Resumes

ENTRYPOINT ["dotnet", "ConstellationWebApp.dll"]
EXPOSE 80
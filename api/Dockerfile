FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /backend
COPY ./src/ ./src
COPY ./tests/ ./tests
COPY FamilyBudget.sln ./

RUN dotnet restore 
RUN dotnet publish -c Release -o /app/out

FROM base AS final
# WORKDIR /app 
COPY  --from=base /app/out .

EXPOSE 5000/tcp
ENTRYPOINT ["dotnet", "FamilyBudget.Api.dll"]
 
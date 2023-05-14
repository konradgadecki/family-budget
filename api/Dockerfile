FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /src
COPY ./src/ ./src
COPY FamilyBudget.sln ./

RUN dotnet restore
# RUN dotnet build "FamilyBudget.csproj" -c Release -o /app/build
 
RUN dotnet publish -c Release -o /app/out

FROM base AS final
# WORKDIR /app 
COPY  --from=base /app/out .

EXPOSE 5018/tcp
ENTRYPOINT ["dotnet", "FamilyBudget.Api.dll"]
 
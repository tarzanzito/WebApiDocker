
#######################
#STEP 1 - build only
#######################

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env


#adiciona informação no metadata da imagem. quando 'docker image inspect' - encontramos em 'Config/labels' (Label key=value)
LABEL mystage=builder
LABEL myname="Paulo"

# directorio de trabalhos. commandos: Any RUN, CMD, ADD, COPY, or ENTRYPOINT são executados neste directorio.
WORKDIR /App

# Copia tudo,  COPY <src-path> <destination-path>
COPY . ./

# Restore as distinct layers , run a linux command
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out
#RUN dotnet publish -c Release -r linux-x64 -o out

############################################################
#STEP 2 - runtime only  - cria uma imagem sem os sources
############################################################

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as production-env


WORKDIR /App

#copia todos do resultado do WORKDIR de 'as build-env' para /App/out
COPY --from=build-env /App/out .

# Portos do container a ficarem expostos : portoExterno:8080
EXPOSE 8080

# volumes necessarios (necessario dizer no run -v MyLogs:/App/Logs)
VOLUME MyLogs

# aplicação que deve correr
ENTRYPOINT ["dotnet", "BasicApi.dll"]



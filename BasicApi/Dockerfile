﻿
# docker build -t imageName:TagName -f Dockerfile .

#######################
#STEP 1 - build only
#######################

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Any RUN, CMD, ADD, COPY, or ENTRYPOINT command will be executed in the specified working directory.
WORKDIR /App

# Copy everything,  COPY <src-path> <destination-path>
COPY . ./

# Restore as distinct layers , run a linux command
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

############################################################
#STEP 2 - runtime only  - cria uma imagem sem os sources
############################################################


# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as production-env

WORKDIR /App

#copy all from image named 'build-env' /App/out to WORRDIR
COPY --from=build-env /App/out .

# no dockerHub quando se faz start, nas configuracoes ficámos a poder dizer que: portoExterno:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BasicApi.dll"]



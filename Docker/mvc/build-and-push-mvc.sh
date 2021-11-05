#!/bin/sh

docker build . -f Docker/mvc/Dockerfile -t epperic/studienuebersicht:latest
docker login
docker push epperic/studienuebersicht:latest

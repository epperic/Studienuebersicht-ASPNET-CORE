#!/bin/sh

docker network create postgres-network

docker run \
--name postgres \
--hostname postgres \
-p5432:5432 \
-e POSTGRES_PASSWORD=testtest \
--net postgres-network \
-v postgres-data:/var/lib/postgresql/data/ \
-d postgres

docker run \
--name pgadmin \
-p8080:80 \
-e PGADMIN_DEFAULT_EMAIL=admin@localhost.com \
-e PGADMIN_DEFAULT_PASSWORD=testtest \
--net postgres-network \
-v pgadmin-data:/var/lib/pgadmin/ \
-d dpage/pgadmin4

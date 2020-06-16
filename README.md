# readme

[![build status](https://github.com/RVTR/rvtr-api-booking/workflows/build/badge.svg)](https://github.com/RVTR/rvtr-api-booking/actions?query=workflow%3Abuild)
[![release status](https://github.com/RVTR/rvtr-api-booking/workflows/release/badge.svg)](https://github.com/RVTR/rvtr-api-booking/actions?query=workflow%3Arelease)
[![coverage status](https://sonarcloud.io/api/project_badges/measure?project=rvtr_api_booking&metric=coverage)](https://sonarcloud.io/dashboard?id=rvtr_api_booking)
[![maintainability rating](https://sonarcloud.io/api/project_badges/measure?project=rvtr_api_booking&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=rvtr_api_booking)
[![reliability rating](https://sonarcloud.io/api/project_badges/measure?project=rvtr_api_booking&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=rvtr_api_booking)
[![security rating](https://sonarcloud.io/api/project_badges/measure?project=rvtr_api_booking&metric=security_rating)](https://sonarcloud.io/dashboard?id=rvtr_api_booking)

RVTR Servicehub Booking API

## backlog

<https://www.pivotaltracker.com/n/projects/2443483>

## license

The project is made available under the terms of the [MIT License][license_mit].

[license_mit]: https://github.com/rvtr/rvtr-api-booking/blob/master/LICENSE 'mit license'

## Guides/References
A set of resources used in the development of this project


### Docker
Run this command to build the docker image for the application and the db

`docker-compose -f .\.docker\docker_manifest.yaml build`

the run `docker-compose -f .\.docker\docker_manifest.yaml up` to start the containers

To wipe everything and start the process again, run `docker-compose -f .\.docker\docker_manifest.yaml  down --volumes`

### <span>ASP.NET</span>

#### <span>ASP.Net</span> Core Web API with Docker Compose, PostgreSQL and EF Core
https://medium.com/front-end-weekly/net-core-web-api-with-docker-compose-postgresql-and-ef-core-21f47351224f

#### Installing Postgresql on a local windows machine
https://www.microfocus.com/documentation/idol/IDOL_12_0/MediaServer/Guides/html/English/Content/Getting_Started/Configure/_TRN_Set_up_PostgreSQL.htm

#### Unit of Work for ASP.NET Core
https://medium.com/@chathuranga94/unit-of-work-for-asp-net-core-706e71abc9d1


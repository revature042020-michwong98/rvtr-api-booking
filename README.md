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

## Usage

The current base API route is: `/api/v0.0/`

### Resources
There are two resourcs that the Booking Api provides: `Booking` and `Stay`

The following are endpoints and the supported request methods for requesting data

#### Booking

`GET /Booking` - Fetches a list of all known bookings from the db

`GET /Booking/:id` - Fetches a single Booking record based on id

`POST /Booking` - Posts a new Booking record to the db.  The following JSON schema is required to post a Booking:

```typescript
{
  "AccountId": number,
  "LodgingId": number,
  "Guests": [],
  "Rentals": [],
  "Status": string,
  "Stay": {
    "CheckIn": Date,
    "CheckOut": Date,
    "Bookingid": number
  }
}
```

`PUT /Booking` - Updates a booking record based on the JSON object it receives. Ensure all properties
are sent in the request for the record to succesfully update.

`DELETE /Booking/:Id` - Deletes a Booking record from the database based on id

#### Stay

`GET /Stay` - Fetches a list of all known Stay records from the db

`GET /Stay/:id` - Fetches a single Stay record based on id

`POST /Stay` - Posts a new Stay record to the db.  The following JSON schema is required to post a Stay:

```typescript
{
  "CheckIn": Date,
  "CheckOut": Date,
  "Bookingid": number
}
```

Note: An exist BookingId must be given

`PUT /Stay` - Updates a Stay record based on the JSON object it receives. Ensure all properties
are sent in the request for the record to succesfully update.

`DELETE /Stay/:Id` - Deletes a Stay record from the database based on id

#### Querying

Currently, the api supports the following methods for querying data, `limit`, `offset`, `sort` and `filter`

`limit` and `limit` are fairly self explantory.  Apply the following parameter queries to utilize these filters:

`/Booking?limit=5&offset=5`

For `sort`, specify the propertyof the resource you wish to sort by.

```
Sort by checkin date
/Stay?sort=Checkin
```

For applying a reverse order sort, simply apply `desc` to the query value

```
Reverse sort by checkin date
/Stay?sort=Checkin desc
```

**Note**: A space is required between the property and the `desc` keyword

For filtering under constraints, apply the `fitler` query

```
Filter Stay's to a matching bookingId
/Stay?filter=bookingId == 1
```


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
https://medium.com/@chathuranga94/unit-of-work-for-asp-net-core-706e71abc9d

#### Tutorial: Add sorting, filtering, and paging - <span>ASP.NET</span> MVC with EF Core
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-3.1

#### Filtering and paging collection result in ASP.NET Core Web API
https://dejanstojanovic.net/aspnet/2019/january/filtering-and-paging-in-aspnet-core-web-api/

CREATE DATABASE "BookingData";

\connect BookingData

CREATE TABLE "Bookings"
(
  "Id" serial,
  "AccountId" INTEGER NOT NULL,
  "LodgingId" INTEGER NOT NULL,
  "Status" VARCHAR(100),
  PRIMARY KEY ("Id")
);

CREATE TABLE "Stays"
(
  "Id" serial,
  "CheckIn" TIMESTAMP NOT NULL,
  "CheckOut" TIMESTAMP NOT NULL,
  "DateCreated" TIMESTAMP NOT NULL,
  "DateModified" TIMESTAMP NOT NULL,
  "BookingId" INTEGER REFERENCES "Bookings"("Id"),
  PRIMARY KEY ("Id")
);
-- CREATE TABLE Guest (id int PRIMARY KEY,);
-- CREATE TABLE Rental (id int PRIMARY KEY,);
-- CREATE TABLE Stays (id int PRIMARY KEY,);

-- Create Bookings
INSERT INTO "Bookings"
  ("AccountId", "LodgingId", "Status")
values
  (1, 1, 'Booking');
INSERT INTO "Bookings"
  ("AccountId", "LodgingId", "Status")
values
  (1, 2, 'Booked');
INSERT INTO "Bookings"
  ("AccountId", "LodgingId", "Status")
values
  (2, 2, 'Cancelled');

-- Create Stays
INSERT INTO "Stays"
  (
  "CheckIn",
  "CheckOut",
  "DateCreated",
  "DateModified",
  "BookingId"
  )
values
  (
    '2020-01-01',
    '2020-02-01',
    '2020-01-01',
    '2020-01-01',
    1
);

INSERT INTO "Stays"
  (
  "CheckIn",
  "CheckOut",
  "DateCreated",
  "DateModified",
  "BookingId"
  )
values
  (
    '2020-04-01',
    '2020-05-01',
    '2020-04-01',
    '2020-04-01',
    2
);

INSERT INTO "Stays"
  (
  "CheckIn",
  "CheckOut",
  "DateCreated",
  "DateModified",
  "BookingId"
  )
values
  (
    '2020-06-01',
    '2020-07-01',
    '2020-06-01',
    '2020-06-01',
    3
);

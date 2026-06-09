-- Table: Reservation_Services
CREATE TABLE Reservation_Services (
                                      ReservationId int  NOT NULL,
                                      ServiceId int  NOT NULL,
                                      Quantity int  NOT NULL,
                                      ServiceDate date  NOT NULL,
                                      CONSTRAINT Reservation_Services_pk PRIMARY KEY  (ReservationId,ServiceId)
);

-- Table: Reservations
CREATE TABLE Reservations (
                              ReservationId int  NOT NULL IDENTITY,
                              GuestId int  NOT NULL,
                              RoomId int  NOT NULL,
                              CheckInDate date  NOT NULL,
                              CheckOutDate date  NULL,
                              Status varchar(50)  NOT NULL,
                              CONSTRAINT Reservations_pk PRIMARY KEY  (ReservationId)
);

-- Table: Guests
CREATE TABLE Guests (
                        GuestId int  NOT NULL IDENTITY,
                        FirstName varchar(50)  NOT NULL,
                        LastName varchar(100)  NOT NULL,
                        Email varchar(100)  NOT NULL,
                        Phone varchar(9)  NOT NULL,
                        CONSTRAINT Guests_pk PRIMARY KEY  (GuestId)
);

-- Table: Rooms
CREATE TABLE Rooms (
                       RoomId int  NOT NULL IDENTITY,
                       RoomNumber varchar(10)  NOT NULL,
                       Type varchar(50)  NOT NULL,
                       PricePerNight decimal(10,2)  NOT NULL,
                       Floor int  NOT NULL,
                       CONSTRAINT Rooms_pk PRIMARY KEY  (RoomId)
);

-- Table: Services
CREATE TABLE Services (
                          ServiceId int  NOT NULL IDENTITY,
                          Name varchar(100)  NOT NULL,
                          Description varchar(200)  NOT NULL,
                          Price decimal(10,2)  NOT NULL,
                          DurationMinutes int  NOT NULL,
                          CONSTRAINT Services_pk PRIMARY KEY  (ServiceId)
);

ALTER TABLE Reservation_Services ADD CONSTRAINT Reservation_Services_Reservations
    FOREIGN KEY (ReservationId)
        REFERENCES Reservations (ReservationId);

ALTER TABLE Reservation_Services ADD CONSTRAINT Reservation_Services_Services
    FOREIGN KEY (ServiceId)
        REFERENCES Services (ServiceId);

ALTER TABLE Reservations ADD CONSTRAINT Reservations_Guests
    FOREIGN KEY (GuestId)
        REFERENCES Guests (GuestId);

ALTER TABLE Reservations ADD CONSTRAINT Reservations_Rooms
    FOREIGN KEY (RoomId)
        REFERENCES Rooms (RoomId);

-- End of file.

-- Guests
INSERT INTO Guests (FirstName, LastName, Email, Phone) VALUES
                                                           ('Anna', 'Kowalska', 'anna.kowalska@email.com', '123456789'),
                                                           ('Jan', 'Nowak', 'jan.nowak@email.com', '234567891'),
                                                           ('Maria', 'Wisniewska', 'maria.w@email.com', '345678912'),
                                                           ('Piotr', 'Zielinski', 'piotr.z@email.com', '456789123'),
                                                           ('Katarzyna', 'Lewandowska', 'k.lewandowska@email.com', '567891234');

-- Rooms
INSERT INTO Rooms (RoomNumber, Type, PricePerNight, Floor) VALUES
                                                               ('101', 'Single', 150.00, 1),
                                                               ('205', 'Double', 250.00, 2),
                                                               ('310', 'Suite', 500.00, 3),
                                                               ('102', 'Single', 150.00, 1),
                                                               ('408', 'Penthouse', 1200.00, 4);

-- Services
INSERT INTO Services (Name, Description, Price, DurationMinutes) VALUES
                                                                     ('Breakfast Buffet', 'Full breakfast buffet with hot and cold dishes', 50.00, 60),
                                                                     ('Spa Treatment', 'Full body relaxation massage', 200.00, 90),
                                                                     ('Airport Transfer', 'Private car transfer to/from airport', 120.00, 45),
                                                                     ('Room Cleaning Extra', 'Additional deep room cleaning service', 80.00, 30),
                                                                     ('City Tour', 'Guided sightseeing tour of the city', 150.00, 180);

-- Reservations
INSERT INTO Reservations (GuestId, RoomId, CheckInDate, CheckOutDate, Status) VALUES
                                                                                  (1, 2, '2026-06-01', '2026-06-05', 'Completed'),
                                                                                  (2, 1, '2026-06-10', NULL, 'Confirmed'),
                                                                                  (3, 3, '2026-06-15', '2026-06-18', 'Completed'),
                                                                                  (4, 5, '2026-07-01', NULL, 'Cancelled'),
                                                                                  (5, 4, '2026-06-20', '2026-06-22', 'Completed');

-- Reservation Services
INSERT INTO Reservation_Services (ReservationId, ServiceId, Quantity, ServiceDate) VALUES
                                                                                       (1, 1, 4, '2026-06-02'),
                                                                                       (1, 2, 1, '2026-06-03'),
                                                                                       (2, 3, 1, '2026-06-10'),
                                                                                       (3, 1, 3, '2026-06-16'),
                                                                                       (5, 4, 1, '2026-06-21'),
                                                                                       (5, 1, 2, '2026-06-21');
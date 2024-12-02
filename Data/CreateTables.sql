-- SQLite
DROP TABLE IF EXISTS Vehicles;
DROP TABLE IF EXISTS Manufacturers;

-- Skapa en tabell för tillverkare ✅
-- Koppla ihop Vehicles med den nya tabellen ✅
-- Skapa regeln för föräldralösa barn... ✅
CREATE TABLE IF NOT EXISTS Manufacturers(
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  Name TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS Vehicles(
  Id INTEGER PRIMARY KEY AUTOINCREMENT,
  ManufacturerId INTEGER NOT NULL,
  RegistrationNumber TEXT NOT NULL,
  Model TEXT NOT NULL,
  ModelYear INTEGER NOT NULL,
  ImageUrl TEXT NULL,
  Mileage INTEGER NOT NULL,
  Value INTEGER NOT NULL,
  Description TEXT NULL,
  FOREIGN KEY(ManufacturerId) REFERENCES Manufacturers(Id)
);
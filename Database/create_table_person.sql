-- Script Date: 03.02.2019 21:14  - ErikEJ.SqlCeScripting version 3.5.2.80
CREATE TABLE [Person] (
  [Id] INTEGER NOT NULL
, [Titel] TEXT
, [Anrede] TEXT NOT NULL
, [Vorname] TEXT NOT NULL
, [Nachname] TEXT NOT NULL
, [Geburtsdatum] TEXT
, [Geburtsort] TEXT
, CONSTRAINT [PK_Person] PRIMARY KEY ([Id])
);

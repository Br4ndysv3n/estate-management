CREATE TABLE [Versorger] (
  [Id] INTEGER NOT NULL
, [VersorgerTyp] INTEGER NOT NULL
, [Name] TEXT NOT NULL
, [Strasse] TEXT NOT NULL
, [Hausnummer] TEXT NOT NULL
, [Plz] TEXT NOT NULL
, [Ort] TEXT NOT NULL
, CONSTRAINT [PK_Versorger] PRIMARY KEY ([Id])
);

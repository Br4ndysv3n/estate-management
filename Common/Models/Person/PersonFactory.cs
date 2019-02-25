using System;

namespace Common.Models.Person
{
    public class PersonFactory
    {
        public static IPerson CreatePerson(int id, string titel, EnumAnrede anrede, string vorname, string nachname, DateTime geburtsdatum, string geburtsort)
        {
            return new Person
            {
                Id = id,
                Titel = titel,
                Anrede = anrede,
                Vorname = vorname,
                Nachname = nachname,
                Geburtsort = geburtsort,
                Geburtsdatum = geburtsdatum
            };
        }

        public static IPerson CreateNew()
        {
            return CreatePerson(0, string.Empty, EnumAnrede.Herr, string.Empty, string.Empty, new DateTime(1970, 1, 1), string.Empty);
        }
    }
}

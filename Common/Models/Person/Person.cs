using System;

namespace Common.Models.Person
{
    public class Person : IPerson
    {
        /// <summary>
        /// Liefert die Id des Stammdatums oder setzt diese.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Liefert den Titel der Person.
        /// </summary>
        public string Titel { get; set; }

        /// <summary>
        /// Liefert die Anrede der Person.
        /// </summary>
        public EnumAnrede Anrede { get; set; }

        /// <summary>
        /// Liefert den Vornamen der Person.
        /// </summary>
        public string Vorname { get; set; }

        /// <summary>
        /// Liefert den Nachnamen der Person.
        /// </summary>
        public string Nachname { get; set; }

        /// <summary>
        /// Liefert das Geburtsdatum der Person.
        /// </summary>
        public DateTime Geburtsdatum { get; set; }

        /// <summary>
        /// Liefert den Geburtsort der Person.
        /// </summary>
        public string Geburtsort { get; set; }

        public EnumStammdatenTyp StammdatenTyp => EnumStammdatenTyp.PERSON;
    }
}

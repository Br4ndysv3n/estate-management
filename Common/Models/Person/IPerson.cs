using System;

namespace Common.Models.Person
{
    /// <summary>
    /// Eine Schnittstelle für alle Klassen, die Personen darstellen.
    /// </summary>
    public interface IPerson : IStammdaten
    {
        /// <summary>
        /// Liefert den Titel der Person.
        /// </summary>
        string Titel { get; set; }

        /// <summary>
        /// Liefert die Anrede der Person.
        /// </summary>
        EnumAnrede Anrede { get; set; }

        /// <summary>
        /// Liefert den Vornamen der Person.
        /// </summary>
        string Vorname { get; set; }

        /// <summary>
        /// Liefert den Nachnamen der Person.
        /// </summary>
        string Nachname { get; set; }

        /// <summary>
        /// Liefert das Geburtsdatum der Person.
        /// </summary>
        DateTime Geburtsdatum { get; set; }

        /// <summary>
        /// Liefert den Geburtsort der Person.
        /// </summary>
        string Geburtsort { get; set; }
    }
}
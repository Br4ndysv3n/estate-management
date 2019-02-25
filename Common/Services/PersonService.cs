using Common.Models.Person;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Common.Services
{
    /// <summary>
    /// Die Klasse stellt Methoden bereit, um Personen aus der DB zu lesen oder sie zu speichern.
    /// </summary>
    public class PersonService
    {
        private string SQL_CONNECTION_STRING = "Data Source=ImmVer.sqlite;Version=3;New=True;";

        private string SQL_SELECT_ALL = "SELECT * FROM Person";
        private string SQL_CLEAR = "DELETE FROM Person";        
        private string SQL_INSERT = "INSERT INTO Person (Titel, Anrede, Vorname, Nachname, Geburtsdatum, Geburtsort) VALUES (@titel, @anrede, @vorname, @nachname, @geburtsdatum, @geburtsort)";
        private string SQL_UPDATE = "UPDATE Person SET Titel = @titel, Anrede = @anrede, Vorname = @vorname, Nachname = @nachname, Geburtsdatum = @geburtsdatum, Geburtsort = @geburtsort WHERE ID = @id";
        private string SQL_LAST_UPDATED = "SELECT last_insert_rowid()";
        private string SQL_GET_BY_ID = "SELECT * FROM Person WHERE Id = @id";
        private string SQL_DELETE_BY_ID = "DELETE FROM Person WHERE Id = @id";

        /// <summary>
        /// Fügt der DB eine neue Person hinzu oder aktualisiert diese.
        /// </summary>
        /// <param name="person">Die zu speichernde Person.</param>
        public IPerson InsertOrUpdate(IPerson person)
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            var statement = new SQLiteCommand(person.Id == 0 ? SQL_INSERT : SQL_UPDATE, connection);
            statement.Parameters.Add(new SQLiteParameter("@titel", person.Titel));
            statement.Parameters.Add(new SQLiteParameter("@anrede", (int)person.Anrede));
            statement.Parameters.Add(new SQLiteParameter("@vorname", person.Vorname));
            statement.Parameters.Add(new SQLiteParameter("@nachname", person.Nachname));
            statement.Parameters.Add(new SQLiteParameter("@geburtsdatum", person.Geburtsdatum));
            statement.Parameters.Add(new SQLiteParameter("@geburtsort", person.Geburtsort));

            statement.ExecuteNonQuery();
            if (person.Id == 0)
            {
                person.Id = GetLastRowId(connection);
            }

            connection.Close();

            return person;
        }

        internal int GetLastRowId(SQLiteConnection cnn)
        {
            using (SQLiteCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandText = SQL_LAST_UPDATED;
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// Liest die Daten zu einer bestimmten Person aus der DB.
        /// </summary>
        /// <param name="id">Die Id der zu ladenden Person.</param>
        /// <returns>Eine <see cref="IPerson"/> mit den Daten aus der DB.</returns>
        public IPerson GetPersonById(int id)
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            IPerson person = new Person();

            var statement = new SQLiteCommand(SQL_GET_BY_ID, connection);
            statement.Parameters.Add(new SQLiteParameter("@id", id));

            SQLiteDataReader reader = statement.ExecuteReader();
            while (reader.Read())
            {
                person = PersonFactory.CreatePerson(reader.GetInt32(0), reader.GetString(1), (EnumAnrede)reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetDateTime(5), reader.GetString(6));
            }

            connection.Close();

            return person;
        }

        /// <summary>
        /// Liefert alle Personen aus der DB.
        /// </summary>
        /// <returns>Eine Liste der Personen.</returns>
        public List<IPerson> GetPersons()
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            var persons = new List<IPerson>();
            var statement = new SQLiteCommand(SQL_SELECT_ALL, connection);

            SQLiteDataReader reader = statement.ExecuteReader();
            while (reader.Read())
            {
                var person = PersonFactory.CreatePerson(reader.GetInt32(0), reader.GetString(1),
                    (EnumAnrede)Enum.ToObject(typeof(EnumAnrede), reader.GetInt32(2)), reader.GetString(3), reader.GetString(4),
                    reader.GetDateTime(5), reader.GetString(6));
                persons.Add(person);
            }

            connection.Close();

            return persons;
        }

        public void Clear()
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            var statement = new SQLiteCommand(SQL_CLEAR, connection);

            statement.ExecuteNonQuery();

            connection.Close();
        }
    }
}

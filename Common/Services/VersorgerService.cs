using Common.Models.Person;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Common.Models;
using Common.Models.Versorger;

namespace Common.Services
{
    /// <summary>
    /// Die Klasse stellt Methoden bereit, um Personen aus der DB zu lesen oder sie zu speichern.
    /// </summary>
    public class VersorgerService
    {
        private string SQL_CONNECTION_STRING = "Data Source=ImmVer.sqlite;Version=3;New=True;";

        private string SQL_SELECT_ALL_BY_VERSORGERTYP = "SELECT * FROM Versorger WHERE VersorgerTyp = @versorgertyp";
        private string SQL_CLEAR = "DELETE FROM Versorger";        
        private string SQL_INSERT = "INSERT INTO Versorger (VersorgerTyp, Name, Strasse, Hausnummer, Plz, Ort) VALUES (@versorgerTyp, @name, @strasse, @hausnummer, @plz, @ort)";
        private string SQL_UPDATE = "UPDATE Versorger SET VersorgerTyp = @versorgertyp, Name = @name, Strasse = @strasse, Hausnummer = @hausnummer, Plz = @plz, Ort = @ort WHERE ID = @id";
        private string SQL_LAST_UPDATED = "SELECT last_insert_rowid()";
        private string SQL_GET_BY_ID = "SELECT * FROM Versorger WHERE Id = @id";
        private string SQL_DELETE_BY_ID = "DELETE FROM Versorger WHERE Id = @id";

        /// <summary>
        /// Fügt der DB eine neue Person hinzu oder aktualisiert diese.
        /// </summary>
        /// <param name="versorger">Die zu speichernde Person.</param>
        public IVersorger InsertOrUpdate(IVersorger versorger)
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            var statement = new SQLiteCommand(versorger.Id == 0 ? SQL_INSERT : SQL_UPDATE, connection);
            statement.Parameters.Add(new SQLiteParameter("@versorgertyp", (int)versorger.StammdatenTyp));
            statement.Parameters.Add(new SQLiteParameter("@name", versorger.Name));
            statement.Parameters.Add(new SQLiteParameter("@strasse", versorger.Strasse));
            statement.Parameters.Add(new SQLiteParameter("@hausnummer", versorger.Hausnummer));
            statement.Parameters.Add(new SQLiteParameter("@plz", versorger.Plz));
            statement.Parameters.Add(new SQLiteParameter("@ort", versorger.Ort));

            statement.ExecuteNonQuery();
            if (versorger.Id == 0)
            {
                versorger.Id = GetLastRowId(connection);
            }

            connection.Close();

            return versorger;
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
        public IVersorger GetById(int id)
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            IVersorger versorger = null;

            var statement = new SQLiteCommand(SQL_GET_BY_ID, connection);
            statement.Parameters.Add(new SQLiteParameter("@id", id));

            SQLiteDataReader reader = statement.ExecuteReader();
            while (reader.Read())
            {
                versorger = VersorgerFactory.Create(reader.GetInt32(0), (EnumStammdatenTyp)reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
            }

            connection.Close();

            return versorger;
        }

        /// <summary>
        /// Liefert alle Personen aus der DB.
        /// </summary>
        /// <returns>Eine Liste der Personen.</returns>
        public List<IVersorger> GetAllByVersorgertyp(EnumStammdatenTyp versorgerTyp)
        {
            var connection = new SQLiteConnection(SQL_CONNECTION_STRING);
            connection.Open();

            var versorger = new List<IVersorger>();
            var statement = new SQLiteCommand(SQL_SELECT_ALL_BY_VERSORGERTYP, connection);
            statement.Parameters.Add(new SQLiteParameter("@versorgertyp", (int)versorgerTyp));

            SQLiteDataReader reader = statement.ExecuteReader();
            while (reader.Read())
            {
                versorger.Add(VersorgerFactory.Create(reader.GetInt32(0), (EnumStammdatenTyp)reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6)));
            }

            connection.Close();

            return versorger;
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

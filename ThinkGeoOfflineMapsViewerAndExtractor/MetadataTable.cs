using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;

namespace MBTilesExtractor
{
    public class MetadataTable
    {
        private SqliteConnection connection;
        public List<MetadataEntry> Entries { get; }
        private string TableName { get; set; }

        public static readonly string MetadataValueColumnName = "value";
        public static readonly string MetadataNameColumnName = "name";

        public MetadataTable(SqliteConnection connection)
        {
            this.connection = connection;
            Entries = new List<MetadataEntry>();
            TableName = "metadata";
        }

        public bool Insert(IEnumerable<MetadataEntry> entries)
        {
            bool result = true;
            string insertSqlStatement = $"INSERT INTO {TableName} (name, value) VALUES (@name, @value);";
            SqliteCommand command = new SqliteCommand(insertSqlStatement, connection);
            command.Parameters.Add("@name", SqliteType.Text);
            command.Parameters.Add("@value", SqliteType.Text);
            command.Prepare();
            IDbTransaction dbTransaction = connection.BeginTransaction();
            command.Transaction = dbTransaction as SqliteTransaction;
            try
            {
                foreach (MetadataEntry entry in entries)
                {
                    command.Parameters["@name"].Value = entry.Name;
                    command.Parameters["@value"].Value = entry.Value;
                    command.ExecuteNonQuery();
                }

                dbTransaction.Commit();
            }
            catch
            {
                dbTransaction.Rollback();
                result = false;
            }

            return result;
        }

        public void ReadAllEntries()
        {
            SqliteDataReader dataReader = null;
            Entries.Clear();

            SqliteCommand command = new SqliteCommand()
            {
                Connection = connection,
            };
            command.CommandText = $"SELECT * FROM {TableName}";
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string name = (string)dataReader["name"];
                string value = (string)dataReader["value"];
                Entries.Add(new MetadataEntry() { Name = name, Value = value });
            }
        }
    }
}

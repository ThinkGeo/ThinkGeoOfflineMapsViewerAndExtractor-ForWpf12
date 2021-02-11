using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace MBTilesExtractor
{
    public class TilesTable
    {
        private SqliteConnection connection;
        public List<TilesEntry> Entries { get; }
        public int Cursor { get; set; }
        public string TableName { get; set; }

        public static readonly string ZoomLevelColumnName = "zoom_level";
        public static readonly string TileColColumnName = "tile_column";
        public static readonly string TileRowColumnName = "tile_row";
        public static readonly string TileIdColumnName = "Id";
        public static readonly string TileDataColumnName = "tile_data";

        public TilesTable(SqliteConnection connection)
        {
            this.connection = connection;
            Entries = new List<TilesEntry>();
            Cursor = 0;
            TableName = "tiles";
        }

        public bool Insert(IEnumerable<TilesEntry> entries)
        {
            bool result = true;

            string insertSqlStatement = $"INSERT INTO {TableName} ({ZoomLevelColumnName},{TileColColumnName},{TileRowColumnName},{TileIdColumnName},{TileDataColumnName}) VALUES (@{ZoomLevelColumnName}, @{TileColColumnName}, @{TileRowColumnName}, @{TileIdColumnName}, @{TileDataColumnName});";

            SqliteCommand command = new SqliteCommand(insertSqlStatement, connection);
            command.Parameters.Add($"@{ZoomLevelColumnName}", SqliteType.Integer);
            command.Parameters.Add($"@{TileColColumnName}", SqliteType.Integer);
            command.Parameters.Add($"@{TileRowColumnName}", SqliteType.Integer);
            command.Parameters.Add($"@{TileIdColumnName}", SqliteType.Text);
            command.Parameters.Add($"@{TileDataColumnName}", SqliteType.Blob);
            command.Prepare();
            IDbTransaction dbTransaction = connection.BeginTransaction();
            command.Transaction = dbTransaction as SqliteTransaction;
            try
            {
                int ExecutedQueryCount = 0;
                foreach (TilesEntry entry in entries)
                {
                    command.Parameters[$"@{ZoomLevelColumnName}"].Value = ParseValue(entry.ZoomLevel);
                    command.Parameters[$"@{TileColColumnName}"].Value = ParseValue(entry.TileColumn);
                    command.Parameters[$"@{TileRowColumnName}"].Value = ParseValue(entry.TileRow);
                    command.Parameters[$"@{TileIdColumnName}"].Value = ParseValue(entry.TileId);
                    command.Parameters[$"@{TileDataColumnName}"].Value = entry.TileData;
                    command.ExecuteNonQuery();
                    ExecutedQueryCount++;
                    if (ExecutedQueryCount % 1000 == 0)
                    {
                        dbTransaction.Commit();
                        dbTransaction = connection.BeginTransaction();
                        command.Transaction = dbTransaction as SqliteTransaction;
                    }
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

        private object ParseValue(object value)
        {
            if (value == null)
                return DBNull.Value;

            return value;
        }

        public List<TilesEntry> Query(string sqlString)
        {
            List<TilesEntry> result = new List<TilesEntry>();

            SqliteCommand command = new SqliteCommand()
            {
                Connection = connection,
            };
            command.CommandText = sqlString;
            SqliteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                TilesEntry newEntry = new TilesEntry();
                newEntry.ZoomLevel = (long)dataReader[ZoomLevelColumnName];
                newEntry.TileColumn = (long)dataReader[TileColColumnName];
                newEntry.TileRow = (long)dataReader[TileRowColumnName];
                newEntry.TileId = (long)dataReader[TileIdColumnName];
                newEntry.TileData = (byte[])dataReader[TileDataColumnName];

                result.Add(newEntry);
                Cursor++;
            }

            return result;
        }
    }
}
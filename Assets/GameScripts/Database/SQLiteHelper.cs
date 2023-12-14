using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

// Credit: 
// https://github.com/rizasif/sqlite-unity-plugin-example

namespace DataBank
{
    public class SQLiteHelper
    {
        private const string Tag = "SQLiteHelper:\t";

        private const string database_name = "Roller_db";

        public string db_connection_string;
        public IDbConnection db_connection;

        public SQLiteHelper()
        {
            db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
            Debug.Log("db_connection_string" + db_connection_string);
            db_connection = new SqliteConnection(db_connection_string);
            db_connection.Open();
        }

        ~SQLiteHelper()
        {
            db_connection.Close();
        }

        // virtual functions
        public virtual IDataReader getDataById(int id)
        {
            // Override in sub-class for tables that use an integer ID as the PK
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getDataByString(string str)
        {
            // Override in sub-class for tables that use an string as the PK
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual void deleteDataById(int id)
        {
            // Override in sub-class for tables that use an integer ID as the PK
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual void deleteDataByString(string id)
        {
            // Override in sub-class for tables that use an string as the PK
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual IDataReader getAllData()
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual void deleteAllData()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual void dropTable()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader getNumOfRows()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        //helper functions
        public IDbCommand getDbCommand()
        {
            return db_connection.CreateCommand();
        }

        public IDataReader getAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        public void deleteAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DELETE FROM " + table_name;
            dbcmd.ExecuteNonQuery();
        }

        public void dropTable(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            dbcmd.ExecuteNonQuery();
        }

        public IDataReader getNumOfRows(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        // SQLite doesn't have a boolean value, it uses an int of 0 or 1
        protected int intForBool(bool value)
        {
            return value ? 1 : 0;
        }

        protected bool boolForInt(int value)
        {
            return value == 0 ? false : true;
        }

        public void close()
        {
            db_connection.Close();
        }
    }
}
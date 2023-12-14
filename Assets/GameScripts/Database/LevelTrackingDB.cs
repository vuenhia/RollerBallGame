using UnityEngine;
using System.Data;

namespace DataBank
{
    public class LevelTrackingDB : SQLiteHelper
    {
        private const string Tag = "LevelTrackingDB.";

        private const string TABLE_Name = "LevelTracking";
        private const string FIELD_LevelName = "LevelName";
        private const string FIELD_Star1 = "Star1";
        private const string FIELD_Star2 = "Star2";
        private const string FIELD_Star3 = "Star3";
        private const string FIELD_BestTime = "BestTime";

        private string[] COLUMNS = new string[] { FIELD_LevelName, FIELD_Star1, FIELD_Star2, FIELD_Star3, FIELD_BestTime };

        public LevelTrackingDB() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_Name + " ( " +
                        FIELD_LevelName + " TEXT PRIMARY KEY, " +
                        FIELD_Star1 + " INTEGER, " +
                        FIELD_Star2 + " INTEGER, " +
                        FIELD_Star3 + " INTEGER, " +
                        FIELD_BestTime + " REAL )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(LevelTrackingEntity levelData)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_Name
                + " ( "
                + FIELD_LevelName + ", "
                + FIELD_Star1 + ", "
                + FIELD_Star2 + ", "
                + FIELD_Star3 + ", "
                + FIELD_BestTime + " )"

                + " VALUES ( '"
                + levelData.levelName + "', "
                + base.intForBool(levelData.star1) + ", "
                + base.intForBool(levelData.star2) + ", "
                + base.intForBool(levelData.star3) + ", "
                + levelData.bestTime + " )";
            Debug.Log(Tag + "AddData: " + dbcmd.CommandText);
            dbcmd.ExecuteNonQuery();
        }

        public void updateData(LevelTrackingEntity levelData)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_Name
                + " SET "
                + FIELD_Star1 + " = " + base.intForBool(levelData.star1) + ", "
                + FIELD_Star2 + " = " + base.intForBool(levelData.star2) + ", "
                + FIELD_Star3 + " = " + base.intForBool(levelData.star3) + ", "
                + FIELD_BestTime + " = " + levelData.bestTime
                + " WHERE " + FIELD_LevelName + " = '" + levelData.levelName + "'";
            Debug.Log(Tag + "UpdateData: " + dbcmd.CommandText);
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataByString(string levelName)
        {
            Debug.Log(Tag + "getDataByString: " + levelName);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_Name + " WHERE " + FIELD_LevelName + " = '" + levelName + "'";
            return dbcmd.ExecuteReader();
        }

        public LevelTrackingEntity getDataForLevel(string levelName)
        {
            System.Data.IDataReader reader = getDataByString(levelName);
            if (reader.Read())
            {
                return new LevelTrackingEntity(reader.GetString(0), reader.GetInt16(1)==1, reader.GetInt16(2)==1, reader.GetInt16(3)==1, reader.GetFloat(4));
            }
            return null;
        }

        public override void deleteDataByString(string levelName)
        {
            Debug.Log(Tag + "deleteDataByString: " + levelName);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_Name + " WHERE " + FIELD_LevelName + " = '" + levelName + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteAllData()
        {
            Debug.Log(Tag + "deleteAllData");

            base.deleteAllData(TABLE_Name);
        }

        public override void dropTable()
        {
            Debug.Log(Tag + "dropTable");

            base.dropTable(TABLE_Name);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_Name);
        }
    }
}

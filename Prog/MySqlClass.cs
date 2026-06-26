using MySql.Data.MySqlClient;
using System.Data;

internal class MySqlClass
{
    private static MySqlConnection sqlCon = new();
    private static MySqlCommand sqlCom = new();
    private static MySqlDataReader sqlRd;
    public static DataTable sqlDt = new();
    private static string sqlQuery;
    private static MySqlDataAdapter sqlDataAdap = new();
    private static DataSet ds = new DataSet();


    private static string connectionString =
    "server=localhost;" +
    "database=tasks;" +
    "uid=root;" +
    "pwd=1234567899;";
    public static void AddNewTask(string title, string desc, DateOnly? remDate, TimeOnly? remTime)
    {
        sqlCom.Connection = sqlCon;
        string nl = "NULL";

        if (remDate == null && remTime == null)
        {
            sqlCom.CommandText = $"INSERT INTO task (Title, Description, ReminderDate, ReminderTime) VALUES ('{title}','{desc}',{nl},{nl})";
        }
        else if (remDate == null)
        {
            sqlCom.CommandText = $"INSERT INTO task (Title, Description, ReminderDate, ReminderTime) VALUES ('{title}','{desc}',{nl},'{remTime}')";
        }
        else if (remTime == null)
        {
            sqlCom.CommandText = $"INSERT INTO task (Title, Description, ReminderDate, ReminderTime) VALUES ('{title}','{desc}','{remDate}',{nl})";
        }
        else
        {
            sqlCom.CommandText = $"INSERT INTO task (Title, Description, ReminderDate, ReminderTime) VALUES ('{title}','{desc}','{remDate}','{remTime}')";
        }

        sqlCom.ExecuteNonQuery();

        ShowTasks();
    }

    public static int DeleteTask(string iden)
    {
        sqlCom.Connection = sqlCon;
        sqlCom.CommandText = $"DELETE FROM task WHERE {iden} LIMIT 1";
        int aff = sqlCom.ExecuteNonQuery();
        sqlDt.Load(sqlRd);

        ShowTasks();
        return aff;
    }

    public static DataTable getData()
    {
        return sqlDt;
    }
    public static void ShowTasks()
    {
        sqlCom.Connection = sqlCon;
        sqlCom.CommandText = "SELECT * FROM task";
        sqlRd = sqlCom.ExecuteReader();
        sqlDt.Clear();
        sqlDt.Load(sqlRd);

    }

    public static int UpdateTaskStatus(string set, string iden)
    {
        sqlCom.Connection = sqlCon;
        sqlCom.CommandText = $"UPDATE task SET Status = '{set}' WHERE {iden}";
        int aff = sqlCom.ExecuteNonQuery();

        ShowTasks();
        return aff;
    }

    public static void openCon()
    {
        sqlCon.ConnectionString = connectionString;
        sqlCon.Open();
    }
    public static void closeCon()
    {
        sqlRd.Close();
        sqlCon.Close();
    }
}
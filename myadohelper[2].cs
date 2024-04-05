using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for MyAdoHelper
/// פעולות עזר לשימוש במסד נתונים  מסוג 
/// SQL SERVER
///  App_Data המסד ממוקם בתקיה 
/// </summary>

public class MyAdoHelper
{


    public static SqlConnection ConnectToDb(string fileName)
    {
        string path = HttpContext.Current.Server.MapPath("../App_Data/");//מיקום מסד בפורוייקט
        path += fileName;
        //string path = HttpContext.Current.Server.MapPath("App_Data/" + fileName);//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\GOLD.EDUEILAT.000\source\repos\WebApplication5\WebApplication5\App_Data\Database1.mdf;Integrated Security=True
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" +
                             path +
                             ";Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        return conn;

    }

    public static SqlConnection ConnectToDbAdmins(string fileName)
    {
        string path = HttpContext.Current.Server.MapPath("App_Data/");//מיקום מסד בפורוייקט
        path += fileName;
        //string path = HttpContext.Current.Server.MapPath("App_Data/" + fileName);//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
        //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\GOLD.EDUEILAT.000\source\repos\WebApplication5\WebApplication5\App_Data\Database1.mdf;Integrated Security=True
        string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" +
                             path +
                             ";Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        return conn;

    }

    public static void DoQuery(string fileName, string sql)
    {

        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        com.ExecuteNonQuery();
        com.Dispose();
        conn.Close();

    }

    public static void DoQueryDel(string fileName, string sql)
    {

        SqlConnection conn = ConnectToDbAdmins(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        com.ExecuteNonQuery();
        com.Dispose();
        conn.Close();

    }



    public static int RowsAffected(string fileName, string sql)
    {
        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        int rowsA = com.ExecuteNonQuery();        
        conn.Close();        
        return rowsA;
    }

  
    public static bool IsExist(string fileName, string sql)
    {

        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        SqlDataReader data = com.ExecuteReader();
        bool found;
        found = (bool)data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
        conn.Close();
        return found;

    }

    public static bool IsExistDel(string fileName, string sql)
    {

        SqlConnection conn = ConnectToDbAdmins(fileName);
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        SqlDataReader data = com.ExecuteReader();
        bool found;
        found = (bool)data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
        conn.Close();
        return found;

    }


    public static DataTable ExecuteDataTable(string fileName, string sql)
    {
        SqlConnection conn = ConnectToDbAdmins(fileName);
        conn.Open();
        SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
        DataTable dt = new DataTable();
        tableAdapter.Fill(dt);
        return dt;
    }


    public static void ExecuteNonQuery(string fileName, string sql)
    {
        SqlConnection conn = ConnectToDb(fileName);
        conn.Open();
        SqlCommand command = new SqlCommand(sql, conn);
        command.ExecuteNonQuery();
        conn.Close();
    }

    public static string printDataTable(string fileName, string sql)
    {


        DataTable dt = ExecuteDataTable(fileName, sql);

        string printStr = "<table border='1'>";

        foreach (DataRow row in dt.Rows)
        {
            printStr += "<tr>";
            foreach (object myItemArray in row.ItemArray)
            {
                printStr += "<td>" + myItemArray.ToString() + "</td>";
            }
            printStr += "</tr>";
        }
        printStr += "</table>";

        return printStr;
    }



}


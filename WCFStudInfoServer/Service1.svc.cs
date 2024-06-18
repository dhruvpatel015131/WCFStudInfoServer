using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFStudInfoServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        static string conStr = ConfigurationManager.ConnectionStrings["StudConStr"].ToString();
        SqlConnection conn = new SqlConnection(conStr);
        SqlCommand cmd = null;

        SqlDataReader dr = null;
        DataTable dt = null;

        public DataTable ShowAllRecords()
        {
             try
            {
                cmd = new SqlCommand("SELECT * FROM stud", conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                dt = new DataTable("studTable");
                dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch(Exception ex)
            {
               // throw ex("Exception! " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
                return dt;
        }


        public string InsertRecord(Student stud1)
        {
            string msg;
            try
            {
                cmd = new SqlCommand("INSERT INTO stud VALUES (@id,@nm,@add)", conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@id", stud1.Id);
                cmd.Parameters.AddWithValue("@nm", stud1.Name);
                cmd.Parameters.AddWithValue("@add", stud1.Address);

                int r = cmd.ExecuteNonQuery();
                if (r != 0)
                {
                    msg = "Record Inserted Sucessfully";
                }
                else
                {
                    msg = "Record Not Inserted Sucessfully";
                }

            }
            catch (Exception ex)
            {
                msg = "Exception " + ex.Message;
            }
            finally
            {
                conn.Close();

            }
            return msg;

        }


    }
}

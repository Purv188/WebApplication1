using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentsController : ApiController
    {
        private string _conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\College\\Sem-2 MSCIT\\ASP\\WebApplication1\\WebApplication1\\App_Data\\StudentDB.mdf\";Integrated Security=True";

        private SqlConnection _connection;

        public StudentsController()
        {
            _connection = new SqlConnection(_conString);
        }

        public List<Student> GetAllData()
        {
            List<Student> list = new List<Student>();
            string que = "select * from student";
            SqlCommand cmd = new SqlCommand(que,_connection);
            SqlDataAdapter adapter=new SqlDataAdapter(cmd);
            DataSet ds=new DataSet();
            adapter.Fill(ds);
            DataTable dt=ds.Tables[0];

            foreach(DataRow dr in dt.Rows)
            {
                Student stud = new Student()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Age = Convert.ToInt32(dr["Age"])
                };
                list.Add(stud);
            }

            return list;
        }
    }
}

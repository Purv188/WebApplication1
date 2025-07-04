using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private string _conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"E:\\College\\Sem-2 MSCIT\\ASP\\WebApplication1\\WebApplication1\\App_Data\\StudentDB.mdf\";Integrated Security=True";

        private SqlConnection _connection;

        public HomeController()
        {
            _connection = new SqlConnection(_conString);
        }
        public ActionResult Index()
        {
            List<Student> list = new List<Student>();
            string que = "select * from student";
            SqlCommand cmd = new SqlCommand(que, _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Student stud = new Student()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = Convert.ToString(dr["Name"]),
                    Age = Convert.ToInt32(dr["Age"])
                };
                list.Add(stud);
            }

            return View(list);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Student student)
        {
            string que = "Insert into student(Name,Age) values(@Name,@Age)";
            using (SqlCommand cmd = new SqlCommand(que, _connection))
            {
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int Id)
        {
            string que = "delete from student where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(que, _connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Update(Student student)
        {
            return View(student);
        }

        [HttpPost]
        public ActionResult Update(int Id, string Name, int Age)
        {
            string que = "update student set Name=@Name,Age=@Age where Id=@Id";

            using (SqlCommand cmd = new SqlCommand(que, _connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Age", Age);

                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            return RedirectToAction("Index");
        }

    }
}

using Dapper;
using LS_DapperMvcCode.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LS_DapperMvcCode.Controllers
{
    public class DeveloperController : Controller
    {
        string LS_DataUrl = ConfigurationManager.ConnectionStrings["LS_DataUrl"].ConnectionString;
        string LS_DataUrlNew = ConfigurationManager.AppSettings["LS_DataUrl"].ToString();
        public ActionResult Index()
        {
            string sql = "select * from  LS_Student";
            using (IDbConnection conn = new SqlConnection(LS_DataUrlNew))
            {
                var result = conn.Query(sql).ToList();
                string str = JsonConvert.SerializeObject(result);
                List<StudentModel> list = JsonConvert.DeserializeObject<List<StudentModel>>(str);
                return View(list);
            }
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(StudentModel studentModel)
        {
            string sql = "insert into LS_Student values(@Name,@Age,@Gender)";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Name", studentModel.Name);
            dynamicParameters.Add("@Age", studentModel.Age);
            dynamicParameters.Add("@Gender", studentModel.Gender);
            using (IDbConnection conn = new SqlConnection(LS_DataUrl))
            {
                var result = conn.Execute(sql, dynamicParameters);
                if (result == 1)
                {
                    return Redirect("/Developer/Index");
                }
                else 
                {
                    return Content("添加失败");
                }
            }
        }
        public ActionResult Delete(int Id)
        {
            string sql = "delete from LS_Student where Id=@Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Id);
            using (IDbConnection conn = new SqlConnection(LS_DataUrl))
            {
                var result = conn.Execute(sql, dynamicParameters);
                if (result > 0)
                {
                    return Redirect("/Developer/Index");
                }
                else
                {
                    return Content("删除失败");
                }
            }
        }
        public ActionResult Upd(int Id)
        {
            string sql = "select * from LS_Student where Id =@Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", Id);
            using (IDbConnection conn = new SqlConnection(LS_DataUrl))
            {
                var student = conn.Query<StudentModel>(sql, dynamicParameters).FirstOrDefault();
                return View(student);
            }
        }
        [HttpPost]
        public ActionResult Upd(StudentModel studentModel)
        {
            string sql = "Update LS_Student Set Name=@Name,Age=@Age,@Gender=Gender where Id=@Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Id", studentModel.Id);
            dynamicParameters.Add("@Name", studentModel.Name);
            dynamicParameters.Add("@Age", studentModel.Age);
            dynamicParameters.Add("@Gender", studentModel.Gender);
            using (IDbConnection conn = new SqlConnection(LS_DataUrl))
            {
                var result = conn.Execute(sql, dynamicParameters);
                if (result == 1)
                {
                    return Redirect("/Developer/Index");
                }
                else
                {
                    return Content("修改失败");
                }
            }
        }
    }
}

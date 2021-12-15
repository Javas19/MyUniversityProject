using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using DigeraitMIS.Models.Login;
using DigeraitMIS.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using DigeraitMIS.Areas.Pupil.Models;

namespace DigeraitMIS.Controllers
{
    public class AccountController : Controller
    {

        private readonly IConfiguration configuration;

        public AccountController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpGet]
        public IActionResult logout()
        {

            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("surname");

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            string connString = configuration.GetConnectionString("connString");

            SqlConnection dbConn = new SqlConnection(connString);

            dbConn.Open();

            SqlCommand dbComm = new SqlCommand("sp_GetProvinces", dbConn);
            dbComm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
            DataTable dt = new DataTable();
            dbAdapter.Fill(dt);
            dbConn.Close();

            List<Province> provinces = new List<Province>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Province province = new Province();
                province.ProvinceID = Convert.ToInt32(dt.Rows[i]["ProvinceID"]);
                province.Name = dt.Rows[i]["Name"].ToString();
                provinces.Add(province);
            }

            ViewBag.Provinces = provinces.ToList();

            // for Cities
            SqlConnection dbConn1 = new SqlConnection(connString);

            dbConn1.Open();

            SqlCommand dbComm1 = new SqlCommand("sp_GetCities", dbConn1);
            dbComm1.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dbAdapter1 = new SqlDataAdapter(dbComm1);
            DataTable dt1 = new DataTable();
            dbAdapter1.Fill(dt1);
            dbConn1.Close();

            List<City> cities = new List<City>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                City city = new City();
                city.CityID = Convert.ToInt32(dt1.Rows[i]["CityID"]);
                city.CityName = dt1.Rows[i]["CityName"].ToString();

                cities.Add(city);
            }

            ViewBag.Cities = cities.ToList();

            //For Regions
            SqlConnection dbConn2 = new SqlConnection(connString);

            dbConn2.Open();

            SqlCommand dbComm2 = new SqlCommand("sp_GetRegions", dbConn2);
            dbComm2.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dbAdapter2 = new SqlDataAdapter(dbComm2);
            DataTable dt2 = new DataTable();
            dbAdapter2.Fill(dt2);
            dbConn2.Close();

            List<Region> regions = new List<Region>();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                Region region = new Region();
                region.RegionId = Convert.ToInt32(dt2.Rows[i]["RegionID"]);
                region.Name = dt2.Rows[i]["RegionName"].ToString();
                regions.Add(region);
            }

            ViewBag.Regions = regions.ToList();

            //for centres
            SqlConnection dbConn3 = new SqlConnection(connString);

            dbConn3.Open();

            SqlCommand dbComm3 = new SqlCommand("sp_GetCentres", dbConn3);
            dbComm3.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dbAdapter3 = new SqlDataAdapter(dbComm3);
            DataTable dt3 = new DataTable();
            dbAdapter3.Fill(dt3);
            dbConn3.Close();

            List<CentreProfile> centres = new List<CentreProfile>();
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                CentreProfile centre = new CentreProfile();
                centre.CentreNo = Convert.ToInt32(dt3.Rows[i]["CentreNo"]);
                centre.Name = dt3.Rows[i]["Name"].ToString();
                centres.Add(centre);
            }

            ViewBag.Centres = centres.ToList();

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Pupil pupil)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();
                //string sql = "INSERT INTO Client (Name,Surname,Email,Password,UserType) VALUES (@name,@surname,@email,@password,@userType)";
                SqlCommand dbComm = new SqlCommand("sp_RegisterPupil", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                pupil.UserStatus = "Active";
                pupil.UserType = "Pupil";

                dbComm.Parameters.AddWithValue("@FirstName", pupil.FirstName);
                dbComm.Parameters.AddWithValue("@Surname", pupil.Surname);
                dbComm.Parameters.AddWithValue("@Email", pupil.EmailAddress);
                dbComm.Parameters.AddWithValue("@UserType", pupil.UserType);
                dbComm.Parameters.AddWithValue("@Password", pupil.Password);
                dbComm.Parameters.AddWithValue("@Status", pupil.UserStatus);
                dbComm.Parameters.AddWithValue("@IdNumber", pupil.IDnumber);
                dbComm.Parameters.AddWithValue("@DateOfBirth", pupil.DateOfBirth);
                dbComm.Parameters.AddWithValue("@Guardian", pupil.GuardianName);
                dbComm.Parameters.AddWithValue("@Address", pupil.Address);
                dbComm.Parameters.AddWithValue("@Centre", pupil.CentreNo);
                dbComm.Parameters.AddWithValue("@Region", pupil.RegionID);


                int x = dbComm.ExecuteNonQuery();

                dbConn.Close();

                return RedirectToAction("Login", "Account");
            }
            else
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_GetProvinces", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
                DataTable dt = new DataTable();
                dbAdapter.Fill(dt);
                dbConn.Close();

                List<Province> provinces = new List<Province>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Province province = new Province();
                    province.ProvinceID = Convert.ToInt32(dt.Rows[i]["ProvinceID"]);
                    province.Name = dt.Rows[i]["Name"].ToString();
                    provinces.Add(province);
                }

                ViewBag.Provinces = provinces.ToList();

                // for Cities
                SqlConnection dbConn1 = new SqlConnection(connString);

                dbConn1.Open();

                SqlCommand dbComm1 = new SqlCommand("sp_GetCities", dbConn1);
                dbComm1.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dbAdapter1 = new SqlDataAdapter(dbComm1);
                DataTable dt1 = new DataTable();
                dbAdapter1.Fill(dt1);
                dbConn1.Close();

                List<City> cities = new List<City>();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    City city = new City();
                    city.CityID = Convert.ToInt32(dt1.Rows[i]["CityID"]);
                    city.CityName = dt1.Rows[i]["CityName"].ToString();

                    cities.Add(city);
                }

                ViewBag.Cities = cities.ToList();

                //For Regions
                SqlConnection dbConn2 = new SqlConnection(connString);

                dbConn2.Open();

                SqlCommand dbComm2 = new SqlCommand("sp_GetRegions", dbConn2);
                dbComm2.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dbAdapter2 = new SqlDataAdapter(dbComm2);
                DataTable dt2 = new DataTable();
                dbAdapter2.Fill(dt2);
                dbConn2.Close();

                List<Region> regions = new List<Region>();
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Region region = new Region();
                    region.RegionId = Convert.ToInt32(dt2.Rows[i]["RegionID"]);
                    region.Name = dt2.Rows[i]["RegionName"].ToString();
                    regions.Add(region);
                }

                ViewBag.Regions = regions.ToList();

                //for centres
                SqlConnection dbConn3 = new SqlConnection(connString);

                dbConn3.Open();

                SqlCommand dbComm3 = new SqlCommand("sp_GetCentres", dbConn3);
                dbComm3.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dbAdapter3 = new SqlDataAdapter(dbComm3);
                DataTable dt3 = new DataTable();
                dbAdapter3.Fill(dt3);
                dbConn3.Close();

                List<CentreProfile> centres = new List<CentreProfile>();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    CentreProfile centre = new CentreProfile();
                    centre.CentreNo = Convert.ToInt32(dt3.Rows[i]["CentreNo"]);
                    centre.Name = dt3.Rows[i]["Name"].ToString();
                    centres.Add(centre);
                }

                ViewBag.Centres = centres.ToList();

                return View(pupil);
            }


        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string connString = configuration.GetConnectionString("connString");

                SqlConnection dbConn = new SqlConnection(connString);

                dbConn.Open();

                SqlCommand dbComm = new SqlCommand("sp_Login", dbConn);
                dbComm.CommandType = CommandType.StoredProcedure;

                dbComm.Parameters.AddWithValue("@Email", model.Email);
                dbComm.Parameters.AddWithValue("@password", model.Password);

                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbComm);
                DataTable dt = new DataTable();
                dbAdapter.Fill(dt);
                dbConn.Close();

                


                if (dt.Rows.Count > 0)
                {
                    model.userID = int.Parse(dt.Rows[0]["UserID"].ToString());

                    int id = int.Parse(dt.Rows[0]["UserID"].ToString());
                    string name = dt.Rows[0]["FirstName"].ToString();
                    string surname = dt.Rows[0]["Surname"].ToString();

                    if (dt.Rows[0]["UserType"].ToString() == "Pupil")
                    {
                        HttpContext.Session.SetInt32("id", id);
                        HttpContext.Session.SetString("name", name);
                        HttpContext.Session.SetString("surname", surname);
                        string connString1 = configuration.GetConnectionString("connString");

                        SqlConnection dbConn1 = new SqlConnection(connString1);
                        ReportViewModel model1 = new ReportViewModel();
                       

                        dbConn1.Open();

                        SqlCommand dbComm1 = new SqlCommand("sp_GetPupilCentre ", dbConn1);
                        dbComm1.CommandType = CommandType.StoredProcedure;
                        int id1 = (int)HttpContext.Session.GetInt32("id");
                        dbComm1.Parameters.AddWithValue("@id", id1);
                        SqlDataAdapter dbAdapter1 = new SqlDataAdapter(dbComm1);
                        DataTable dt1 = new DataTable();
                        dbAdapter1.Fill(dt1);

                        dbConn1.Close();

                        string centreName = "";

                        centreName = dt1.Rows[0]["Name"].ToString();
                        

                        HttpContext.Session.SetString("Centre", centreName);



                        return RedirectToAction("Index", "Home", new { area = "Pupil" });
                    }
                    else if (dt.Rows[0]["UserType"].ToString() == "Teacher")
                    {
                        HttpContext.Session.SetInt32("TeacherID", id);
                        return RedirectToAction("Display", "TeacherProfile");
                    }
                    else if (dt.Rows[0]["UserType"].ToString() == "Manager")
                    {
                        HttpContext.Session.SetInt32("managerID", id);
                        HttpContext.Session.SetString("managerName", name);
                        HttpContext.Session.SetString("managerSurname", surname);

                        string connString2 = configuration.GetConnectionString("connString");
                        SqlConnection dbConn2 = new SqlConnection(connString2);


                        dbConn2.Open();

                        SqlCommand dbComm2 = new SqlCommand("sp_GetCentreById", dbConn2);
                        dbComm2.CommandType = CommandType.StoredProcedure;
                        int? id2 = HttpContext.Session.GetInt32("managerID");
                        dbComm2.Parameters.AddWithValue("@ManagerID", id2);

                        SqlDataAdapter dbAdapter2 = new SqlDataAdapter(dbComm2);
                        DataTable dt2 = new DataTable();
                        dbAdapter2.Fill(dt2);

                        dbConn2.Close();
                        string centreName1 = "";
                        int centreNo = 0;
                        string cenEmailassword = "";
                        string cenEmail = "";

                        for (int i = 0; i < 1; i++)
                        {
                            centreName1 = dt2.Rows[0]["Name"].ToString();
                            centreNo = int.Parse(dt2.Rows[0]["centreNo"].ToString());
                            cenEmail = dt2.Rows[0]["EmailAddress"].ToString();
                            cenEmailassword = dt2.Rows[0]["Password"].ToString();

                        }

                        HttpContext.Session.SetString("CentreName", centreName1);
                        HttpContext.Session.SetInt32("CentreNo", centreNo);
                        HttpContext.Session.SetString("CentreEmailAddress", cenEmail);
                        HttpContext.Session.SetString("CentreEmailPassword", cenEmailassword);

                        return RedirectToAction("Index", "Home", new { area = "Manager" }); ;
                    }
                    else if (dt.Rows[0]["UserType"].ToString() == "Regional")
                    {

                        return RedirectToAction("Welcome", "Regional");
                    }
                    else if (dt.Rows[0]["UserType"].ToString() == "Provincial")
                    {

                        return RedirectToAction("Welcome", "Provincial");
                    }
                    else
                        return Invalid();



                }
                else
                    return Invalid();
            }
            else
            {
                ModelState.AddModelError("", "Invalid username/password.");
                return View(model);
            }
        }
        private ActionResult Invalid()
        {
            ModelState.AddModelError("", "Invalid username/password");
            return View("Login");
        }


    }
}

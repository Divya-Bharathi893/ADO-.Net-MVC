using district_cal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace district_cal.Controllers
{
    public class CityController : Controller
    {
        // GET: City

        //create

        public ActionResult CityCreate()
        {
            CityModel model = new CityModel();
            var getcountry = model.country();
            ViewBag.country = new SelectList(getcountry,"country_id","country_name");
            return View("CityCreate");
        }

        //insert
        public ActionResult InsertRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
               //var data = frm["country_id"];
                CityModel model = new CityModel();               
                string city_name = frm["city_name"];              
                bool isActive = frm["isActive"] == "on" ? true : false;
                int country = Convert.ToInt32(frm["country"]);             
                var status = model.InsertCity(city_name, isActive, country);
                return RedirectToAction("CityIndex");
            }
            else
            {
                return RedirectToAction("CityIndex");
            }
        }

        /*Delete*/
        public ActionResult Delete(int city_id)
        {
            CityModel model = new CityModel();
            model.DeleteCity(city_id);
            return RedirectToAction("CityIndex");
        }

        public ActionResult CityEdit(int city_id)
        {
            CityModel model = new CityModel();
            DataTable dt = model.GetCityByID(city_id);
            int cty = dt.Rows[0].Field<int>(1);
            var getcountry = model.country();
            ViewBag.country = new SelectList(getcountry, "country_id", "country_name",cty);
            return View(dt);
        }
        public ActionResult UpdateRecord(FormCollection frm, string action)
        {
            if (action == "Submit")
            {
                //var data = frm["country_id"];
                CityModel model = new CityModel();
                int city_id = Convert.ToInt32(frm["city_id"]);
                var country = Convert.ToInt32(frm["country"]);               
              string city_name = frm["city_name"];
                bool isActive = frm["isActive"] == "on" ? true : false;                
                var status = model.UpdateCity(city_id, country, city_name, isActive);

                return RedirectToAction("CityIndex");
            }
            else
            {
                return RedirectToAction("CityIndex");
            }
        }
    public ActionResult CityIndex()
        {
            CityModel model = new CityModel();
            DataTable dt = model.GetAllCity();
            return View("CityIndex", dt);
        }

       
    }
}
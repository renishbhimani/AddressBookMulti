using AddressBookMulti.DAL;
using AddressBookMulti.Areas.LOC_City.Models;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static AddressBookMulti.Areas.LOC_Country.Models.LOC_CountryModel;
using static AddressBookMulti.Areas.LOC_State.Models.LOC_StateModel;
using System.Data.SqlClient;
using AddressBookMulti.Areas.LOC_Country.Models;
using AddressBookMulti.Areas.LOC_State.Models;

namespace AddressBookMulti.Areas.LOC_City.Controllers
{
    [CheckAccess]
    [Area("LOC_City")]
    [Route("LOC_City/[Controller]/[action]")]
    public class LOC_CityController : Controller
    {

        #region Index
        public IActionResult Index()
        {
            #region SelectAll

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_City_SelectAll();
            return View("LOC_CityList", dt);

            #endregion
        }
        #endregion

        #region Add
        public IActionResult Add(int CityID)
        {

            #region Country Drop Down

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dataTableByCountryDropDwon = dalLOC.CountryDropDwon();

            List<LOC_Country_SelectForDropDownModel> CountryDropDwonListPage = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dataTableByCountryDropDwon.Rows)
            {
                LOC_Country_SelectForDropDownModel CountryDropDwon = new LOC_Country_SelectForDropDownModel();
                CountryDropDwon.CountryID = Convert.ToInt32(dr["CountryID"]);
                CountryDropDwon.CountryName = dr["CountryName"].ToString();
                CountryDropDwonListPage.Add(CountryDropDwon);
            }
            ViewBag.CountryList = CountryDropDwonListPage;


            List<LOC_State_SelectForDropDownModel> StateDropDwonListPage = new List<LOC_State_SelectForDropDownModel>();
            ViewBag.StateList = StateDropDwonListPage;

            #endregion

            #region Select By PK

            if (CityID != null)
            {
                DataTable dt = dalLOC.dbo_PR_LOC_City_SelectByPK(CityID);
                if (dt.Rows.Count > 0)
                {
                    LOC_CityModel modelLOC_City = new LOC_CityModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                        modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                        modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                        modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_City.CityName = dr["CityName"].ToString();
                        modelLOC_City.PinCode = dr["PinCode"].ToString();
                        modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("LOC_CityAddEdit", modelLOC_City);
                }

            }

            #endregion

            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {

                LOC_DAL dalLOC = new LOC_DAL();


                if (modelLOC_City.CityID == null)
                {

                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_City_Insert(modelLOC_City)))
                    {
                        TempData["CityInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_City_UpdateByPK(modelLOC_City)))
                    {

                        TempData["CityUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }

            return RedirectToAction("Add");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {

            LOC_DAL dalLOC = new LOC_DAL();

            if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_City_DeleteByPK(CityID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }
        #endregion

        #region DropDownByCountry
        [HttpPost]
        public IActionResult DropDownByCountry(int CountryID)
        {
            #region State Drop Down

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.StateDropDwon(CountryID);

            List<LOC_State_SelectForDropDownModel> StateDropDwonPage = new List<LOC_State_SelectForDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_State_SelectForDropDownModel modelStateDropDwon = new LOC_State_SelectForDropDownModel();
                modelStateDropDwon.StateID = Convert.ToInt32(dr["StateID"]);
                modelStateDropDwon.StateName = dr["StateName"].ToString();
                StateDropDwonPage.Add(modelStateDropDwon);
            }
            ViewBag.StateList = StateDropDwonPage;
            var vModel = StateDropDwonPage;
            return Json(vModel);

            #endregion
        }
        #endregion

        #region Filter Records
        public IActionResult Filter(LOC_CityModel modelLOC_City, string CountryName, string StateName)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.SearchCityTable(CountryName, StateName, modelLOC_City.CityName);
            return View("LOC_CityList", dt);
        }
        #endregion

    }
}

using AddressBookMulti.DAL;
using AddressBookMulti.Areas.LOC_State.Models;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static AddressBookMulti.Areas.LOC_Country.Models.LOC_CountryModel;
using AddressBookMulti.Areas.LOC_Country.Models;

namespace AddressBookMulti.Areas.LOC_State.Controllers
{
    [CheckAccess]
    
    [Area("LOC_State")]
    [Route("LOC_State/[Controller]/[action]")]
    public class LOC_StateController : Controller
    {
        #region Index
        public IActionResult Index()
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_State_SelectAll();
            return View("LOC_StateList", dt);

        }
        #endregion

        #region Add
        public IActionResult Add(int StateID)
        {
            #region Country Drop Down

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dataTableByCountryDropDwon = dalLOC.CountryDropDwon();

            List<LOC_Country_SelectForDropDownModel> CountryDropDwonListPage = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dataTableByCountryDropDwon.Rows)
            {
                LOC_Country_SelectForDropDownModel modelLOC_Country = new LOC_Country_SelectForDropDownModel();
                modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                modelLOC_Country.CountryName = dr["CountryName"].ToString();
                CountryDropDwonListPage.Add(modelLOC_Country);
            }
            ViewBag.CountryList = CountryDropDwonListPage;

            #endregion

            #region Select By PK

            if (StateID != null)
            {
                DataTable dt = dalLOC.dbo_PR_LOC_State_SelectByPK(StateID);

                if (dt.Rows.Count > 0)
                {
                    LOC_StateModel modelLOC_State = new LOC_StateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                        modelLOC_State.StateName = dr["StateName"].ToString();
                        modelLOC_State.StateCode = dr["StateCode"].ToString();
                        modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("LOC_StateAddEdit", modelLOC_State);
                }
            }

            #endregion

            return View("LOC_StateAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
             LOC_DAL dalLOC = new LOC_DAL();

             if (modelLOC_State.StateID == null)
             {
                 if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_Insert(modelLOC_State)))
                 {
                    TempData["StateInsertMessage"] = "Record inserted successfully";
                 }
             }
             else
             {
                if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_UpdateByPK(modelLOC_State)))
                {
                   TempData["StateUpdateMessage"] = "Record Update Successfully";
                }

                return RedirectToAction("Index");
             }

            return RedirectToAction("Add");
        }

        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            LOC_DAL dalLOC = new LOC_DAL();

            if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_State_DeleteByPK(StateID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        #endregion

        #region Filter Records
        public IActionResult Filter(LOC_StateModel modelLOC_State, string? CountryName)
        {

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.SearchStateTable(modelLOC_State.StateName,modelLOC_State.StateCode, CountryName);
            return View("LOC_StateList", dt);
        }
        #endregion

    }
}

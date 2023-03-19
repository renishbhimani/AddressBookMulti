using AddressBookMulti.DAL;
using AddressBookMulti.Areas.LOC_Country.Models;
using MetronicAddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookMulti.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    [Route("LOC_Country/[Controller]/[action]")]

    public class LOC_CountryController : Controller
    {

        #region Index

        #region Select All
        public IActionResult Index()
        {

            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.dbo_PR_LOC_Country_SelectAll();

            return View("LOC_CountryList", dt);

            
        }
        #endregion

        #endregion

        #region Add
        public IActionResult Add(int CountryID)
        {
            #region Select By PK
            if (CountryID != null)
            {
                LOC_DAL dalLOC = new LOC_DAL();

                DataTable dt = dalLOC.dbo_PR_LOC_Country_SelectByPK(CountryID);
                if (dt.Rows.Count > 0)
                {
                    LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_Country.CountryName = dr["CountryName"].ToString();
                        modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                        modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);

                    }
                    return View("LOC_CountryAddEdit", modelLOC_Country);
                }
            }
            #endregion

            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Save 
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (ModelState.IsValid)
            {

                LOC_DAL dalLOC = new LOC_DAL();


                if (modelLOC_Country.CountryID == null)
                {

                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_Country_Insert(modelLOC_Country)))
                    {
                        TempData["CountryInsertMessage"] = "Record inserted successfully";

                    }
                }
                else
                {
                    if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_Country_UpdateByPK(modelLOC_Country)))
                    {

                        TempData["CountryUpdateMessage"] = "Record Update Successfully";

                    }
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {

            LOC_DAL dalLOC = new LOC_DAL();

            if (Convert.ToBoolean(dalLOC.dbo_PR_LOC_Country_DeleteByPK(CountryID)))
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }
        #endregion


        #region Filter Records
        public IActionResult Filter(LOC_CountryModel modelLOC_Country)
        {
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.SearchCountryTable(modelLOC_Country.CountryName, modelLOC_Country.CountryCode);
            return View("LOC_CountryList", dt);

        }
        #endregion

    }
}

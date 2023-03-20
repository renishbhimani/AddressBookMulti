using AddressBookMulti.DAL;
using AddressBookMulti.Areas.CON_Contact.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using MetronicAddressBook.BAL;
using AddressBookMulti.Areas.LOC_Country.Models;
using AddressBookMulti.Areas.LOC_State.Models;
using AddressBookMulti.Areas.LOC_City.Models;
using AddressBookMulti.Areas.MST_ContactCategory.Models;

namespace AddressBookMulti.Areas.CON_Contact.Controllers
{
    [CheckAccess]
    [Area("CON_Contact")]
    [Route("CON_Contact/[Controller]/[action]")]

    public class CON_ContactController : Controller
    {
        #region Index

        public IActionResult Index()
        {
            #region SelectAll


            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.dbo_PR_CON_Contact_SelectAll();
            return View("CON_ContactList", dt);
            
            #endregion
        }

        #endregion

        #region Add
        public IActionResult Add(int ContactID)
        {

            #region Country Drop Down

            CON_DAL dalCON = new CON_DAL();
            DataTable dataTableByCounryDropDwon = dalCON.CountryDropDwon();

            List<LOC_Country_SelectForDropDownModel>  CountryDropDwonListPage = new List<LOC_Country_SelectForDropDownModel>();
            foreach (DataRow dr in dataTableByCounryDropDwon.Rows)
            {
                LOC_Country_SelectForDropDownModel modelLOC_Country = new LOC_Country_SelectForDropDownModel();
                modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                modelLOC_Country.CountryName = dr["CountryName"].ToString();
                CountryDropDwonListPage.Add(modelLOC_Country);
            }
            ViewBag.CountryList = CountryDropDwonListPage;


            List<LOC_State_SelectForDropDownModel> StateDropDwonListPage = new List<LOC_State_SelectForDropDownModel>();
            ViewBag.StateList = StateDropDwonListPage;
            List<LOC_City_SelectForDropDownModel> CityDropDwonListPage = new List<LOC_City_SelectForDropDownModel>();
            ViewBag.CityList = CityDropDwonListPage;

            #endregion

            #region Contact Category Drop Down

            DataTable dataTableByContactCategoryDropDwon = dalCON.ContactCategoryDropDwon();

            List<MST_ContactCategory_SelectForDropDownModel> ContactCategoryDropDwonListPage = new List<MST_ContactCategory_SelectForDropDownModel>();
            foreach (DataRow dr in dataTableByContactCategoryDropDwon.Rows)
            {
                MST_ContactCategory_SelectForDropDownModel modelMST_ContactCategory = new MST_ContactCategory_SelectForDropDownModel();
                modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                ContactCategoryDropDwonListPage.Add(modelMST_ContactCategory);
            }
            ViewBag.ContactCategoryList = ContactCategoryDropDwonListPage;

            #endregion

            #region Select By PK
            if (ContactID != null)
            {
                DataTable dt = dalCON.dbo_PR_CON_Contact_SelectByPK(ContactID);

                if (dt  !=null && dt.Rows.Count > 0)
                {
                    CON_ContactModel modelCON_Contact = new CON_ContactModel();
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (!dr["CountryID"].Equals(DBNull.Value))
                            DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                        
                        DropDownByState(Convert.ToInt32(dr["StateID"]));

                        modelCON_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                        modelCON_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelCON_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                        modelCON_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                        modelCON_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                        modelCON_Contact.ContactName = dr["ContactName"].ToString();
                        modelCON_Contact.Address = dr["Address"].ToString();
                        modelCON_Contact.PinCode = dr["PinCode"].ToString();
                        modelCON_Contact.MobileNo = dr["MobileNo"].ToString();
                        modelCON_Contact.AlternetContact = dr["AlternetContact"].ToString();
                        modelCON_Contact.Email = dr["Email"].ToString();
                        modelCON_Contact.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                        modelCON_Contact.LinkedIn = dr["LinkedIn"].ToString();
                        modelCON_Contact.Twitter = dr["Twitter"].ToString();
                        modelCON_Contact.Insta = dr["Insta"].ToString();
                        modelCON_Contact.Gender = dr["Gender"].ToString();
                        modelCON_Contact.PhotoPath = dr["PhotoPath"].ToString();
                        modelCON_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelCON_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }

                    return View("CON_ContactAddEdit", modelCON_Contact);

                }
            }
            #endregion

            return View("CON_ContactAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            /*#region Server Side Validation
            string strError = "";


            if (modelCON_Contact.ContactName.Trim() == "")
                strError += "- Enter Contact Name";

            if(strError!="")
            {
                TempData["CountryUpdateMessage"] = "Kindly correct following error(s) <br />" + strError;
                return RedirectToAction("Add"); //Remain in the same form and diaplssy the Server Error
            }

            #endregion Server Side Validation*/

                #region PhotoPath
            if (modelCON_Contact.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);
                modelCON_Contact.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCON_Contact.File.CopyTo(stream);
                }

            }
            #endregion

            CON_DAL dalCON = new CON_DAL();

            if (modelCON_Contact.ContactID == null)
            {
                if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_Insert(modelCON_Contact)))
                {
                    TempData["CountryInsertMessage"] = "Record inserted successfully";

                }
            }
            else
            {
                if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_UpdateByPK(modelCON_Contact)))
                {
                    TempData["CountryUpdateMessage"] = "Record Update Successfully";
                }
                    
                return RedirectToAction("Index");
            }

            return RedirectToAction("Add");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {

            CON_DAL dalCON = new CON_DAL();

            if (Convert.ToBoolean(dalCON.dbo_PR_CON_Contact_DeleteByPK(ContactID)))
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

            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.StateDropDwon(CountryID);

            List<LOC_State_SelectForDropDownModel> StateDropDwonListPage = new List<LOC_State_SelectForDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_State_SelectForDropDownModel modelLOC_State = new LOC_State_SelectForDropDownModel();
                modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                modelLOC_State.StateName = dr["StateName"].ToString();
                StateDropDwonListPage.Add(modelLOC_State);
            }
            ViewBag.StateList = StateDropDwonListPage;
            var vModel = StateDropDwonListPage;
            return Json(vModel);

            #endregion
        }
        #endregion

        #region DropDownByState
        [HttpPost]
        public IActionResult DropDownByState(int StateID)
        {
            #region City Drop Down

            CON_DAL dalCON = new CON_DAL();
            DataTable dt = dalCON.CityDropDwon(StateID);

            List<LOC_City_SelectForDropDownModel> CityDropDwonListPage = new List<LOC_City_SelectForDropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                LOC_City_SelectForDropDownModel modelLOC_City = new LOC_City_SelectForDropDownModel();
                modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                modelLOC_City.CityName = dr["CityName"].ToString();
                CityDropDwonListPage.Add(modelLOC_City);
            }
            ViewBag.CityList = CityDropDwonListPage;
            var vModel = CityDropDwonListPage;
            return Json(vModel);

            #endregion
        }
        #endregion

        #region Filter Records
        public IActionResult Filter(CON_ContactModel modelCON_Contact, string CountryName, string StateName, string CityName)
        {
            CON_DAL dalCON= new CON_DAL();
            DataTable dt = dalCON.SearchContactTable(CountryName,StateName,CityName, modelCON_Contact.ContactName);
            return View("CON_ContactList", dt);
        }
        #endregion
    }
}

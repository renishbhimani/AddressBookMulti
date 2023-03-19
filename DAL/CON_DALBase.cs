﻿using AddressBookMulti.Areas.CON_Contact.Models;
using AddressBookMulti.Areas.MST_ContactCategory.Models;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using AddressBookMulti.BAL;

namespace AddressBookMulti.DAL
{
    public class CON_DALBase : DALHelper
    {
        #region CON_SelectAll
     
        #region CON_Contact_SelectAll
        public DataTable dbo_PR_CON_Contact_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_SelectAllByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region MST_Contact_Category_SelectAll
        public DataTable dbo_PR_MST_ContactCategory_SelectAll()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectAllByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #endregion

        #region CON_DELETE

        #region CON_Contact_DeleteByPK
        public bool dbo_PR_CON_Contact_DeleteByPK(int ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region MST_ContactCategory_DeleteByPK
        public bool dbo_PR_MST_ContactCategory_DeleteByPKUserID(int ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_DeleteByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region CON_SelectByPK

        #region CON_Contact_SelectByPK
        public DataTable dbo_PR_CON_Contact_SelectByPK(int ContactID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_SelectByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, ContactID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region MST_ContactCategory_SelectByPK
        public DataTable dbo_PR_MST_ContactCategory_SelectByPK(int ContactCategoryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region CON_UpdateByPK

        #region CON_Contact_UpdateByPK
        public bool dbo_PR_CON_Contact_UpdateByPK(CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_UpdateByPKUserID");
                sqlDB.AddInParameter(dbCMD, "ContactID", SqlDbType.Int, modelCON_Contact.ContactID);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, modelCON_Contact.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.NVarChar, modelCON_Contact.ContactName);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, modelCON_Contact.Address);
                sqlDB.AddInParameter(dbCMD, "PinCode", SqlDbType.NVarChar, modelCON_Contact.PinCode);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.NVarChar, modelCON_Contact.MobileNo);
                sqlDB.AddInParameter(dbCMD, "AlternetContact", SqlDbType.NVarChar, modelCON_Contact.AlternetContact);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, modelCON_Contact.BirthDate);
                sqlDB.AddInParameter(dbCMD, "LinkedIn", SqlDbType.NVarChar, modelCON_Contact.LinkedIn);
                sqlDB.AddInParameter(dbCMD, "Twitter", SqlDbType.NVarChar, modelCON_Contact.Twitter);
                sqlDB.AddInParameter(dbCMD, "Insta", SqlDbType.NVarChar, modelCON_Contact.Insta);
                sqlDB.AddInParameter(dbCMD, "Gender", SqlDbType.NVarChar, modelCON_Contact.Gender);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelCON_Contact.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region MST_ContactCategory_UpdateByPK
        public bool dbo_PR_MST_ContactCategory_UpdateByPK(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_UpdateByPKUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, modelMST_ContactCategory.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region CON_Insert

        #region CON_Contact_Insert
        public bool dbo_PR_CON_Contact_Insert(CON_ContactModel modelCON_Contact)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_InsertByUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelCON_Contact.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelCON_Contact.StateID);
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelCON_Contact.CityID);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryID", SqlDbType.Int, modelCON_Contact.ContactCategoryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.NVarChar, modelCON_Contact.ContactName);
                sqlDB.AddInParameter(dbCMD, "Address", SqlDbType.NVarChar, modelCON_Contact.Address);
                sqlDB.AddInParameter(dbCMD, "PinCode", SqlDbType.NVarChar, modelCON_Contact.PinCode);
                sqlDB.AddInParameter(dbCMD, "MobileNo", SqlDbType.NVarChar, modelCON_Contact.MobileNo);
                sqlDB.AddInParameter(dbCMD, "AlternetContact", SqlDbType.NVarChar, modelCON_Contact.AlternetContact);
                sqlDB.AddInParameter(dbCMD, "Email", SqlDbType.NVarChar, modelCON_Contact.Email);
                sqlDB.AddInParameter(dbCMD, "BirthDate", SqlDbType.DateTime, modelCON_Contact.BirthDate);
                sqlDB.AddInParameter(dbCMD, "LinkedIn", SqlDbType.NVarChar, modelCON_Contact.LinkedIn);
                sqlDB.AddInParameter(dbCMD, "Twitter", SqlDbType.NVarChar, modelCON_Contact.Twitter);
                sqlDB.AddInParameter(dbCMD, "Insta", SqlDbType.NVarChar, modelCON_Contact.Insta);
                sqlDB.AddInParameter(dbCMD, "Gender", SqlDbType.NVarChar, modelCON_Contact.Gender);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelCON_Contact.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region MST_ContactCategory_Insert
        public bool dbo_PR_MST_ContactCategory_Insert(MST_ContactCategoryModel modelMST_ContactCategory)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_InsertByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.NVarChar, modelMST_ContactCategory.ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region Search ContactTable
        public DataTable SearchContactTable(String CountryName, String StateName, String CityName , String ContactName)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_CON_Contact_FilterByCountryNameStateNameCityNameContactCategoryNameContactNameUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, CityName);
                sqlDB.AddInParameter(dbCMD, "ContactName", SqlDbType.NVarChar, ContactName);
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region CountryDropDwon

        public DataTable CountryDropDwon()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectForDropDownByUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region StateDropDwon

        public DataTable StateDropDwon(int CountryID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectForDropDownByUserID");
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, CountryID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region CityDropDwon

        public DataTable CityDropDwon(int StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectForDropDownByUserID");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region ContactCategoryDropDwon

        public DataTable ContactCategoryDropDwon()
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(myConnectionString);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_SelectForDropDownByContactCategoryNameUserID");
                sqlDB.AddInParameter(dbCMD, "UserID", SqlDbType.Int, CV.UserID());
                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);

                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}

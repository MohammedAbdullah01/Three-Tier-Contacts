using System;
using System.Data;
using System.Text;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsCountry
    {
        public int CountryID { get; private set; }
        public string CountryName { get; set; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }

        public enum enMode { AddNew = 0 , Update = 1}

        public enMode Mode { get; private set; } = enMode.AddNew;

        public clsCountry()
        {
            CountryID = -1;
            CountryName = string.Empty;
            Code = string.Empty;
            PhoneCode = string.Empty;

            Mode = enMode.AddNew;
        }

        private clsCountry(int countryID, string countryName , string code , string phoneCode)
        {
            CountryID = countryID;
            CountryName = countryName;
            Code = code;
            PhoneCode = phoneCode;

            Mode = enMode.Update;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }

        public static clsCountry Find(int countryId)
        {
            string countryName = string.Empty;
            string code = string.Empty;
            string phoneCode = string.Empty;

            if (clsCountryDataAccess.GetFindInfoByCountryId(countryId, ref countryName , ref code, ref phoneCode))
            {
                return new clsCountry(countryId, countryName, code, phoneCode);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string countryName)
        {
            int countryID = -1;
            string code = string.Empty, phoneCode = string.Empty;


            if (clsCountryDataAccess.GetFindInfoByCountryName(countryName, ref countryID , ref code, ref phoneCode))
            {
                return new clsCountry(countryID, countryName, code, phoneCode);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewCountry()
        {
            CountryID = clsCountryDataAccess.AddNewCountry(CountryName , Code, PhoneCode);

            return CountryID != -1;
        }

        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(CountryID, CountryName, Code, PhoneCode);
        }

        public static bool Delete(int countryID)
        {
            return clsCountryDataAccess.DeleteCountry(countryID);
        }

        public static bool IsExistsByCountryId(int countryID)
        {
            return clsCountryDataAccess.IsCountryIdExist(countryID);
        }

        public static bool IsExistsByCountryName(string countryName)
        {
            return clsCountryDataAccess.IsCountryNameExist(countryName);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if(_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();

                default:
                    return false;
            }
        }

        public override string ToString()
        {
            StringBuilder sa = new StringBuilder();

            sa.AppendLine($"Country ID: {CountryID}");
            sa.AppendLine($"Country Name: {CountryName}");
            sa.AppendLine($"Code: {Code}");
            sa.AppendLine($"Phone Code: {PhoneCode}");
            return sa.ToString();
        }
    }
}

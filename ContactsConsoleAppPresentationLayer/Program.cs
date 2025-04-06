using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsConsoleAppPresentationLayer
{
    internal class Program
    {
        #region Contacts
        static void TestFindContactInfoByID(int id)
        {
            clsContact contact = clsContact.Find(id);
            Console.WriteLine(contact != null ? contact.ToString() : $"Not Found ContactID: [{id}]");
        }

        static void TestAddNewContact()
        {
            clsContact contact = new clsContact();

            contact.FirstName = "TestF";
            contact.LastName = "TestL";
            contact.Email = "TestEmail";
            contact.Phone = "0111111111";
            contact.Address = "Address";
            contact.CountryID = 1;
            contact.DateOfBirth = DateTime.Now;
            contact.ImagePath = string.Empty;

            Console.WriteLine(contact.Save()
                ? $"Contact Added Successfullly With ContactId = [{contact.Id}]"
                : "Fail Added Contact");
        }

        static void TestUpdateContact(int id)
        {
            clsContact contact = clsContact.Find(id);

            if (contact != null)
            {
                contact.FirstName = "updateF";
                contact.LastName = "updateL";
                contact.Email = "updateEmail";
                contact.Address = "updateAddress";
                contact.Phone = "updatePhone";
                contact.CountryID = 2;
                contact.DateOfBirth = new DateTime(1997, 8, 16, 08, 20, 10);
                contact.ImagePath = "C:/Pc";

                Console.WriteLine(contact.Save()
                ? $"Contact Updated Successfullly With ContactId = [{contact.Id}]"
                : "Fail Updated Contact");
            }
            else
            {
                Console.WriteLine($"Not Fount Contact [ID: {id}]");
            }
        }

        static void TestDeleteContact(int id)
        {
            if (clsContact.IsContactExist(id))
            {
                Console.WriteLine(clsContact.Delete(id)
                    ? $"Contact Deleted Successfully With Contact Id : {id}"
                    : "Fail Deleted Contact");
            }
            else
            {
                Console.WriteLine($"Not Found Contact [ID: {id}]");
            }

        }

        static void TestGetAllContacts()
        {
            DataTable dateTable = clsContact.GetAllContacts();

            foreach (DataRow row in dateTable?.Rows)
            {
                Console.WriteLine($"ContactID: {row["ContactID"]}, FirstName: {row["FirstName"]}, LastName: {row["LastName"]}");
            }
        }
        #endregion

        #region Countries

        static void TestFindCountryInfoByCountryId(int countryId)
        {
            clsCountry country = clsCountry.Find(countryId);
            Console.WriteLine(country != null ? country.ToString() : $"Not Found CountryID: [{countryId}]");
        }

        static void TestFindCountryInfoByCountryName(string countryName)
        {
            clsCountry country = clsCountry.Find(countryName);
            Console.WriteLine(country != null ? country.ToString() : $"Not Found Country Name: [{countryName}]");
        }

        static void TestAddNewCountry()
        {
            clsCountry country = new clsCountry();

            country.CountryName = "TestCountry";
            country.Code = "TC";
            country.PhoneCode = "123";

            Console.WriteLine(country.Save()
                ? $"Country Added Successfullly With CountryId = [{country.CountryID}]"
                : "Fail Added Country");
        }

        static void TestUpdateCountery(int countryId)
        {
            clsCountry country = clsCountry.Find(countryId);
            if(country != null)
            {
                country.CountryName = "updateCountry";
                country.Code = "upC";
                country.PhoneCode = "upP";
                Console.WriteLine(country.Save()
                    ? $"Country Updated Successfullly With CountryId = [{country.CountryID}]"
                    : "Fail Updated Country");
            }
            else
            {
                Console.WriteLine($"Not Found Country [ID: {countryId}]");

            }
        }

        static void TestDeleteCountryById(int countryId)
        {
            if (clsCountry.IsExistsByCountryId(countryId))
            {
                Console.WriteLine(clsCountry.Delete(countryId)
                    ? $"Country Deleted Successfully With Country Id : {countryId}"
                    : "Fail Deleted Country");
            }
            else
            {
                Console.WriteLine($"Not Found Country [ID: {countryId}]");
            }
        }

        static void TestIsExistsByCountryId(int countryId)
        {
            Console.WriteLine(clsCountry.IsExistsByCountryId(countryId)
                ? $"Country ID [{countryId}] Exists"
                : $"Country ID [{countryId}] Not Exists");
        }

        static void TestIsExistsByCountryName(string countryName)
        {
            Console.WriteLine(clsCountry.IsExistsByCountryName(countryName)
                ? $"Country Name [{countryName}] Exists"
                : $"Country Name [{countryName}] Not Exists");
        }

        static void TestListCountries()
        {

            foreach(DataRow row in clsCountry.GetAllCountries().Rows)
            {
                Console.WriteLine($@"Country ID : [{row["countryId"]}] , Country Name : [{row["countryName"]}] , Code : [{row["code"]}] , Phone Code : [{row["phoneCode"]}]");
            }
        }
        #endregion

        static void Main(string[] args)
        {
            #region Contacts (CRUD) && (Get All Contacts) && Find Contact 
            //TestFindContactInfoByID(1);

            //TestAddNewContact();

            //TestUpdateContact(15);

            //TestDeleteContact(19);

            //TestGetAllContacts(); 
            #endregion

            #region Countries (CRUD) && (Get All Countries) && Find Country(Country ID || Country Name)
            //TestFindCountryInfoByCountryName("Germany");

            //TestFindCountryInfoByCountryId(5);

            //TestAddNewCountry();

            //TestUpdateCountery(50);

            //TestDeleteCountryById(8);

            //TestIsExistsByCountryName("Germany"); 

            //TestIsExistsByCountryId(4);

            //TestListCountries();

            #endregion
        }
    }
}

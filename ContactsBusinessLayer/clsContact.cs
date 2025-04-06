using System;
using System.Data;
using System.Text;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CountryID { get; set; }
        public string ImagePath { get; set; }

        public enum enMode { AddNewContact = 0 , UpdateContact = 1};

        public enMode Mode = enMode.AddNewContact;

        public clsContact()
        {
            Id = -1;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            DateOfBirth = DateTime.MinValue;
            CountryID = -1;
            ImagePath = string.Empty;

            Mode = enMode.AddNewContact;
        }

        private clsContact(int id, string FirstName, string LastName, string Email, 
                            string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            Id = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.UpdateContact;
        }

        public static clsContact Find(int id)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";

            DateTime DateOfBirth = DateTime.Now; 

            int CountryID = default;

            if
                (
                    clsContactDataAccess.GetFindInfoById
                    (
                        id,
                        ref FirstName,
                        ref LastName,
                        ref Email,
                        ref Phone,
                        ref Address,
                        ref DateOfBirth,
                        ref CountryID,
                        ref ImagePath
                    )
                )
            {
                return new clsContact(id, FirstName, LastName, Email, Phone, Address, 
                                        DateOfBirth, CountryID, ImagePath);
                
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewContact()
        {
            Id = clsContactDataAccess.AddNewContact
            (
                FirstName,
                LastName,
                Email,
                Phone,
                Address,
                DateOfBirth,
                CountryID,
                ImagePath
            );

            return (Id != -1);
        }

        private bool _UpdateContact()
        {
            return (clsContactDataAccess.UpdateContact(
                Id,
                FirstName,
                LastName,
                Email,
                Phone,
                Address,
                DateOfBirth,
                CountryID,
                ImagePath) > 0);
        }

        public static bool Delete(int id)
        {
            return (clsContactDataAccess.DeleteContact(id) > 0);
        }

        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }

        public static bool IsContactExist(int id)
        {
            return clsContactDataAccess.IsContactExist(id);
        }

        public bool Save()
        {
            bool isSuccess = false;
            switch (Mode)
            {
                case enMode.AddNewContact:
                    if(_AddNewContact())
                    {
                        Mode = enMode.UpdateContact;
                        isSuccess = true;
                    }
                    else
                    {
                        isSuccess = false;
                    }
                    break;

                case enMode.UpdateContact:
                    isSuccess = _UpdateContact();
                    break;
            }

            return isSuccess;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("=============== Contact Information ================");
            sb.AppendLine($"First Name   : {FirstName}");
            sb.AppendLine($"Last Name    : {LastName}");
            sb.AppendLine($"Email        : {Email}");
            sb.AppendLine($"Phone        : {Phone}");
            sb.AppendLine($"Address      : {Address}");
            sb.AppendLine($"Date of Birth: {DateOfBirth:yyyy-MM-dd}");
            sb.AppendLine($"Country ID   : {CountryID}");
            sb.AppendLine($"Image Path   : {ImagePath}");
            sb.AppendLine("=================================================");
            return sb.ToString();
        }

    }
}

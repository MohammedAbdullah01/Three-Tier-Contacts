# Contacts Management System

A C# console application that provides complete management for contacts and country information with full CRUD (Create, Read, Update, Delete) functionality using a SQL Server database.

## 📋 Overview

This system is built with a clean architecture that separates business logic from data access, allowing for a maintainable and scalable application. It implements two main entities:

- **Contacts**: Manage personal contact information including name, email, phone, address, and more
- **Countries**: Track country data including name, code, and phone code

## 🏗️ Architecture

The application follows a layered architecture:

1. **Business Layer**: Contains business logic in classes like `clsContact` and `clsCountry`
2. **Data Access Layer**: Handles all database operations through classes like `clsContactDataAccess` and `clsCountryDataAccess`
3. **Presentation Layer**: Console-based UI for testing and demonstrating functionality

## ✨ Features

### Contacts Management
- Add new contacts with detailed information
- Find contacts by ID
- Update existing contact information
- Delete contacts
- List all contacts

### Country Management
- Add new country records
- Find countries by ID or name
- Update country information
- Delete countries
- Check if countries exist by ID or name
- List all countries

## 🔧 Technical Implementation

### Contact Class
The `clsContact` class encapsulates all contact-related functionality:
- Properties for contact information
- State management via an enum Mode (AddNewContact/UpdateContact)
- Static methods for database operations
- Method overrides like ToString for easy visualization

### Country Class
The `clsCountry` class provides similar functionality for country data:
- Properties for country details
- Mode tracking (AddNew/Update)
- Static utility methods
- Custom string representation

### Data Access
Both entities have dedicated data access classes that:
- Use parameterized SQL queries for security
- Handle exceptions appropriately
- Use connection string from a centralized settings class
- Return meaningful results for all operations

## 📊 Database Schema

### Contacts Table
- ContactID (PK)
- FirstName
- LastName
- Email
- Phone
- Address
- DateOfBirth
- CountryID (FK)
- ImagePath

### Countries Table
- CountryID (PK)
- CountryName
- Code
- PhoneCode

## 🚀 Getting Started

### Setup
1. Clone this repository
2. Update the connection string in `clsDataAccessSettings.cs` to point to your SQL Server instance
3. Create the database and tables using the schema information
4. Build and run the application

### Usage Examples
The `Program.cs` file contains test methods demonstrating all functionality:

```csharp
// Find a contact by ID
TestFindContactInfoByID(1);

// Add a new contact
TestAddNewContact();

// List all countries
TestListCountries();
```

## 💡 Future Improvements
- Add a graphical user interface
- Implement authentication and authorization
- Add validation for user inputs
- Expand search functionality with filters
- Create reports and data export capabilities

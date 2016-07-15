# Hair Salon Database Management Tool

#### Short Description
This webapp is an Epicodus student project designed to use C# and SQL to be able to create a database of stylists and clients in a hair salon.

#### Author(s)
Matthew Reyes
## Description
This project will allow users to enter in a stylist and click on the stylist to go to their page and see a list of their clients.  From there the user can add clients and edit and delete them as they see fit.  The user also has the ability to update the list of stylists and their information from the main page.

## Technologies Used

* C#
* Nancy and Razor View Engines
* XUnit
* SQL

## Instructions

* Clone the repository
* Open Microsoft SQL Server Management software and run the included .sql file
* In atom open Startup.cs and each test file and update 'Data Source=DESKTOP-7OLC9FT\\SQLEXPRESS' to your own server name.
* In powershell, navigate to the project directory
* In powershell, enter '>dnu restore' and then '>dnx kestrel'.
* Navigate your web browser to http://localhost:5004

## Known Bugs

* Clients are not actually deleted, but their references are no longer available to the user.

## Contacts
<a mailto:"mreyez@gmail.com">Matthew Reyes</a>


### License

Licensed under the MIT License.

&copy;Matthew Reyes

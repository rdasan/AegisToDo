# Aegis ToDo Web Application
##### A simple ToDo MVC 5 WebApp Using Entity Frameowork Code First as ORM for Local SQL Server.

#### Prerequsites:
- Visual Studio IDE
- Relevant Nuget Packages (this will be automatically downloaded with the solution in built)
- Local SQL Server **[(localDb)/MSSQLLocalDb]**

#### Description
This project uses **Entity Framework Code First Migrations** for generating the Database Schema. The Database will be automatically generated in the machine localDb when the project in Run for the first time.

The project provides a basic MVC5 based webpage for basic CRUD operations for ToDo Items
- Add a ToDo Item
- Edit a ToDo Item
- Delete a ToDo Item
- Mark a ToDo Item as **Done**.

The Webpage also tells you if an item is **OverDue** based on today's date. (The item would be marked red)

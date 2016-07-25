# RealEstateAgentApp

This is a code exercise to upload a list of Agents and Agencies to MSSQL Server Database from a CSV File.
This is built using Entity Framework 6, which using CodeFirst migration.


##Assumptions
1. Unique agent is determined by the Agent Name AND Mobile
2. Unique agency is determined by the Agency Phone number
3. Only AUS phone number (including 1300 number and mobile number) that is recognized for validation and sanitation of the data.


##Setting Up

###Connecting to Database

Make sure to change the connection string of the `DefaultConnection` to the PROD or other MSSQL Server Db on these two files:
- Assemblies / App.config
- RealEstateAgencyApp / Web.config

###Performing Initial Data Migration (EF Code First migration)
1. Open the solution in MS Visual Studio.
2. Go to your Package Manager Console and make sure the `Default project` is pointing to `Assemblies`
3. Run these command

```
Update-Database
```

This will create all the necessary Tables and Relationships to the MS SQL Server Db that have been set in the config








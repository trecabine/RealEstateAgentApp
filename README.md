# RealEstateAgentApp

This is a code exercise to upload a list of Agents and Agencies to MSSQL Server Database from a CSV File.
This is built using Entity Framework 6, which using CodeFirst migration.


##Assumptions



##Setting Up

###Connecting to Database

Make sure to change the connection string of the `DefaultConnection` to the PROD or other MSSQL Server Db on these two files:
- Assemblies / App.config
- RealEstateAgencyApp / Web.config

###Performing Initial Data Migration (EF Code First migration)
1. Open the solution in MS Visual Studio.
2. Go to your Package Manager Console and make sure the `Default project` is pointing to `Assemblies`
3. Run these command

'''
Database-Update
'''

This will create all the necessary Tables and Relationships to the MS SQL Server that have been set in the config












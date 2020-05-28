echo off
rem batch file to run a script to create a db
rem 10/15/2018

sqlcmd -S localhost -E -i PharmacyDB_AM.sql
rem sqlcmd -S localhost\sqlexpress -E -i pharmacyDB.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE
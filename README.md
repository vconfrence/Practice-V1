Dear user,
This project has two applications, one is the console app called "ConsoleApp" and the other is the web api REST 
service called "Practice" all hosted within the same solution file which also includes a class library used by both
applications as well as test projects for both the console app as well as the web app which is set to the default
application to run within the IDE (VS2017).

Console Apllication:
Asks for a folder which contains the comma, pipe and space delimited files within that folder.
Note that the files should be named cfile.csv, pfile.csv and sfile.csv for comma, pipe and space delimited files.
At least one of the files have to be present within the folder for the program to run otherwise the user will be
prompted and waits for the user input.

Input files have no headers and have the data in the order of lastname, firstname, gender, favorite color, birthdate
While processing each file if the program finds a row which is invalid it skips that row and continues processing.
Invalid rows either don't contain all the fields mentioned earlier or the fields are invalid. 
Firstname and Lastname are considered valid including characters only.
Gender has to be 'male' or 'female' case insensitive.
Birthdate has be a valid date in the format month/day/year.
Once the files have been processed respective sort order views will be displayed on the console.


Web Application:
The user has access to api/Records/POST and api/Records/GET/sortorder once the application is launched.

POST
The user posts into the body of the client "lastname, firstname, gender, favorite color, birthdate" in the
given order and format. Note that instead of commas space or pipe can be used but the order has to be consistent 
within a given string.


GET
If the user specifies sort order then that sort order will be used (gender, birthdate, name) otherwise default 
will be sort by name which is lastname, firstname sort respectively.

Same rules regarding the order and validity of the information applied to the desktop application applies to 
posted information on the web app.

To control what type of environment the application is running in:

Use the environment variable:

asp.net_environment

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1

To start this app:

run build.sh or build.cmd
docker run -p 8000:80 13faf875633a
then go to http://localhost:8080 in the browser

To migrate the schema manually in Visual Studio type:

run script-migration

Take the SQL output and execute it in the new database.


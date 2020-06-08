To control what type of environment the application is running in (defaults to development):

Use the environment variable:

asp.net_environment

https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1

To start this app (using docker, be sure to install it first: https://docs.docker.com/get-docker/):

run run.sh or run.cmd

then go to http://localhost:8080 in the browser

To migrate the schema manually in Visual Studio type:

run script-migration

Take the SQL output and execute it in the new database.


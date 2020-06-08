docker build -t constellation .
docker run -p 8080:80 -e ASPNETCORE_ENVIRONMENT=development constellation
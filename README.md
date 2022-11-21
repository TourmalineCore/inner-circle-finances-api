# InnerCircle.SalaryService

____
# Getting started with Docker
To start the service, you should go to the solution folder and enter this command in the terminal. This command starts the service in Docker and raises the database.
```
docker-compose up -d
```

You can use Swagger to see all roots by following this link:
```
http://localhost:5000/index.html
```
Service requests are made like this
```
GET http://localhost:5000/api/finances/get-finance-data
POST http://localhost:5000/api/employees/create-employee
```

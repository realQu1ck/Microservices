# Microservices
Microservices Project With Docker

Requirements :
------------------------------------------------------------
ASP.Net Core Version = 6.0.102;
Visual Studio Version = 2022 (For dotnet 6.0)
Docker With (Redis - SQL Server - MongoDB - RabbitMQ - PostgreSQL & Portainer.io) Images // IF you run docker compose this images will be downloaded automatically

Docker Requirements :
------------------------------------------------------------

Memory : 4GB
CPU: 2

How To Run Project ?
------------------------------------------------------------
After Install Requirements And Clone or Download the Project
go to src/ Folder open terminal and run this command :

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

URLs :
------------------------------------------------------------
Catalog API -> http://localhost:8000/swagger/index.html 	| for debug or run -> http://localhost:5000/swagger/index.html 

Basket API -> http://localhost:8001/swagger/index.html 		| for debug or run -> http://localhost:5001/swagger/index.html 

Discount API -> http://localhost:8002/swagger/index.html	| for debug or run -> http://localhost:5002/swagger/index.html 

Discount GRPS -> http://localhost:8003				| for debug or run -> http://localhost:5003

Ordering API -> http://localhost:8004/swagger/index.html	| for debug or run -> http://localhost:5004/swagger/index.html 

Shopping.Aggregator -> http://localhost:8005/swagger/index.html	| for debug or run -> http://localhost:5005/swagger/index.html 

API Gateway -> http://localhost:8010/Catalog Its Not Complate !!!!  -> | for debug or run -> http://localhost:5010/swagger/index.html 

Rabbit Management Dashboard -> http://localhost:15672 -- guest/guest

Portainer -> http://localhost:9000 -- Username - Password in Docker Compose File

pgAdmin PostgreSQL -> http://localhost:5050 -- Username - Password in Docker Compose File


This Project Its Not Complate yet ...!!! :D
------------------------------------------------------------
- Gateway
- WebUI
Its not develope i finish it soon 0_-

Thanks For Course :
https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/?couponCode=FA24745CC57592AB612A

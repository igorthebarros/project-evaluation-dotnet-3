# 📦 .Net Cash Flow Control System - .Net Dockerized Microservices

This system is composed by two main services: TransactionService and ConsolidationService. Transaction deals with all the cash flow and Consolidation with the daily cash consolidation.

Tech-stack: .Net 9, ASP.Net, Docker, Entity Framework Core, PostgreSQL, PGAdmin, Redis, MediatR, AutoMapper, YARP...

# Developer thoughts:

Hi, all! Here I'm going to write it down some ideas and plans I had for this project but didn't manage to deliver in due time.

First of all, this was fun! I've focused on bringing DDD principles and create everything based on the domain contracts/entities. Also, the single responsability and inversion of control principles were well represented in this project.

1. Add Redis Cache to store queries results and avoid too many requests directly to the SQL database
2. Implement Controllers, Services and Repository for User, Merchant, Transaction and DailyBalance entities
3. Implement Polly to handle transient failures and set resilience
4. Implement unit tests
5. Implement integration tests

## 🐳 Running with Docker Compose

Make sure Docker and Docker Compose are installed on your machine.

1. Open your terminal.
2. Navigate to the project root folder.
3. Run this command:

```bash
docker-compose up --build
```

This will download dependencies, build the containers, and start the services.

---

## 💾 Applying EF Core Migrations

The application is configured to run the migrations automatically if it's on Development environment.

If in Production environment, please consider the following:

Once the container is running, open a terminal and execute:

```bash
docker exec -it {your-api-container-name} dotnet ef database update
```

Replace `{your-api-container-name}` with the name of your running API container.

This will apply any pending Entity Framework Core migrations to your database.

---

## 🔍 Accessing Swagger UI

After your container is up and running, open this URL in your browser:

```
http://localhost:{your_port}/swagger
```

Example:
```
http://localhost:5000/swagger
```

This will open the Swagger page where you can explore and test your endpoints.

---

## 📬 Sample Data for `/api/Auth`

When using Swagger or Postman to test your API, send a POST request to:

```
/api/Auth
```

The data presented below represents the user that was automatically setted in the database to make it easier to test this endpoint. Otherwise, the developer would have to add manually. This only happens in the Development environment.

{
  "email": "developer@developer.com",
  "password": "$3cUr3"
}
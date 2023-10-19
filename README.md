# Sportsbook Solution

## Before Starting

* Before running the projects, navigate to the Sportsbook.Docker folder and run the 'CreateAndRun.bat' file.
This will create and run the docker containers (RabbitMQ and Redis).

## General Information

* While developing the project, paid attention to clean code writing principles such as SOLID, KISS, YAGNI, DRY.

* RabbitMQ was used as Message Broker and MassTransit as library.

* It was kept simple, especially because database-related and data operations are not clear.
No complicated design was done. Not even a relationship between tables was created, in-memory SQLite was used.

* Unit Test was not used because it was not needed. But Integration Test was used.

* Added a simple Exception Handling.

## Sportsbook Solution Overview:

API > API.QueueService ---> (RabbitMQ) ---> MatchConsumer > Data.Dapper

1. Sportsbook.API:
    * This is the main API project that provides endpoints for managing sports matches.
    * It contains configurations for controllers, JSON serialization, Swagger, MassTransit, Redis caching, and Fluent Validations.

2. Sportsbook.API.Common:
    * Contains common DTOs used across the solution.
    * Provides extensions for Fluent Validations.

3. Sportsbook.API.QueueService:
    * Responsible for processing match-related operations using a queue.
    * Contains extensions for adding queue services and configurations for AutoMapper.
    * Contains mapping profile for AutoMapper.

4. Sportsbook.Contracts:
    * Defines models, requests, and responses used across the solution.
    * For example, the MatchModel represents a sports match with its associated details.

5. Sportsbook.Data.Dapper:
    * Handles database operations using the Dapper ORM.
    * Contains entities, repositories, and configurations like the DatabaseConfig for database connection details.

6. Sportsbook.Data.Redis:
    * Related to Redis operations for caching purposes.
    * Contains configurations like the RedisConfig for Redis connection details.

7. Sportsbook.Docker:
    * Contains Docker-related files to containerize the application components.
    * The DockerCompose.yml file defines services like RabbitMQ and Redis for container orchestration.

8. Sportsbook.Infrastructure.ExceptionHandler:
    * Provides a middleware for global exception handling across the API.
    * The ExceptionHandlerMiddleware captures and logs exceptions, returning a standardized error response.

9. Sportsbook.Infrastructure.MassTransit:
    * Contains configurations related to MassTransit, a message bus for .NET.
    * The RabbitMQConfig provides settings for connecting to RabbitMQ.

10. Sportsbook.Infrastructure.Swagger:
    * Provides extensions for integrating Swagger, a tool for API documentation and testing.
    * Contains methods to add and use Swagger configurations.

11. Sportsbook.MatchConsumer:
    * Consumes messages from RabbitMQ related to match operations.
    * Contains configurations for MassTransit, repositories, AutoMapper, and services.

12. Sportsbook.MatchConsumer.Business:
    * Contains business logic for processing match-related operations.
    * Contains mapping profile for AutoMapper.

13. Sportsbook.MatchConsumer.Tests
    * This is the test project for Sportsbook.MatchConsumer.
    * It contains integration tests for the functionality provided in the MatchConsumer project.

Each of these projects plays a specific role in the overall solution, providing functionalities ranging from API endpoints, data access, message consumption, to infrastructure concerns.
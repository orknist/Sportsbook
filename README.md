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

1. Sportsbook.Contracts:
    * Defines models (*MessageModel), requests (*MessageRequest), and responses (*MessageResponse) used across the solution.
    * For example, the MatchMessageModel represents a sports match with its associated details.
    * Related to RabbitMQ operations for message publishing and consumption.

2. Sportsbook.Infrastructure.Dapper:
    * Contains Dapper configurations like the DatabaseConfig for database connection details.
    * Creates database tables and data for test purpose.

3. Sportsbook.Infrastructure.ExceptionHandler:
    * Provides a middleware for global exception handling across the API.
    * The ExceptionHandlerMiddleware captures and logs exceptions, returning a standardized error response.

4. Sportsbook.Infrastructure.MassTransit:
    * Contains configurations related to MassTransit, a message bus for .NET.
    * The RabbitMQConfig provides settings for connecting to RabbitMQ.

5. Sportsbook.Infrastructure.Redis:
    * Contains configurations like the RedisConfig for Redis connection details.
    * Related to Redis operations for caching purposes.

6. Sportsbook.Infrastructure.Swagger:
    * Provides extensions for integrating Swagger, a tool for API documentation and testing.
    * Contains methods to add and use Swagger configurations.

7. Sportsbook.API:
    * This is the main API project that provides endpoints for managing sports matches.
    * It contains configurations for controllers, JSON serialization, Swagger, MassTransit, Redis caching, and Fluent Validations.

8. Sportsbook.API.Common:
    * Contains common models (*ApiModel), requests (*ApiRequest), and responses (*ApiResponse) used across the solution.
    * Provides extensions for Fluent Validations.
    * Related to API operations for request validation and response formatting.

9. Sportsbook.API.QueueService:
    * Responsible for processing match-related operations using a queue.
    * Contains extensions for adding queue services and configurations for AutoMapper.
    * Contains mapping profile for AutoMapper.
    * Related to RabbitMQ operations for message publishing and consumption.

10. Sportsbook.MatchConsumer:
    * Consumes messages from RabbitMQ related to match operations.
    * Contains Consumers and configurations for MassTransit, Dapper, Repositories.

11. Sportsbook.MatchConsumer.Business:
    * Contains business logic for processing match-related operations.
    * Contains configurations for AutoMapper, and services.
    * Contains mapping profile for AutoMapper.

12. Sportsbook.Data:
    * Contains entities (*Entity) and repository interfaces.

13. Sportsbook.Data.Dapper:
    * Contains Dapper specified entities (*DapperEntity), concrete repositories, and entity mapping files.
    * Handles database operations using the Dapper ORM.

14. Sportsbook.MatchConsumer.Tests
    * This is the test project for Sportsbook.MatchConsumer.
    * It contains integration tests for the functionality provided in the MatchConsumer project.

15. Sportsbook.Docker (Directory):
    * Contains Docker-related files to containerize the application components.
    * The DockerCompose.yml file defines services like RabbitMQ and Redis for container orchestration.

# Sportsbook Demo Solution

## Before Starting

Before running the projects, navigate to the Sportsbook.Docker folder and run the 'CreateAndRun.bat' file.
This will create and run the docker containers (RabbitMQ and Redis).

## General Information

While developing the project, paid attention to clean code writing principles such as SOLID, KISS, YAGNI, DRY.

RabbitMQ was used as Message Broker and MassTransit as library.

It was kept simple, especially because database-related and data operations are not clear.
No complicated design was done. Not even a relationship between tables was created, in-memory SQLite was used.

Unit Test was not used because it was not needed. But Integration Test was used.

Added a simple Exception Handling.
Assessment Project

Overview

This project is a .NET solution developed for an assessment. It follows a basic Clean Architecture structure to maintain separation of concerns and ensure maintainability.

Project Structure

The solution is structured using Clean Architecture principles, dividing responsibilities across layers:

Domain: Contains core business logic and entities.

Application: Houses use cases and service interfaces.

Infrastructure: Provides implementations for data access and external integrations.

Presentation: The API or UI layer exposing functionality to consumers.

Design Considerations

Due to the simplicity of this assessment project:

We do not address non-functional requirements (e.g., security, scalability, logging, error handling beyond basic needs).

We adhere to the Keep It Simple principle, focusing only on the necessary functionality to demonstrate the core logic.

Getting Started

Clone the repository.

Open the solution in Visual Studio or your preferred IDE.

Restore dependencies using dotnet restore.

Build and run the project using dotnet run.

Notes

The project is intended for assessment purposes only and is not optimized for production use.

Future improvements could involve handling cross-cutting concerns like authentication, logging, and validation in a more structured manner.

The docker file  is simple and we do not include a docker compose for database container etc.

The code is self-expainatory and comments are added only in points where we need extra clarification.

We included only basic unit test coverage. All unit test is from generated code and not hand coded. 
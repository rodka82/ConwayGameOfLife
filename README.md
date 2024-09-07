
# Conway's Game of Life API

This project is an implementation of Conway's Game of Life. 
It has endpoints to manage board states and simulate the game.
Additionally, there is a console app simulator to demonstrate the game's functionality.

## Table of Contents

1. [Requirements](#requirements)
2. [API Overview](#api-overview)
3. [Simulator](#simulator)
4. [Architecture](#architecture)
5. [How to Run](#how-to-run)
6. [Production-Readiness Considerations](#production-readiness-considerations)

## Requirements

- .NET 7.0 SDK
- SQLite 

## API Overview

The API provides the following functionalities:

1. **Upload a new board state**  
   Endpoint: `POST /api/board`  
   Description: Uploads a new board state and returns the ID of the board.  
   Request: JSON representation of the initial board state.  
   Response: Board ID.

2. **Get the next state for a board**  
   Endpoint: `GET /api/board/{id}/next`  
   Description: Returns the next state of the board with the specified ID.

3. **Get X states away for a board**  
   Endpoint: `GET /api/board/{id}/states/{x}`  
   Description: Returns the board state after X generations.

4. **Get the final state for a board**  
   Endpoint: `GET /api/board/{id}/final?steps={x}`  
   Description: Returns the final state of the board. If the board doesn't reach a conclusion after X generations, returns an error.

### Error Handling
- If a board doesn't reach a conclusion within the specified number of steps, the API will return a `400 Bad Request` error with a message explaining the failure.

### Persistence
- Board states are stored persistently in a SQLite database. The API is resilient to restarts or crashes, and previously uploaded boards are retained.

## Simulator

The project includes a console-based simulator for Conway's Game of Life. This allows users to run simulations and visualize the state transitions on the console.

To run the simulator, use the following command:

```bash
dotnet run --project Simulator
```

## Architecture

This project is divided into the following layers:

1. **API Layer (`ConwayGameOfLife.API`)**  
   This handles HTTP requests and routes them to the appropriate service methods.
   
2. **Business Layer (`ConwayGameOfLife.Business`)**  
   Contains the business logic for managing board states and calculating transitions. 

2. **Domain Layer (`ConwayGameOfLife.Domain`)**  
   Contains the project domain entities.
   
4. **Data Layer (`ConwayGameOfLife.Infra.Data`)**  
   This handles persistence, using Entity Framework Core with SQLite for storing board states. Migrations and database management are done in this layer.

5. **Simulator (`Simulator`)**  
   A console application that simulates Conway's Game of Life interactively.

### Key Design Decisions

- **Persistence:** I chose SQLite to ensure state persistence, so the game can be resumed after a restart or crash.
- **Separation of Concerns:** The business logic is isolated, allowing for easier unit testing and maintainability.
- **Resilience:** The API is designed to handle service restarts and retain board states, ensuring it is robust for real-world production use.

## How to Run

1. **Clone the repository**  
   ```bash
   git clone https://github.com/rodka82/ConwayGameOfLife.git
   ```

2. **Run Migrations**  
   Ensure that the database is set up with the correct schema by running the migrations:
   ```bash
   dotnet ef database update --project ConwayGameOfLife.Infra.Data
   ```

3. **Run the API**  
   ```bash
   dotnet run --project ConwayGameOfLife.API
   ```

4. **Run the Simulator**  
   To launch the simulator, execute the following:
   ```bash
   dotnet run --project Simulator
   ```

## Production-Readiness Considerations

- **Error Handling:** All endpoints return appropriate HTTP status codes with descriptive error messages.
- **Persistence:** SQLite ensures the API retains board states even in the event of a crash or restart.
- **Scalability:** The use of dependency injection and well-defined layers makes it easy to extend the API for more complex use cases.
- **Logging:** Logging is implemented to track important events and errors, which is essential for monitoring and debugging in production environments.

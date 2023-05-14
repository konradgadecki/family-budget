# Family Budget
Web app for creating a list of any number of budgets and share it with any number of users.
 
 ## Quick start

The startup application consists of three parts: frontend, backend, and database. Users can create their own accounts and log in to the app using one of the following accounts: marek@gmail.com or karol@gmail.com.

## Frontend

The frontend part of the application is built using HTML, CSS, React and TypeScript.

### Getting Started

To run the frontend, follow these steps:

1. Navigate to the `web/` directory: `cd web/`
2. Install dependencies: `npm install`
3. Start the development server: `npm start`

The frontend will be accessible at `http://localhost:3000`.

## Backend

The backend part of the application is built using .NET and runs in Visual Studio.

### Getting Started

To run the backend, follow these steps:

1. Open the project in Visual Studio.
2. Build the solution.
3. Start the application using the appropriate run configuration.

### Database

The database for the application is managed using Docker Compose.

#### Prerequisites

Make sure you have Docker installed on your machine.

#### Getting Started

To start the database, follow these steps:

1. Open a terminal.
2. Navigate to the project's root directory.
3. Run the following command: `docker-compose up -d`

This will start the PostgreSQL database in a Docker container.

## License

This project is licensed under the [MIT License](./LICENSE).

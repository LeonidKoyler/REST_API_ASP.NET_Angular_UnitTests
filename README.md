# Calculation Tool

This project consists of a REST API backend and two frontend options: Angular and ASP.NET Core. The backend handles the core logic and calculations, while the frontend provides a user interface to interact with the API.

## Features

- REST API built with ASP.NET Core for calculation services.
- Frontend implemented in both Angular and ASP.NET Core.

## Setup of the Project(from Visual Studio)

### Backend

1. Right-click on the `Calculation.API` project in Visual Studio.
2. Select `Debug > Run Without Debugging`.

### Frontend

There are two options for the frontend: ASP.NET Core or Angular. Choose one of the following methods based on your preference.

#### ASP.NET Core Frontend

1. In Visual Studio, expand the `Razor` folder, and right-click on the `Calculation.Tool` project.
2. Select `Run Without Debugging` to start the ASP.NET Core frontend.

#### Angular Frontend

1. Install dependencies: Open the Developer PowerShell or terminal, navigate to `...\Frontend_Angular\Angular.Calculation.Tool` and run the following command:
   ```sh
   npm install

2. After the installation is complete, start the Angular frontend by running:
   ```sh
   npm start

## Docker and Docker Compose for Containerized Development

## Prerequisites

Docker installed on your machine. You can download it from [here](https://www.docker.com/get-started) 

Docker Compose installed. For installation instructions, visit Docker Compose installation [Docker Compose installation](https://docs.docker.com/compose/install/).

In the project directory (where docker-compose.yaml is located), run the following command to build/run the images using Docker Compose:
 ```sh
docker-compose build
docker-compose up

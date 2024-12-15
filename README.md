__Project Name: Calculation Tool__
This project consists of a REST API backend and two frontend options: Angular and ASP.NET Core. 
The backend handles the core logic and calculations, while the frontend provides a user interface to interact with the API.

__Features:__
  * REST API built with ASP.NET Core for calculation services.
  * Frontend implemented in both Angular and ASP.NET Core.

__Setup of the project__
Backend: Right-click on the Calculation.API project in Visual Studio.
Select Debug > Run Without Debugging.

__Frontend__
There are two options for the frontend: ASP.NET Core or Angular. 
Choose one of the following methods based on your preference.

__ASP.NET Core Frontend__

In Visual Studio, expand the Razor folder, and right-click on the Calculation.Tool project.
Select Run Without Debugging to start the ASP.NET Core frontend.

__Angular Frontend__

Install dependencies:
Open the Developer PowerShell or terminal, navigate to ...\Frontend_Angular\Angular.Calculation.Tool 
and run the following command (npm install) to install the required npm packages.
After the installation is complete, start the Angular frontend by running: npm start 

__Docker and Docker Compose for containerized development__
## Prerequisites

- Docker installed on your machine. You can download it from [here](https://www.docker.com/get-started).
- Docker Compose installed. For installation instructions, visit [Docker Compose installation](https://docs.docker.com/compose/install/).

## Setting Up the Project

Follow these steps to get the project up and running using Docker Compose.
Navigate to the folder where docker-compose.yaml installed.

In the project directory, run the following command to build the images using Docker Compose:
docker-compose build

Once the images are built, you can start the project using the following command:
docker-compose up

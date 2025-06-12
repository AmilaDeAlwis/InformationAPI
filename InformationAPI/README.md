## InformationAPI - ASP.NET Core Web API

### Features
- Fetch post by ID or fetch all posts
- Caches posts from external API into SQL Server
- REST API with pagination support
- Simple HTML + JavaScript frontend
- SQL scripts included for easy DB setup
- Docker support for SQL Server

### Getting Started

## 🧰 Tech Stack

- **Backend**: ASP.NET Core 8 Web API
- **Database**: SQL Server
- **Frontend**: HTML, CSS, JavaScript (Vanilla)
- **Docker**: For containerizing SQL Server

#### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) 

## Project Structure
InformationAPI/
│
├── Controllers/
│ └── HomeController.cs
├── Impl/
│ └── ManageService.cs and ManageConnection.cs
├── Interfaces/
│ └── IManageService.cs and IManageConnection
├── Models/
│ └── InformationModel.cs
│
├── Scripts/
│ └── schema.sql # SQL script to create necessary tables
│
├── wwwroot/
│ ├── index.html # Frontend entry point
│ ├── style.css
│ └── script.js
│
├── docker-compose.yml
└── README.md

#### Setup
1. Clone the repo:
Create a folder: 
```bash
   mkdir InformationAPI && cd InformationAPI
   ```
Clone the repo:  
```bash
   git clone https://github.com/your-username/InformationAPI.git
   ```
Open the .gitignore file add the followings
	appsettings.json
	appsettings.Development.json
	.env

2. Set up the docker desktop:
Create a .env file and add a password.
      Example: SQL_SA_PASSWORD=Your-StrongPassword
Compose the server: bash/cmd => docker compose up -d
Open Docker Desktop → You should see the container InformationAPI-sql running
Run InformationAPI-sql

3. Run SQl Script:
Open SQL Server Management Studio (SSMS).
Connect using:
    Server: localhost,14300
    Login: sa
    Password: Your-StrongPassword

Create a DB:
```bash
   sqlcmd -S localhost,14300 -U sa -P "Your-StrongPassword" -Q "CREATE DATABASE YourDatabaseName"
   ```
Create a table:
```bash
   sqlcmd -S localhost,14300 -U sa -P "Your-StrongPassword" -d YourDatabaseName -i Scripts/schema.sql
   ```
          
4. Add the ConnectionString to the appsettings.Development.json file:
       Example: "ConnectionStrings": {
                     "DefaultConnection": "Server=localhost,14300;Database=YourDatabaseName;User Id=sa;Password=Your-StrongPassword;TrustServerCertificate=True;"
                }

5. Add the 3rd API URL into the appsettings.json file:
       Example: "ExternalApi": {
                     "BaseUrl": "https://jsonplaceholder.typicode.com/posts"
                }

6. Build and Run:
Open the terminal in the project directory.
Build the project:
   ```bash
   dotnet build
   ```
Run the application: 
   ```bash
   dotnet run
   ```

### Output
get a list of records: https://localhost:7052/
get a single record by ID: https://localhost:7052/api/home/1
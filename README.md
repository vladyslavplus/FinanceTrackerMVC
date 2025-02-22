# Finance Tracker

Finance Tracker is a web application for managing finances, built with ASP.NET Core MVC using Clean Architecture.

## 📷 Project Screenshots
See screenshots of the project here: [Finance Tracker Images](https://imgur.com/a/finance-tracker-project-by-vladyslav-perevispa-asp-net-core-mvc-IlojimR)

## 🔥 Key Technologies
- **ASP.NET Core MVC** – backend framework
- **Entity Framework Core** – database management (MSSQL)
- **ASP.NET Identity** – authentication and role-based authorization
- **Bootstrap CSS** – UI styling
- **JavaScript & jQuery** – interactivity
- **Toastr.js** – notifications
- **Syncfusion Components** – advanced UI components
- **Clean Architecture** – structured approach

## 📂 Project Structure
- `FinanceTracker.Application` – business logic
- `FinanceTracker.Domain` – domain models
- `FinanceTracker.Infrastructure` – database and external service interactions
- `FinanceTracker.Web` – web interface (MVC)
- `Services` – additional services

## ⚡ Features
- Income and expense management
- Categorization of financial transactions
- User authentication with role-based system
- Data visualization
- Transaction notifications

## 🚀 How to Run
1. Clone the repository:  
   ```sh
   git clone https://github.com/yourusername/FinanceTracker.git
   ```
2. Configure the MSSQL connection in `appsettings.json`
3. Apply migrations using:  
   ```sh
   dotnet ef database update
   ```
4. Run the application:  
   ```sh
   dotnet run --project FinanceTracker.Web
   ```

## 📜 License
MIT License


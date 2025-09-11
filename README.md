# 📘 WorklogApp

A **Blazor Server Application** built with **.NET 8** and **MS SQL Server** that helps organizations track worklogs, manage projects, and generate reports.  
The app supports multiple roles — **Admin**, **Manager**, and **Employee**.

---

## ✨ Features

- 🔑 **Authentication & Authorization** with role-based access  
- 👤 **Admin Panel**
  - Create new users and ssign them roles.  
  - Manage previously created users (edit/delete).  
- 📂 **Manager Dashboard**
  - Create and manage projects.
  - Assign employees to projects.
  - Approve/Reject employees worklogs.
  - View and filter employee worklogs.  
  - Export reports to Excel.
- 📝 **Employee Portal**
  - Submit daily worklogs (online, offline, and other hours). 
  - Status tracking of submitted logs.  
- 📊 **Reporting**
  - Filter by project, employee, status, and date range  
  - Search by employee ID or project name  
  - Export to Excel with caching for optimized performance  

---

## 🛠️ Tech Stack

- **Frontend & Backend:** Blazor Server (.NET 8.0)  
- **Database:** Microsoft SQL Server  
- **ORM:** Entity Framework Core  
- **Styling:** Custom inline styles + color palette (`#e2cfea`, `#a06cd5`, `#6247aa`, `#102b3f`, `#062726`)  
- **Export:** EPPlus (Excel generation)  

---

## 🚀 Getting Started

### 1️⃣ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  
- Visual Studio 2022 

### 2️⃣ Clone Repository
```bash
git clone https://github.com/RohitStartbit2024/WorklogApp.git
cd WorklogApp
```

### 3️⃣ Database Setup
- Update `appsettings.json` with your SQL Server connection string.
- Run EF Core migrations:
```bash
dotnet ef database update
```

### 4️⃣ Run Application
```bash
dotnet run
```

## 📦 Deployment

- Publish using:
```bash
dotnet publish -c Release
```
- Deploy to your server (IIS, Linux, or containerized).  

---

## 👥 Roles & Access

- **Admin:** Manage  users.  
- **Manager:** Create projects, review employee logs, generate reports.  
- **Employee:** Submit daily logs, track status.  

# 🧩 Task Manager API

A RESTful Web API built with **ASP.NET Core 6** and **Entity Framework Core** for managing tasks, projects, users, and comments.  
It provides full CRUD operations, authentication, caching, logging, and follows a layered architecture for clean and scalable code.

---

## 1️⃣ Domain & Entities

- **User** → `id`, `name`, `email`, `passwordHash`, `role`  
- **Project** → `id`, `name`, `description`, `ownerId`  
- **Task** → `id`, `title`, `description`, `status` (`ToDo`, `InProgress`, `Done`), `priority`, `assignedUserId`, `projectId`, `dueDate`  
- **Comment** → `id`, `taskId`, `userId`, `content`, `createdAt`  

---

## 2️⃣ Core Features

1. **CRUD** → Create, Read, Update, Delete for all entities  
2. **Pagination + Sorting + Filtering** → Task and Project lists  
3. **JWT Authentication & Role-based Authorization**  
   - **Admin** → full access  
   - **Project Owner** → own projects & tasks  
   - **User** → only assigned tasks + own comments  
4. **Cache** → Task and Project lists (MemoryCache or Redis)  
5. **Logging + Global Exception Handling** → full pipeline  
6. **Swagger / OpenAPI** → auth header + example requests  

---

## 3️⃣ Advanced / Optional Features

- **Soft Delete** → Tasks and Projects are not physically removed from DB  
- **Due Date Notification** → background job (Hangfire or Quartz) for upcoming deadlines (mocked log or email)  
- **Search** → Task title and description search  

---

## 🚀 Technologies Used

- **.NET 6 / ASP.NET Core Web API**  
- **Entity Framework Core 6**  
- **SQL Server**  
- **Repository–Service–Controller architecture**  
- **Dependency Injection (IoC)**  
- **Swagger (OpenAPI)**  
- **JWT Authentication**  
- **MemoryCache / Redis (optional)**  

---

## 🧠 Purpose

This project is designed to **practice backend development skills** and demonstrate clean architecture principles, authentication, caching, logging, and background tasks in ASP.NET Core 6.

---

## 🧍 Author

**Batuhan** — SQL & Data Analyst | Software Support Engineer | Aspiring Backend Developer  
Currently building real-world projects to sharpen **.NET Web API** skills and prepare for full-stack development.

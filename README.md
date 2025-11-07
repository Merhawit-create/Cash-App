# Cash-App

# Cash-App

A simple Blazor WebAssembly app for creating accounts, transferring money, and viewing transaction history.  
All data is saved locally in the browser.

---

##  Project Purpose

This project was built to demonstrate understanding of C#, Blazor components, dependency injection, and local data storage.  
It simulates a small “banking” system completely in the browser.

---

##  Technical Design & Rationale

### Why Blazor WebAssembly?
- Allows writing **C# instead of JavaScript** on the frontend.  
- Easy to share logic between UI and backend.  
- Runs fully in the **browser**, ideal for small demos without a real server.

### Why use localStorage for saving data?
- Simple way to **persist data** between page reloads.  
- Works without an internet connection.  
- No need for a backend database, which makes it easier to test and deploy.

### Why use interfaces (`IAccountServices`, `IStorageService`, `IBankAccount`)?
- Makes the app **modular and testable**.  
- UI doesn’t depend directly on implementation details.  
- Future-proof: you could replace `localStorage` with a real database later.

### Why a domain model with Account and Transaction classes?
- Separates **business logic** from UI code.  
- Makes features like **transaction history** and **balance tracking** easy to implement.  
- Encourages clean architecture and reusable components.

### Why JSON serialization with `System.Text.Json`?
- Built into .NET — no extra dependencies.  
- Works well with Blazor and JavaScript interop.  
- Using `CamelCase` and `JsonStringEnumConverter` makes data readable and consistent.

### Why simple PIN login?
- Demonstrates how to handle **form input and validation** in Blazor.  
- Shows basic navigation and service calls.  
- Not meant for real security, but for learning.

---

##  Main Components

- `Accountservices`: Handles account creation, transfers, and saving data.  
- `StorageServicecs`: Handles saving/loading JSON in localStorage.  
- `Bankacount`: Core domain class representing a bank account.  
- `Transaction`: Represents one money movement.  
- Razor pages:
  - `/CreateAccount`
  - `/Transfer`
  - `/History`
  - `/Home`
  - `/` (Login)

---

##  Running the App

1. Restore and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```

2. Run the app:
   ```bash
   dotnet run
   ```

3. Open the shown URL (usually `https://localhost:****`)  
   Login with the demo PIN: `12345`

4. Try:
   - Creating accounts  
   - Depositing, withdrawing, or transferring  
   - Viewing history and sorting transactions

---

##  Future Improvements

- Replace `localStorage` with a real database.  
- Add real user authentication instead of a fixed PIN.  
- Add charts and statistics for accounts.  
- Support multiple users.  
- Improve UI with better design and accessibility.

---

##  Summary

This project shows how to build a **fully client-side financial app** in Blazor using only C#.  
It demonstrates clean architecture with interfaces, data validation, and local persistence.  
The technical choices were made for **simplicity, clarity, and reusability**.

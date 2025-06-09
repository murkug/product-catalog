# Product Catalog Full Stack Application

## Technology Stack

### Backend
*   **.NET 8** & C#
*   **ASP.NET Core Web API**
*   **Entity Framework Core 8**
*   **SQLite**

### Frontend
*   **Vue 3**
*   **Vue Router**
*   **Vite**
*   **HTML & CSS**

## Project Structure

The repository is organized into two main folders:
```
/
├── ProductCatalogBackend/
└── ProductCatalogFrontend/
```

## Prerequisites

Before you begin, ensure you have the following installed on your system:

*   **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
*   **[Node.js](https://nodejs.org/)** 

## Local Setup and Running the Application

To run the full application, you must start both the backend server and the frontend development server.

---

### Step 1: Clone the Repository

First, get the code onto your local machine.

```bash
git clone https://github.com/murkug/product-catalog.git
cd product-catalog
```

---

### Step 2: Set Up and Run the Backend API

1.  **Open your first terminal window.**
2.  Navigate into the backend project directory:
    ```bash
    cd ProductCatalogBackend
    ```
3.  **Trust the local HTTPS development certificate.** This is a one-time step that allows your browser to trust the local server.
    ```bash
    dotnet dev-certs https --trust
    ```
    You may need to accept a security prompt.

4.  **Run the backend server** using the specific `https` launch profile.
    ```bash
    dotnet run --launch-profile https
    ```

5.  **Verification:** The terminal should show output indicating that the server is running and listening on both HTTP and HTTPS ports. Look for lines like this:
    ```
    info: Microsoft.Hosting.Lifetime[14]
          Now listening on: https://localhost:7123
    info: Microsoft.Hosting.Lifetime[14]
          Now listening on: http://localhost:5156
    ```
    The application will also automatically create the `productcatalog.db` database file and populate it with initial data on the first run.

**Leave this terminal running.**

---

### Step 3: Set Up and Run the Frontend Application

1.  **Open a second, separate terminal window.**
2.  Navigate into the frontend project directory from the root folder:
    ```bash
    cd ProductCatalogFrontend
    ```
3.  **Install the necessary Node.js dependencies:**
    ```bash
    npm install
    ```
4.  **Start the frontend development server:**
    ```bash
    npm run dev
    ```

5.  **Verification:** The terminal will show you the URL where the frontend is being served. It will look like this:
    ```
      ➜  Local:   http://localhost:5173/
    ```

---

### Step 4: Use the Application

1.  Open your web browser (Chrome, Firefox, Edge, etc.).
2.  Navigate to the local URL provided by the frontend server: **http://localhost:5173**

You should now see the "Products List" page, with data loaded from your running backend API.

## Backend API Endpoints

If you wish to test the backend API directly, you can use a tool like Postman or `curl`. The base URL will be `https://localhost:7123`.

*   `GET /api/products`: Get a list of all products for the table view.
*   `POST /api/products`: Create a new product.
*   `GET /api/products/types`: Get a list of available product types for the form.
*   `GET /api/products/colours`: Get a list of available colors for the form.

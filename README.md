# Restaurant App - ASP.NET Core MVC

This is a sample **Restaurant Management** web application built using **ASP.NET Core MVC**. The project is based on the full course tutorial by [Evan Gudmestad](https://www.youtube.com/watch?v=q9X3SDEZtpw).

## Overview

The app allows managing ingredients, products, and orders in a restaurant scenario. It demonstrates:

- ASP.NET Core MVC architecture
- Entity Framework Core with SQLite database
- User authentication and authorization with Identity
- CRUD operations for Ingredients and Products
- Shopping cart and order management
- File uploads (images for products)
- Bootstrap 5 for UI styling
- Session management
- Dependency Injection and Repository pattern

---

## Features

- **User Authentication**: Register, Login, Logout
- **Ingredient Management**: Add, Edit, Delete ingredients
- **Product Management**: Add, Edit, Delete products with categories and multiple ingredients
- **Shopping Cart**: Add products with quantity, view cart
- **Order Placement & Viewing**: Place orders and view past order history
- **Image Uploads**: Upload product images and display them
- **Responsive UI**: Built with Bootstrap 5

---

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) or later
- [SQLite](https://sqlite.org/index.html) (included via EF Core)
- IDE: Visual Studio 2022 / VS Code / Rider
- Node.js (optional, if you want to manage frontend packages)

---

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/CharakaJith/restaurant-app-dotnet-mvc.git
   cd restaurant-app-dotnet-mvc
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Apply database migrations**

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:7090` (or the URL displayed in the console).

---

## Project Structure

- **Controllers/** – MVC controllers handling HTTP requests
- **Models/** – Entity and View Models
- **Data/** – DbContext and database migrations
- **Services/** – Business logic and data access abstractions
- **Views/** – Razor views and partials for UI
- **wwwroot/** – Static assets including CSS, JS, and images
- **Areas/Identity/** – Authentication and user management UI and logic

---

## Notes

- Image uploads are stored in `wwwroot/images`.
- User sessions store shopping cart data temporarily.
- Authorization restricts some pages to logged-in users.

---

## Learn More

- [ASP.NET Core MVC Documentation](https://learn.microsoft.com/en-us/aspnet/core/mvc/)
- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.0/getting-started/introduction/)

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## Acknowledgements

- [Programming with Mosh](https://www.youtube.com/@programmingwithmosh) for the original tutorial
- ASP.NET Core and EF Core teams at Microsoft

---

## Contact
Email: [charaka.info@gmail.com](mailto:charaka.info@gmail.com) | LinkedIn: [Charaka Jith Gunasinghe](https://www.linkedin.com/in/charaka-gunasinghe/)


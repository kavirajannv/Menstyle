# MenStyle – Men's Dress E-Commerce Website

## Overview

MenStyle is a production-ready e-commerce web application developed using ASP.NET Core MVC (.NET 10 LTS), Entity Framework Core, and SQL Server.

The system allows users to browse products, add items to cart, complete secure checkout using Razorpay payment integration, and manage their orders. Admin users can manage products, categories, and customer orders.

---

## Technology Stack

- ASP.NET Core MVC (.NET 10 LTS)
- Entity Framework Core
- SQL Server
- ASP.NET Identity (Authentication & Authorization)
- Razorpay Payment Gateway
- HTML5, CSS3, Bootstrap 5
- IIS / Azure App Service (Deployment)

---

## Architecture

The application follows a three-layer architecture:

1. Presentation Layer (Controllers and Views)
2. Business Logic Layer (Services)
3. Data Access Layer (Entity Framework Core)

---

## Features

### User Features
- User Registration and Login
- Browse and Search Products
- Add to Cart and Update Quantity
- Secure Checkout with Razorpay
- Real-Time Payment Processing
- Order History

### Admin Features
- Product Management (CRUD)
- Category Management
- Order Management
- Payment Status Monitoring

---

## Payment Integration

Razorpay is integrated for secure online transactions.

Payment Flow:
1. User initiates checkout.
2. Backend creates Razorpay order.
3. Razorpay checkout popup is displayed.
4. User completes payment.
5. Payment signature is verified on the server.
6. Order and payment details are stored in the database.
7. Cart is cleared after successful payment.

Security Measures:
- Razorpay signature verification
- Orders created only after successful payment validation
- HTTPS enforcement
- Role-based access control

---

## Database Tables

- Products
- Categories
- CartItems
- Orders
- OrderDetails
- Payments
- AspNetUsers

---

## Installation

1. Clone the repository:
   git clone https://github.com/yourusername/menstyle.git

2. Configure:
   - Update SQL Server connection string in appsettings.json
   - Add Razorpay KeyId and KeySecret

3. Apply migrations:
   dotnet ef database update

4. Run the application:
   dotnet run

---
<img width="1920" height="1080" alt="Screenshot (3)" src="https://github.com/user-attachments/assets/5c37a1bd-db1c-4d40-87f0-df0ee81b9879" />
<img width="1920" height="1080" alt="Screenshot (4)" src="https://github.com/user-attachments/assets/dccec386-07f1-467d-8493-d18ded2f8448" />

<img width="1920" height="1080" alt="Screenshot (5)" src="https://github.com/user-attachments/assets/60ba930d-b827-40c9-85f3-0cc6a096ee27" />

<img width="1920" height="1080" alt="Screenshot (6)" src="https://github.com/user-attachments/assets/5001a3bb-c00f-416f-8f66-a554e07b2c0e" />

<img width="1920" height="1080" alt="Screenshot (7)" src="https://github.com/user-attachments/assets/32aaf98f-52f9-4254-9a8b-59f65fba084d" />
<img width="1920" height="1080" alt="Screenshot (13)" src="https://github.com/user-attachments/assets/0577fa80-1725-4f87-aa87-07b3c4fa556b" />
<img width="1920" height="1080" alt="Screenshot (14)" src="https://github.com/user-attachments/assets/50e9ab5c-13e5-409d-89c0-3525284c2f97" />
<img width="1920" height="1080" alt="Screenshot (15)" src="https://github.com/user-attachments/assets/f975b6a6-5c21-4fd7-8279-cdc79b12b7e2" />
<img width="1920" height="1080" alt="Screenshot (16)" src="https://github.com/user-attachments/assets/723bdbc2-b0ea-49c0-9980-ea3f52f31189" />
<img width="1920" height="1080" alt="Screenshot (18)" src="https://github.com/user-attachments/assets/fb14f0fc-9ac3-4a67-a2ea-51f9580cee96" />
<img width="1920" height="1080" alt="Screenshot (19)" src="https://github.com/user-attachments/assets/e9edba37-b372-4be3-bd7f-cb4e8b7cbcac" />
<img width="1920" height="1080" alt="Screenshot (20)" src="https://github.com/user-attachments/assets/bae1b8ac-ade3-4467-97d2-c1084007cfa3" />

<img width="1920" height="1080" alt="Screenshot (21)" src="https://github.com/user-attachments/assets/f1519bf0-d628-4aa7-bc1e-776a4c88ad45" />

<img width="1920" height="1080" alt="Screenshot (22)" src="https://github.com/user-attachments/assets/4164d220-13c2-4100-a084-210eaa712de6" />
<img width="1920" height="1080" alt="Screenshot (23)" src="https://github.com/user-attachments/assets/30275022-a645-4b4f-b8e2-1647fd1c3deb" />



## Deployment

The application can be deployed using:
- IIS
- Azure App Service
- Azure SQL Database

---

## Future Improvements

- Email Notifications
- Coupon System
- Invoice PDF Generation
- REST API for Mobile Application
- Multi-Payment Gateway Support

---

## Author

Kavirajan  
ASP.NET Core Developer

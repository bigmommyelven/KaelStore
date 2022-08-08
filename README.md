# KaelStore

Project KaelStore Test

## Installation

Untuk inisiasi pertama, run command pada Package Manager Console dan pilih Default Project: KaelStore.Persistence
```bash
PM> Add-Migration Initial-Commit-Application -Context ApplicationDbContext -o Migrations/Application
PM> Add-Migration Initial-Commit-Identity -Context IdentityContext -o Migrations/Identity

PM> Update-Databasee -Context ApplicationDbContext
PM> Update-Databasee -Context IdentityContext 
```

## Usage

Untuk setiap action pada route, diperlukan Authorization Bearer JwtToken
```
[Post] /api/Account/authenticate
{
  "email": "superadmin@gmail.com",
  "password": "Password@123"
}
```
#### Result
```
{
  "status": "Success",
  "data": {
    "id": "34bb7bea-dcfb-404c-8405-ea7e4ed68a2d",
    "userName": "superadmin",
    "email": "superadmin@gmail.com",
    "roles": [
      "Admin",
      "Basic",
      "Moderator",
      "SuperAdmin"
    ],
    "isVerified": true,
    "jwToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdXBlcmFkbWluIiwianRpIjoiYzlkNGU1OWYtYTY1YS00YzgxLTk4NDMtOTVjNTBjOTgxNzU0IiwiZW1haWwiOiJzdXBlcmFkbWluQGdtYWlsLmNvbSIsInVpZCI6IjM0YmI3YmVhLWRjZmItNDA0Yy04NDA1LWVhN2U0ZWQ2OGEyZCIsImlwIjoiMTkyLjE2OC4xLjYiLCJyb2xlcyI6WyJBZG1pbiIsIkJhc2ljIiwiTW9kZXJhdG9yIiwiU3VwZXJBZG1pbiJdLCJleHAiOjE2NTg5NTY5MjQsImlzcyI6IklkZW50aXR5IiwiYXVkIjoiSWRlbnRpdHlVc2VyIn0.s2391rems_Co4O3QtJbNfqiEoM378suRYGmzWxdI_9c",
    "refreshToken": "91C4DF5EDFE8497BB5970AF014031989DCFF86AA3520B0BF40A5A910714D1E1C65C8E93E6D5A3E99"
  },
  "message": "Authenticated superadmin"
}
```

### 1. Data Master
```
# Kategori Barang
[Get] /api/v1/Category

# Barang
[Get] /api/v1/Product
```

### 2. Data Transaksi
```
# Stock
[Get] /api/v1/Product

# Transaksi Pembelian
[Get] /api/v1/Order
```

### 3. Buat CRUD API untuk Kategori Barang
#### Create
```
[Post] /api/v1/Category
{
  "categoryName": "string",
  "description": "string"
}
```
#### Read All
```
[Get] /api/v1/Category
```
#### Read By ID
```
[Get] /api/v1/Category/{id}
```
#### Update
```
[Put] /api/v1/Category/{id}
{
  "categoryName": "string",
  "description": "string"
}
```
#### Delete
```
[Delete] /api/v1/Category/{id}
```

### 3. Buat CRUD API untuk Barang
#### Create
```
[Post] /api/v1/Product
{
  "productName": "string",
  "categoryId": 0,
  "price": 0
}
```
#### Read All
```
[Get] /api/v1/Product
```
#### Read By ID
```
[Get] /api/v1/Product/{id}
```
#### Update
```
[Put] /api/v1/Product/{id}
{
  "productName": "string",
  "categoryId": 0,
  "price": 0
}
```
#### Delete
```
[Delete] /api/v1/Product/{id}
```

### 4. Buat API untuk Add Stock
#### Tambah Stock
```
[Post] /api/v1/ProductStock
{
  "id": 0,   // ProductId
  "stock": 0 // Jumlah
}
```
#### Lihat Stock
```
[Get] /api/v1/Product
```

### 4. Buat API untuk Pembelian / Order
#### Tambah Stock
```
[Post] /api/v1/Order
{
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 15
    },
    {
      "productId": 2,
      "quantity": 3
    },
  ]
}
```
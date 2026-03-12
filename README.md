# Reactivities

A social activities platform built with .NET 9 and React, allowing users to create, join, and manage social events.

## 🏗️ Architecture

This application follows **Clean Architecture** principles with clear separation of concerns:

- **Domain**: Core business entities and logic
- **Application**: Business logic, CQRS handlers (MediatR), and DTOs
- **Infrastructure**: External services (Cloudinary for photos, security)
- **Persistence**: Database context and migrations (SQL Server)
- **API**: ASP.NET Core Web API
- **Client**: React + TypeScript + Vite frontend

## 🚀 Tech Stack

### Backend
- **.NET 9.0** - ASP.NET Core Web API
- **Entity Framework Core** - ORM with SQL Server
- **MediatR** - CQRS pattern implementation
- **FluentValidation** - Input validation
- **ASP.NET Identity** - Authentication & authorization
- **SignalR** - Real-time comments
- **AutoMapper** - Object mapping
- **Cloudinary** - Photo storage

### Frontend
- **React 19** - UI library
- **TypeScript** - Type safety
- **Vite** - Build tool
- **MobX** - State management
- **TanStack Query** - Data fetching & caching
- **Material-UI (MUI)** - UI components
- **React Router** - Navigation
- **Axios** - HTTP client
- **React Hook Form + Zod** - Form handling & validation
- **Leaflet** - Maps integration
- **SignalR Client** - Real-time updates

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/) 
- [SQL Server](https://www.microsoft.com/sql-server) or [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd reactivities
```

### 2. Database Setup

#### Option A: Using Docker (Recommended)

```bash
# Copy the example override file
cp docker-compose.override.yml.example docker-compose.override.yml

# Edit docker-compose.override.yml and set a strong password
# Then start SQL Server
docker-compose up -d
```

#### Option B: Local SQL Server

Ensure SQL Server is running locally on port 1433.

### 3. Backend Configuration

```bash
cd API

# Copy the example appsettings
cp appsettings.example.json appsettings.Development.json

# Edit appsettings.Development.json and configure:
# - ConnectionStrings:DefaultConnection (if not using default)
# - CloudinarySettings (for photo uploads)
# - Authentication:GitHub (for GitHub OAuth)
```

**Required Configuration:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=reactivities;Integrated Security=True;TrustServerCertificate=True;Encrypt=False"
  },
  "CloudinarySettings": {
    "CloudName": "your-cloudinary-cloud-name",
    "ApiKey": "your-cloudinary-api-key",
    "ApiSecret": "your-cloudinary-api-secret"
  },
  "ClientAppUrl": "https://localhost:3002",
  "Authentication": {
    "GitHub": {
      "ClientId": "your-github-oauth-client-id",
      "ClientSecret": "your-github-oauth-client-secret"
    }
  }
}
```

### 4. Run Database Migrations

```bash
# From the API directory
dotnet ef database update
```

The database will be seeded with test data:
- **bob@test.com** / Password: `Pa$$w0rd`
- **tom@test.com** / Password: `Pa$$w0rd`
- **jane@test.com** / Password: `Pa$$w0rd`

### 5. Start the Backend

```bash
# From the API directory
dotnet run
```

The API will be available at: `https://localhost:5001`

### 6. Frontend Setup

```bash
cd client

# Install dependencies
npm install

# Create .env file (optional - default API URL is https://localhost:5001)
echo "VITE_API_URL=https://localhost:5001" > .env

# Start the development server
npm run dev
```

The frontend will be available at: `https://localhost:3002`

## 🧪 Running Tests

```bash
# Backend tests (when available)
dotnet test

# Frontend tests (when available)
cd client
npm test
```

## 🏃 Development Workflow

### Starting the Application

1. **Start Database** (if using Docker):
   ```bash
   docker-compose up -d
   ```

2. **Start Backend** (in one terminal):
   ```bash
   cd API
   dotnet run
   ```

3. **Start Frontend** (in another terminal):
   ```bash
   cd client
   npm run dev
   ```

### Creating a New Migration

```bash
cd API
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

### Building for Production

#### Backend
```bash
dotnet publish API/API.csproj -c Release -o ./publish
```

#### Frontend
```bash
cd client
npm run build
```

The built files will be in `client/dist` and are served by the API in production.

## 🔐 Security Notes

- **Never commit** `appsettings.Development.json` or `appsettings.Production.json` with real secrets
- **Never commit** `docker-compose.override.yml` with real passwords
- Use **strong passwords** for SQL Server in production
- Configure **CORS** appropriately for your production domain
- Enable **HTTPS** in production
- Store secrets in **Azure Key Vault**, **AWS Secrets Manager**, or similar in production

## 📝 Features

- ✅ User authentication (email/password + GitHub OAuth)
- ✅ Create, edit, and delete activities
- ✅ Join/leave activities
- ✅ Real-time comments using SignalR
- ✅ Photo upload with Cloudinary
- ✅ User profiles
- ✅ Follow/unfollow users
- ✅ Activity filtering and pagination
- ✅ Map integration for activity locations
- ✅ Responsive design

## 🏗️ Project Structure

```
reactivities/
├── API/                    # ASP.NET Core Web API
│   ├── Controllers/        # API endpoints
│   ├── Middleware/         # Custom middleware
│   ├── SignalR/           # SignalR hubs
│   └── DTOs/              # Data transfer objects
├── Application/           # Business logic layer
│   ├── Activities/        # Activity CQRS handlers
│   ├── Profiles/         # Profile handlers
│   ├── Core/             # Shared logic
│   └── Interfaces/       # Service contracts
├── Domain/               # Core entities
├── Infrastructure/       # External services
│   ├── Photos/          # Cloudinary service
│   └── Security/        # User accessor
├── Persistence/         # Database access
│   ├── Migrations/      # EF migrations
│   └── AppDbContext.cs  # DB context
└── client/              # React frontend
    └── src/
        ├── app/         # App configuration
        ├── features/    # Feature modules
        └── lib/         # Shared utilities
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 🐛 Known Issues

- None currently

## 📞 Support

For issues and questions, please open an issue on GitHub.

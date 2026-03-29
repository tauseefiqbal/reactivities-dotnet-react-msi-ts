# Reactivities

A full-stack social activity-management web application built with **ASP.NET Core 9** and **React 19** (TypeScript + Vite). Users can create, browse, attend, and comment on real-world events, manage their profiles, follow other users, and chat in real time.

---

## 📑 Table of Contents

- [Reactivities](#reactivities)
  - [📑 Table of Contents](#-table-of-contents)
  - [✅ Features](#-features)
    - [Activity Management](#activity-management)
    - [Social Features](#social-features)
    - [Account \& Authentication](#account--authentication)
    - [System Features](#system-features)
  - [🚀 Tech Stack](#-tech-stack)
    - [Backend](#backend)
    - [Frontend](#frontend)
  - [🌐 Deployment](#-deployment)
  - [🧪 Test and Admin User Credentials](#-test-and-admin-user-credentials)
    - [Test Users](#test-users)
    - [Admin User](#admin-user)
  - [👤 How to Use the App — Regular User](#-how-to-use-the-app--regular-user)
    - [Getting Started](#getting-started)
    - [Browsing and Managing Activities](#browsing-and-managing-activities)
    - [Creating an Activity](#creating-an-activity)
    - [Attending an Activity](#attending-an-activity)
    - [Real-Time Chat](#real-time-chat)
    - [Managing Your Profile](#managing-your-profile)
    - [Following Other Users](#following-other-users)
    - [Photo Management](#photo-management)
    - [Forgot Password / Reset Password](#forgot-password--reset-password)
  - [📋 Prerequisites](#-prerequisites)
  - [🛠️ Installation \& Setup](#️-installation--setup)
    - [1. Clone the Repository](#1-clone-the-repository)
    - [2. Database Setup](#2-database-setup)
    - [3. Backend Configuration](#3-backend-configuration)
    - [4. Run the Backend](#4-run-the-backend)
    - [5. Frontend Setup](#5-frontend-setup)
  - [🔧 Configuration Reference](#-configuration-reference)
  - [📁 Project Structure](#-project-structure)
  - [🔌 API Endpoints](#-api-endpoints)
    - [Account Endpoints](#account-endpoints)
    - [Activities Endpoints](#activities-endpoints)
    - [Profiles Endpoints](#profiles-endpoints)
    - [SignalR Hub](#signalr-hub)
  - [🐛 Troubleshooting](#-troubleshooting)
    - [Backend Won't Start](#backend-wont-start)
    - [Frontend Can't Connect to Backend](#frontend-cant-connect-to-backend)
    - [Photos Not Uploading](#photos-not-uploading)
    - [Password Reset Email Not Received](#password-reset-email-not-received)
    - [GitHub Login Not Working](#github-login-not-working)
  - [📄 License](#-license)
  - [👨‍💻 Author](#-author)

---

## ✅ Features

### Activity Management

- ✅ Browse a paginated dashboard of activities with filters (All / Going / Hosting, by date, by category)
- ✅ View full activity details including description, date/time, location, attendee list, and host
- ✅ Create a new activity with title, description, category, date/time, city, venue, and map pin (via LocationIQ)
- ✅ Edit or delete your own activities (only the host can modify)
- ✅ Cancel and uncancel an activity without deleting it

### Social Features

- ✅ Join or leave any activity with a single click
- ✅ Real-time per-activity chat powered by SignalR — messages appear instantly for all attendees
- ✅ Follow and unfollow other users; browse their followers and following lists
- ✅ Dedicated profile pages showing bio, photos, follower/following counts, and hosted/attended activities
- ✅ Upload and crop a profile photo (processed to 500×500 px via Cloudinary)
- ✅ Set any uploaded photo as your main avatar; delete photos you no longer want
- ✅ Edit your display name and bio at any time

### Account & Authentication

- ✅ Register with email address, display name, and password
- ✅ Log in with email and password
- ✅ Log in with GitHub OAuth (one-click, no password required)
- ✅ Sign out securely
- ✅ Change password from your account settings
- ✅ Forgot password — receive a reset-link email via Resend
- ✅ Reset password using the link sent to your email
- ✅ Email confirmation flow with verification link

### System Features

- ✅ Clean Architecture: API / Application / Domain / Infrastructure / Persistence layers
- ✅ CQRS pattern with MediatR — every operation is a distinct Command or Query
- ✅ FluentValidation pipeline behaviour for automatic input validation
- ✅ Global exception middleware — structured JSON errors (stack trace in dev only)
- ✅ Rate limiting middleware — 60 req/min general; 5 req/min on auth endpoints (per IP)
- ✅ Security response headers: `X-Frame-Options`, `X-XSS-Protection`, `Content-Security-Policy`, and more
- ✅ `IsActivityHost` authorisation policy — only the host can edit or delete their activity
- ✅ Database auto-seeded with sample users and activities on first run
- ✅ SPA fallback controller — serves the React frontend for all non-API routes in production

---

## 🚀 Tech Stack

### Backend

| Technology | Purpose |
|---|---|
| **ASP.NET Core 9** | Web API framework |
| **Entity Framework Core 9** | ORM and database migrations |
| **SQL Server** | Relational database |
| **ASP.NET Core Identity** | User management and cookie-based auth |
| **MediatR 14** | CQRS mediator pattern |
| **AutoMapper 16** | Object-to-object mapping |
| **FluentValidation 12** | Input validation with pipeline behaviour |
| **SignalR** | Real-time WebSocket communication |
| **CloudinaryDotNet** | Photo upload, crop, and storage |
| **Resend** | Transactional email (password reset, confirmation) |
| **Clean Architecture** | API / Application / Domain / Infrastructure / Persistence |

### Frontend

| Technology | Purpose |
|---|---|
| **React 19** | UI library (TypeScript) |
| **Vite 6** | Build tool and dev server (HTTPS via mkcert) |
| **React Router 7** | Client-side routing |
| **MUI Material 7** | Component library and theming |
| **MUI X DatePickers 8** | Date & time picker components |
| **TanStack React Query 5** | Server state management and caching |
| **MobX 6 + MobX React Lite** | Client-side reactive state |
| **Axios** | HTTP client |
| **@microsoft/signalr 10** | Real-time SignalR client |
| **React Hook Form 7 + Zod 4** | Form management and schema validation |
| **Leaflet / react-leaflet** | Interactive map with activity pin |
| **React Cropper** | In-browser photo cropping before upload |
| **React Dropzone** | Drag-and-drop photo upload zone |
| **date-fns 4** | Date formatting and manipulation |
| **React Toastify** | Toast notifications |
| **TypeScript 5** | Static type checking |

---

## 🌐 Deployment

The application is deployed and available at:

> **https://reactivities-wa.azurewebsites.net**

| Component | Details |
|---|---|
| **Platform** | Azure App Service |
| **Live URL** | https://reactivities-wa.azurewebsites.net |
| **Backend** | ASP.NET Core 9 — serves the API and the compiled React SPA |
| **Database** | SQL Server (Azure SQL) |
| **Photo Storage** | Cloudinary |
| **Email** | Resend (`Resend__ApiKey` / `Resend__FromEmail` environment variables) |

> **Azure config note:** Use double-underscore syntax for nested keys in environment variables (e.g. `Cloudinary__ApiKey`, `Resend__ApiKey`).

---

## 🧪 Test and Admin User Credentials

### Test Users

The following accounts are seeded automatically on first run. All users have the same capabilities — any user can create, host, attend, and manage activities.

| Display Name | Email | Password |
|---|---|---|
| Bob | `bob@test.com` | `Pa$$w0rd` |
| Tom | `tom@test.com` | `Pa$$w0rd` |
| Jane | `jane@test.com` | `Pa$$w0rd` |

Each test user has:
- **Sample activities** they are hosting or attending — visible on the Activity Dashboard
- **Attendance records** — visible in their profile under the Activities tab

### Admin User

This application does not currently have a separate admin role. All authenticated users share the same set of permissions. The only privilege distinction is the **IsActivityHost** policy — when a user creates an activity, they become the host and only they can edit, delete, or cancel that activity.

---

## 👤 How to Use the App — Regular User

### Getting Started

1. Open the application — use the [live deployment](https://reactivities-wa.azurewebsites.net) or the local dev server (default: `https://localhost:5173`).
2. Click **Login** in the nav bar and sign in with one of the test credentials above, or click **Register** to create a new account.
3. To sign in with GitHub, click the **GitHub** button on the login page.
4. After logging in you are taken to the **Activity Dashboard**.

### Browsing and Managing Activities

1. The **Activity Dashboard** shows a paginated list of upcoming activities.
2. Use the **Filters** panel on the right to switch between **All**, **Going**, and **Hosting** views.
3. Use the **calendar** in the filter panel to show only activities on or after a chosen date.
4. Click **View** on any activity card to open its full detail page.

### Creating an Activity

1. Click **Create Activity** in the nav bar.
2. Fill in the title, description, category, date/time, city, and venue.
3. Use the map component to drop a pin at the exact location (powered by LocationIQ).
4. Click **Submit** — your activity is published and you are automatically set as the host.

### Attending an Activity

1. Open any activity's detail page.
2. Click **Join Activity** to sign up as an attendee; your avatar appears in the attendee list immediately.
3. Click **Cancel attendance** to remove yourself from the activity.
4. If you are the host, click **Cancel Activity** to mark it cancelled (it stays visible but greyed out), or **Re-activate** to open it again.

### Real-Time Chat

1. On the activity detail page, scroll to the **Chat** panel.
2. Type your message and press **Enter** (or click the send button).
3. All attendees currently viewing the page see your message appear instantly — no page refresh needed.

### Managing Your Profile

1. Click your **avatar** in the nav bar and select your display name to open your profile page.
2. The **About** tab shows your bio — click **Edit Profile** to update your display name or bio.
3. The **Activities** tab lists activities you are **Hosting**, **Going to**, or have **Attended** in the past.

### Following Other Users

1. Open another user's profile by clicking their avatar or name anywhere in the app.
2. Click **Follow** to follow them; click **Unfollow** to stop following.
3. The **Followers** and **Following** tabs on any profile page show the complete follow graph for that user.

### Photo Management

1. Go to your own profile page and open the **Photos** tab.
2. Click **Add Photo**, drag and drop (or browse) an image file, crop it in the editor, then click **Upload**.
3. Click **Main** on any uploaded photo to set it as your profile avatar across the app.
4. Click the **delete** icon on a photo to permanently remove it (also deleted from Cloudinary).

### Forgot Password / Reset Password

1. On the **Login** page click **Forgot password?**.
2. Enter your registered email address and click **Send reset email**.
3. Check your inbox (and spam/junk folder) for an email containing a reset link.
4. Click the link — it opens the **Reset Password** page with your email and reset code pre-filled.
5. Enter your new password and confirm it, then click **Reset Password**.

---

## 📋 Prerequisites

Before running this application locally, ensure you have installed:

- **Node.js** v18 or higher
- **npm** v9 or higher
- **.NET SDK 9.0** or higher
- **SQL Server** (LocalDB, Express, or full)
- **Git**

Optional (required for the corresponding feature):
- A **Cloudinary** account — for photo upload (free tier is sufficient)
- A **Resend** account and API key — for password reset emails (free tier sends to your signup email)
- A **GitHub OAuth App** — for GitHub login

---

## 🛠️ Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/tauseefiqbal/reactivities-dotnet-react-msi-mui-ts.git
cd reactivities-dotnet-react-msi-mui-ts
```

### 2. Database Setup

The application targets **SQL Server**. The default connection string uses LocalDB:

```
Server=(localdb)\mssqllocaldb;Database=Reactivities;Trusted_Connection=True;
```

EF Core migrations run automatically on startup — no manual schema creation is required. Tables are created and seed data is inserted on first run.

### 3. Backend Configuration

Copy the example settings file and fill in your own values:

```bash
cp API/appsettings.example.json API/appsettings.Development.json
```

Open `API/appsettings.Development.json` and configure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=Reactivities;Trusted_Connection=True;"
  },
  "Cloudinary": {
    "CloudName": "YOUR_CLOUD_NAME",
    "ApiKey": "YOUR_API_KEY",
    "ApiSecret": "YOUR_API_SECRET"
  },
  "Resend": {
    "ApiKey": "YOUR_RESEND_API_KEY",
    "FromEmail": "onboarding@resend.dev"
  },
  "GitHub": {
    "ClientId": "YOUR_GITHUB_CLIENT_ID",
    "ClientSecret": "YOUR_GITHUB_CLIENT_SECRET"
  },
  "ClientAppUrl": "https://localhost:5173"
}
```

> `GitHub`, `Cloudinary`, and `Resend` sections are optional for basic local testing. The app starts without them, but the corresponding features will be unavailable.

### 4. Run the Backend

```bash
cd API
dotnet run
```

The API starts on `https://localhost:5001`. On first startup it applies EF migrations and seeds the database automatically.

### 5. Frontend Setup

Open a second terminal:

```bash
cd client
npm install
npm run dev
```

The dev server starts on `https://localhost:5173` (HTTPS via `vite-plugin-mkcert`). Accept the self-signed certificate prompt in your browser if asked.

---

## 🔧 Configuration Reference

| Key | Description | Required |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | SQL Server connection string | ✅ Yes |
| `Cloudinary:CloudName` | Cloudinary cloud name | Photo upload only |
| `Cloudinary:ApiKey` | Cloudinary API key | Photo upload only |
| `Cloudinary:ApiSecret` | Cloudinary API secret | Photo upload only |
| `Resend:ApiKey` | Resend API key for transactional email | Email features only |
| `Resend:FromEmail` | Sender address (`onboarding@resend.dev` on free tier) | Email features only |
| `GitHub:ClientId` | GitHub OAuth App client ID | GitHub login only |
| `GitHub:ClientSecret` | GitHub OAuth App client secret | GitHub login only |
| `ClientAppUrl` | Frontend origin used to build password-reset links | Email features only |

> **Azure deployment:** Replace `:` with `__` in environment variable names (e.g. `Cloudinary__ApiKey`, `Resend__ApiKey`).

---

## 📁 Project Structure

```
reactivities/
├── Reactivities.sln
├── API/                          # ASP.NET Core 9 Web API
│   ├── Controllers/              # AccountController, ActivitiesController,
│   │                             # ProfilesController, BuggyController, FallbackController
│   ├── DTOs/                     # RegisterDto, ChangePasswordDto, GitHubInfo
│   ├── Middleware/               # ExceptionMiddleware, RateLimitingMiddleware
│   ├── SignalR/                  # CommentHub
│   ├── wwwroot/                  # Compiled React SPA (production only)
│   └── Program.cs
├── Application/                  # CQRS use-cases (MediatR)
│   ├── Activities/
│   │   ├── Commands/             # CreateActivity, EditActivity, DeleteActivity,
│   │   │                         # UpdateAttendance, AddComment
│   │   ├── Queries/              # GetActivityList, GetActivityDetails, GetComments
│   │   ├── DTOs/                 # ActivityDto, CreateActivityDto, EditActivityDto
│   │   └── Validators/
│   ├── Profiles/
│   │   ├── Commands/             # AddPhoto, DeletePhoto, SetMainPhoto,
│   │   │                         # EditProfile, FollowToggle
│   │   ├── Queries/              # GetProfile, GetProfilePhotos,
│   │   │                         # GetFollowings, GetUserActivities
│   │   ├── DTOs/                 # UserProfile, UserActivityDto, PhotoUploadResult
│   │   └── Validators/
│   └── Core/                     # Result, PagedList, MappingProfiles, ValidationBehavior
├── Domain/                       # Pure domain entities
│   ├── Activity.cs
│   ├── ActivityAttendee.cs
│   ├── Comment.cs
│   ├── Photo.cs
│   ├── User.cs
│   └── UserFollowing.cs
├── Infrastructure/               # External service implementations
│   ├── Email/                    # ResendEmailSender
│   ├── Photos/                   # CloudinarySettings, PhotoService
│   └── Security/                 # IsHostRequirement, UserAccessor
├── Persistence/                  # EF Core DbContext, Migrations, DbInitializer
└── client/                       # React 19 + TypeScript (Vite)
    └── src/
        ├── app/
        │   ├── layout/           # App.tsx, NavBar, UserMenu
        │   ├── router/           # routes.tsx, RequireAuth
        │   └── shared/components/# PhotoUploadWidget, MapComponent,
        │                         # form inputs, AvatarPopover, etc.
        ├── features/
        │   ├── account/          # Login, Register, ForgotPassword, ResetPassword,
        │   │                     # ChangePassword, VerifyEmail, AuthCallback
        │   ├── activities/       # Dashboard, Details, Form (create/edit)
        │   ├── profiles/         # ProfilePage, ProfileHeader, ProfileContent,
        │   │                     # ProfilePhotos, ProfileFollowings, ProfileActivities
        │   └── errors/           # NotFound, ServerError, TestErrors
        └── lib/stores/           # MobX stores
```

---

## 🔌 API Endpoints

### Account Endpoints

Base path: `/api/account`

| Method | Route | Auth | Description |
|---|---|---|---|
| `POST` | `/api/account/github-login?code=` | Public | Exchange GitHub OAuth code for session |
| `POST` | `/api/account/register` | Public | Register with email, password, and display name |
| `GET`  | `/api/account/user-info` | Optional | Get current user info (204 if not authenticated) |
| `POST` | `/api/account/logout` | Required | Sign out and clear session cookie |
| `POST` | `/api/account/change-password` | Required | Change the current user's password |

ASP.NET Identity endpoints are also mapped under `/api` via `MapIdentityApi<User>()` (e.g. `/api/login`, `/api/forgotPassword`, `/api/resetPassword`).

### Activities Endpoints

Base path: `/api/activities`

| Method | Route | Auth | Description |
|---|---|---|---|
| `GET`    | `/api/activities` | Required | Paginated list — supports `filter`, `startDate`, `pageNumber`, `pageSize` |
| `GET`    | `/api/activities/{id}` | Required | Get a single activity with attendees and comments |
| `POST`   | `/api/activities` | Required | Create a new activity |
| `PUT`    | `/api/activities/{id}` | Host only | Edit an existing activity |
| `DELETE` | `/api/activities/{id}` | Host only | Delete an activity |
| `POST`   | `/api/activities/{id}/attend` | Required | Toggle attendance (join / leave / cancel) |

### Profiles Endpoints

Base path: `/api/profiles`

| Method | Route | Auth | Description |
|---|---|---|---|
| `GET`    | `/api/profiles/{userId}` | Required | Get a user's profile |
| `PUT`    | `/api/profiles` | Required | Update display name and bio |
| `POST`   | `/api/profiles/add-photo` | Required | Upload a photo (multipart form) |
| `GET`    | `/api/profiles/{userId}/photos` | Required | Get all photos for a user |
| `PUT`    | `/api/profiles/{photoId}/setMain` | Required | Set a photo as the main avatar |
| `DELETE` | `/api/profiles/{photoId}/photos` | Required | Delete a photo |
| `POST`   | `/api/profiles/{userId}/follow` | Required | Toggle follow / unfollow |
| `GET`    | `/api/profiles/{userId}/follow-list?predicate=` | Required | Get followers or following list |
| `GET`    | `/api/profiles/{userId}/activities?filter=` | Required | Get user's activities (hosting / going / attended) |

### SignalR Hub

| Route | Description |
|---|---|
| `/comments?activityId={id}` | Connect to the real-time chat room for an activity |

**Events:**

| Direction | Event | Payload |
|---|---|---|
| Client → Server | `SendComment` | `{ activityId, body }` |
| Server → Client | `ReceiveComment` | Comment object (id, body, createdAt, authorId, displayName, imageUrl) |
| Server → Client (on connect) | `LoadComments` | Array of all existing comments for the activity |

---

## 🐛 Troubleshooting

### Backend Won't Start

- Ensure SQL Server (or LocalDB) is running.
- Check the connection string in `appsettings.Development.json`.
- Check if port 5001 is already in use: `Get-NetTCPConnection -LocalPort 5001`.

### Frontend Can't Connect to Backend

- Ensure the backend is running before starting the frontend.
- Confirm the `vite.config.ts` proxy target matches the backend port (`https://localhost:5001`).
- Accept the self-signed dev certificate in your browser if prompted.

### Photos Not Uploading

- Verify `Cloudinary:CloudName`, `ApiKey`, and `ApiSecret` are set correctly in `appsettings.Development.json`.
- Restart the backend after updating the config file.

### Password Reset Email Not Received

- Confirm the account's `EmailConfirmed` flag is `true` in the database — Identity silently skips sending if it is `false`.
- Check your spam / junk / Other folder.
- On Resend's free tier, delivery only works to the email address used to sign up at resend.com.
- Verify `Resend:ApiKey` is set and the backend was restarted after adding it.

### GitHub Login Not Working

1. Create a GitHub OAuth App at **Settings → Developer settings → OAuth Apps**.
2. Set **Homepage URL** to `https://localhost:5173` and **Callback URL** to `https://localhost:5173/auth-callback`.
3. Add the `ClientId` and `ClientSecret` to `appsettings.Development.json` and restart the backend. 

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

## 👨‍💻 Author

Built by **Tauseef Iqbal**

- GitHub: [@tauseefiqbal](https://github.com/tauseefiqbal)
- Live App: [https://reactivities-wa.azurewebsites.net](https://reactivities-wa.azurewebsites.net)

# Reactivities

A full-stack social activities platform where users can create, browse, join, and manage real-world activities. Features real-time chat, user profiles with photo management, a follow system, and GitHub OAuth login — built with ASP.NET Core 9 and React 19.

**Email Functionality Note**
This project uses Resend for email services. At the moment, email delivery is limited by the current Resend configuration, so full production email functionality is not enabled. To support live email sending, a verified custom domain and proper API key setup are required. For testing purposes, I have currently disabled email verification for new registered users.

---

## Table of Contents

- [Reactivities](#reactivities)
  - [Table of Contents](#table-of-contents)
  - [✅ Features](#-features)
  - [🛠 Tech Stack](#-tech-stack)
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
    - [Updating and Cancelling an Activity](#updating-and-cancelling-an-activity)
    - [Attending an Activity](#attending-an-activity)
    - [Real-Time Chat](#real-time-chat)
    - [Managing Your Profile](#managing-your-profile)
    - [Following Other Users](#following-other-users)
    - [Forgot Password / Reset](#forgot-password--reset)
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
  - [🔒 Security](#-security)
  - [🐛 Troubleshooting](#-troubleshooting)
    - [Backend Won't Start](#backend-wont-start)
    - [Frontend Can't Connect to Backend](#frontend-cant-connect-to-backend)
    - [Photos Not Uploading](#photos-not-uploading)
    - [Password Reset Email Not Received](#password-reset-email-not-received)
    - [GitHub Login Not Working](#github-login-not-working)
  - [📄 License](#-license)

---

## ✅ Features

- ✅ User registration and login with email/password
- ✅ GitHub OAuth login (Authorization Code flow)
- ✅ Forgot password and reset password via email
- ✅ Change password for authenticated users
- ✅ Create, edit and cancel activities
- ✅ Browse activities with pagination and infinite scroll
- ✅ Filter activities by All, Going, and Hosting
- ✅ Filter activities by date using a calendar picker
- ✅ Join and leave activities as an attendee
- ✅ Real-time chat on activity pages via SignalR
- ✅ Interactive map with pin for activity location (Leaflet + LocationIQ)
- ✅ User profiles with bio, display name, and avatar
- ✅ Photo upload with drag-and-drop and in-browser cropping
- ✅ Set main profile photo / delete photos (synced with Cloudinary)
- ✅ Follow and unfollow other users
- ✅ Followers and following lists on profile pages
- ✅ View user activity history (hosting, going, attended)
- ✅ Host-only activity editing (IsActivityHost authorization policy)
- ✅ Security headers (CSP, X-Frame-Options, X-Content-Type-Options, Referrer-Policy)
- ✅ Rate limiting middleware (general + auth endpoint throttling)
- ✅ Server-side validation with FluentValidation
- ✅ Client-side form validation with React Hook Form + Zod
- ✅ Global error handling middleware
- ✅ Toast notifications for user feedback
- ✅ Responsive Material UI design with theming
- ✅ HTTPS in development via mkcert

---

## 🛠 Tech Stack

### Backend

| Technology | Purpose |
|---|---|
| **ASP.NET Core 9** | Web API framework |
| **Entity Framework Core 9** | ORM and database migrations |
| **SQL Server** | Relational database |
| **ASP.NET Identity** | Authentication, user management, password hashing |
| **MediatR (CQRS)** | Command/Query separation |
| **AutoMapper** | Object-to-object mapping |
| **FluentValidation** | Request validation |
| **SignalR** | Real-time WebSocket communication |
| **Cloudinary SDK** | Cloud photo storage and management |
| **Resend** | Transactional email (password reset) |

### Frontend

| Technology | Purpose |
|---|---|
| **React 19** | UI library |
| **TypeScript 5** | Static type checking |
| **Vite 6** | Build tool and dev server (HTTPS via mkcert) |
| **React Router 7** | Client-side routing |
| **MUI Material 7** | Component library and theming |
| **MUI X DatePickers 8** | Date and time picker components |
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

---

## 🌐 Deployment

The application is deployed and available at:

> **https://reactivities-wa.azurewebsites.net**

| Component | Details |
|---|---|
| **Live URL** | https://reactivities-wa.azurewebsites.net |
| **Platform** | Azure App Service |
| **Backend** | ASP.NET Core 9 — serves the API and the compiled React SPA |
| **Database** | SQL Server |
| **Photo Storage** | Cloudinary |
| **Email** | Resend |

---

## 🧪 Test and Admin User Credentials

### Test Users

The following accounts are seeded automatically on first run. All users have the same capabilities — any user can create/host, attend, and manage activities.

| Display Name | Email | Password |
|---|---|---|
| Bob | `bob@test.com` | `Pa$$w0rd` |
| Tom | `tom@test.com` | `Pa$$w0rd` |
| Jane | `jane@test.com` | `Pa$$w0rd` |

Each test user has:
- **Sample activities** they are hosting or attending — visible on the Activity Dashboard
- **Attendance records** — visible in their profile under the Activities tab

### Admin User

This application does not currently have a separate admin role. All authenticated users share the same set of permissions. The only privilege distinction is the **IsActivityHost** policy — when a user creates an activity, they become the host and only they can edit or cancel that activity.

---

## 👤 How to Use the App — Regular User

### Getting Started

1. Open the application — use the [live deployment](https://reactivities-wa.azurewebsites.net) or the local dev server (default: `https://localhost:5173`), from there, click the **TAKE ME TO THE ACTIVITIES** to take you to the login page.
2. Enter your registered email address and password, or use any of the test accounts provided in this guide, and click the **Login** button to log in to the system, or click **Register** in the Navbar to create a new account and enter Email, Display name, Password and click the **Register** button to register your account.
3. To sign in with GitHub, click the **GitHub** button on the login page.
4. After logging in, you are taken to the **Activity Dashboard**.

### Browsing and Managing Activities

1. The **Activity Dashboard** shows a list of upcoming activities. In this list, under each activity title, it's mentioned who created/hosted that activity, like this: **HOSTED BY (NAME OF A PERSON WHO CREATED/HOSTED THAT ACTIVITY)**.
2. Use the **Filters** panel on the right to switch between **All Event**, **I'm Going**, and **I'm Hosting** views.
3. Use the **calendar** in the filter panel to show only activities on or after a chosen date.
4. Click **View** on any activity card to open its full detail page.

### Creating an Activity

1. Click your **avatar** in the nav bar and select the **Create Activity** option.
3. Fill in the title, description, category, **Future** date/time, and location (start typing your desired location in the location field; the location field will show a list of locations related to your type; select any location from the list).
4. Click **Submit** — your activity will start reflecting on **Activity Dashboard**, and you will be automatically set as the host.

### Updating and Cancelling an Activity

1. Open any activity's detail page after clicking **View** on activity card in **Activity Dashboard**.
2. If you are the **Host** of that Activity, you will see the **Manage Event** button to update the information for your created Activity, and you will also see the **Cancel Activity** button to cancel your Activity.
3. Clicking **Manage Event** opens the Update Activity Form, where you can update details such as Title, Description, Category, Future Date, and Location.
4. Clicking the **Cancel Activity** button cancels your created or hosted activity.
   
### Attending an Activity

1. Open any activity's detail page after clicking **View** on activity card in **Activity Dashboard**.
2. Click **Join Activity** to sign up as an attendee; your avatar appears in the attendee list immediately.
3. Click **Cancel attendance** to remove yourself from the activity.

### Real-Time Chat

1. Open any activity's detail page after clicking **View** on activity card in **Activity Dashboard**, scroll to the **Chat** panel.
3. Type your message and press **Enter** (or click the send button).
5. All attendees currently viewing the page see your message appear instantly — no page refresh needed.

### Managing Your Profile

1. Click your **avatar** in the nav bar and select the **My profile** option.
2. The **About** tab shows your bio — click **Edit Profile** to update your display name or bio.
3. The **Photos** tab allows you to set a photo as your profile avatar across the app.
   Click **Add Photo**, drag and drop (or browse) an image file, crop it in the editor, then click **Upload**. 
   Click the **delete** icon on a photo to permanently remove it (also deleted from Cloudinary).
4. The **Event/Activity** tab shows the most recent **Future Events/Activities**, **Past Events/Activities**, and **Hosting**. 

### Following Other Users

1. Open another user's profile by clicking their avatar or name anywhere in the app.
2. Click **Follow** to follow them; click **Unfollow** to stop following.
3. The **Followers** and **Following** tabs on any profile page show the complete follow graph for that user.

### Forgot Password / Reset

1. On the **Login** page, click **Forgot password?**.
2. Enter your registered email address and click **Send reset email**.
3. Check your inbox and spam/junk folder for an email containing a reset password link.
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
  "Authentication": {
    "GitHub": {
      "ClientId": "YOUR_GITHUB_CLIENT_ID",
      "ClientSecret": "YOUR_GITHUB_CLIENT_SECRET"
    }
  },
  "CloudinarySettings": {
    "CloudName": "YOUR_CLOUD_NAME",
    "ApiKey": "YOUR_API_KEY",
    "ApiSecret": "YOUR_API_SECRET"
  },
  "Resend": {
    "ApiKey": "YOUR_RESEND_API_KEY",
    "FromEmail": "onboarding@resend.dev"
  },
  "ClientAppUrl": "https://localhost:5173"
}
```

> `Authentication:GitHub`, `CloudinarySettings`, and `Resend` sections are optional for basic local testing. The app starts without them, but the corresponding features will be unavailable.

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
| `ConnectionStrings:DefaultConnection` | SQL Server connection string | Yes |
| `Authentication:GitHub:ClientId` | GitHub OAuth App client ID | GitHub login only |
| `Authentication:GitHub:ClientSecret` | GitHub OAuth App client secret | GitHub login only |
| `CloudinarySettings:CloudName` | Cloudinary cloud name | Photo upload only |
| `CloudinarySettings:ApiKey` | Cloudinary API key | Photo upload only |
| `CloudinarySettings:ApiSecret` | Cloudinary API secret | Photo upload only |
| `Resend:ApiKey` | Resend API key for transactional email | Email features only |
| `Resend:FromEmail` | Sender address (`onboarding@resend.dev` on free tier) | Email features only |
| `ClientAppUrl` | Frontend origin used to build OAuth redirect and password-reset links | Yes |

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

## 🔒 Security

- **Security headers** — CSP, X-Frame-Options (DENY), X-Content-Type-Options (nosniff), X-XSS-Protection, Referrer-Policy
- **Rate limiting** — 60 requests/minute per IP for general endpoints; 5 requests/minute per IP for auth endpoints (login/register)
- **CORS** — restricted to specific localhost origins in development
- **Authentication** — cookie-based sessions via ASP.NET Identity
- **Authorization** — global `RequireAuthenticatedUser` policy; host-only editing via `IsActivityHost` policy
- **Password hashing** — handled by ASP.NET Identity (PBKDF2)
- **Input validation** — server-side FluentValidation + client-side Zod schemas

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

- Verify `CloudinarySettings:CloudName`, `ApiKey`, and `ApiSecret` are set correctly in `appsettings.Development.json`.
- Restart the backend after updating the config file.

### Password Reset Email Not Received

- Confirm the account's `EmailConfirmed` flag is `true` in the database — Identity silently skips sending if it is `false`.
- Check your spam / junk / Other folder.
- On Resend's free tier, delivery only works to the email address used to sign up at resend.com.
- Verify `Resend:ApiKey` is set and the backend was restarted after adding it.

### GitHub Login Not Working

1. Create a GitHub OAuth App at **Settings → Developer settings → OAuth Apps**.
2. Set **Homepage URL** to `https://localhost:5173` and **Callback URL** to `https://localhost:5173/auth-callback`.
3. Add the `ClientId` and `ClientSecret` to `appsettings.Development.json` under `Authentication:GitHub` and restart the backend.
4. Ensure the `ClientAppUrl` in `appsettings.Development.json` is set to `https://localhost:5173`.

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

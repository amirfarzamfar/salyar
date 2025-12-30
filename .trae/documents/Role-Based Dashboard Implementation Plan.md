# Implementation Plan: Role-Based Dashboard System

I have analyzed your project structure and authentication system. Here is the comprehensive plan to implement the role-based dashboard as requested.

## 1. Architecture & Folder Structure
We will organize the dashboard code to be modular and scalable:

```
src/
  app/
    dashboard/
      layout.tsx       # Main dashboard layout (Header + Sidebar)
      page.tsx         # Role-based content switcher
  components/
    dashboard/
      layout/
        Sidebar.tsx    # Responsive sidebar with role-based links
        Header.tsx     # Top bar with user profile & mobile menu trigger
        UserNav.tsx    # User dropdown menu
      roles/
        AdminDashboard.tsx      # Admin view
        SupervisorDashboard.tsx # Supervisor view
        CaregiverDashboard.tsx  # Caregiver view (Nurse, Helper, etc.)
        ClientDashboard.tsx     # Client view (Patient, Family, etc.)
      shared/
        StatCard.tsx   # Reusable statistic card component
        RecentActivity.tsx # Reusable activity list
  config/
    dashboard.ts       # Navigation configuration for each role
```

## 2. Role Mapping Strategy
Your system has many specific roles. We will group them into the 4 requested dashboard types:

| Dashboard Type | System Roles |
| :--- | :--- |
| **Admin** | `Admin` |
| **Supervisor** | `Supervisor` |
| **Caregiver** | `Nurse`, `AssistantNurse`, `ElderlyCompanion`, `MotherHelper` |
| **Client** | `Patient`, `Elderly`, `Family` |

## 3. Implementation Steps

### Step 1: Dashboard Configuration
Create `src/config/dashboard.ts` to define:
- Navigation items (Icon, Label, Href) for each role.
- Helper function `getDashboardType(role)` to map specific system roles to the 4 main dashboard types.

### Step 2: Dashboard Layout Components
Implement the shared layout components using Tailwind CSS:
- **Sidebar:** Fixed left navigation that changes based on the user's role.
- **Header:** Sticky top bar with breadcrumbs and user actions.
- **Layout Wrapper:** Ensures the sidebar and content area work correctly on desktop and mobile.

### Step 3: Role-Specific Dashboard Components
Create the 4 main dashboard views with mock data:
- **Admin:** System overview, total users, revenue, active tickets.
- **Supervisor:** Shift management, active caregivers, patient alerts.
- **Caregiver:** Today's schedule, patient vitals input, assigned tasks.
- **Client:** Care plan summary, upcoming visits, request new service.

### Step 4: Main Dashboard Page Logic
Implement `src/app/dashboard/page.tsx` to:
1. Check the current user's role from `AuthContext`.
2. Determine the dashboard type.
3. Render the correct component (`<AdminDashboard />`, `<ClientDashboard />`, etc.).

### Step 5: Styling & Polish
- Apply a "Medical SaaS" aesthetic (Clean whites, soft grays, primary blue/teal accents).
- Ensure full responsiveness (Mobile sidebar drawer).
- Use `lucide-react` for consistent iconography.

## 4. Verification
- We will log in with different users (mocked or real) to verify the correct dashboard loads.
- Check responsive behavior on mobile and desktop sizes.

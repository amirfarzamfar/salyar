I will refactor the Header component to follow SOLID principles and modernize the UI.

### 1. Architecture & Configuration (SRP & OCP)
*   **Create `src/config/site.ts`**: Centralize navigation items and site metadata here. This separates data from UI components, making it easy to update links without touching the code.

### 2. Component Atomization (Interface Segregation)
I will break down the monolithic `Header.tsx` into smaller, focused components in `src/components/layout/header/`:
*   **`Logo.tsx`**: Renders the brand logo/name.
*   **`DesktopNav.tsx`**: Handles the horizontal menu for larger screens with modern hover animations.
*   **`MobileNav.tsx`**: A dedicated component for the mobile hamburger menu and drawer animation.
*   **`AuthButtons.tsx`**: Manages the Login/Register buttons (and eventually the User Profile).

### 3. UI/UX Enhancements
*   **Glassmorphism**: Enhanced blur effects for a modern look.
*   **Scroll Awareness**: The header will be transparent at the top and transition to a glass effect when scrolling down.
*   **Animations**:
    *   **Desktop**: Smooth underline animations on hover.
    *   **Mobile**: A smooth slide-in/fade-in drawer using `framer-motion`.

### 4. Implementation Steps
1.  Create `src/config/site.ts`.
2.  Create the sub-components folder `src/components/layout/header`.
3.  Implement the atomic components (`Logo`, `DesktopNav`, `MobileNav`, `AuthButtons`).
4.  Reassemble them in the main `Header` component.

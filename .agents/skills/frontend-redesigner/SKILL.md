# Frontend Design & Development – PaulAlarba Portfolio

This document defines standards, tools, and best practices for the frontend of the **PaulAlarba** portfolio website. It builds upon the visual inspiration from **carlrosales.com** and integrates seamlessly with the ASP.NET Core MVC backend.

---

## 🧱 Tech Stack (Frontend)

- **Core:** HTML5, CSS3, JavaScript (ES6+)
- **CSS Framework:** Tailwind CSS (via CDN or NPM)
- **Icons:** Font Awesome 6 (Free CDN)
- **Fonts:** Google Fonts – `Inter` (body) + `Fira Code` (monospace accents)
- **Layout:** Flexbox, CSS Grid, responsive breakpoints
- **Interactivity:** Vanilla JavaScript (no jQuery)
- **Animations:** CSS transitions + Intersection Observer for scroll effects

> **Integration note:** Static `.html` prototypes will later be converted to Razor views (`_Layout.cshtml`, partials). Keep markup clean and class‑based.

---

## 🎨 Design Principles

| Principle | Implementation |
|-----------|----------------|
| **Dark theme first** | Background: `#0a0a0a` or `#0f0f12`. Text: `#e5e5e5`. Accent: neon cyan `#00f3ff` or purple `#b973ff`. |
| **Story‑driven** | Hero section begins with a personal narrative (e.g., “From first line of code to full‑stack architect”). |
| **Subtle depth** | Glass‑morphic cards (`bg-white/5`, `backdrop-blur-sm`), soft borders, and shadow on hover. |
| **High readability** | Minimum contrast ratio 4.5:1. Use `font-medium` or `font-semibold` for body text on dark backgrounds. |
| **Consistent spacing** | 8px grid system. Section padding: `py-16` (mobile) / `py-24` (desktop). |

---

## 🧩 Component Guidelines

### Navigation (Sticky + Mobile Hamburger)
- Sticky at top, `backdrop-blur-md`, border‑bottom with `border-white/10`.
- Links: HOME, ABOUT, PROJECTS, CONTACT.
- **Active link** highlighted with accent underline or glow.
- Mobile: hamburger menu (☰) → overlay drawer or full‑width dropdown.

### Hero Section
- **Headline:** Bold, narrative tagline (2 lines max).
- **Subtext:** Short bio + location/role (e.g., “Full‑stack developer based in [City]”).
- **CTAs:** Two buttons – “View Work” (scroll to Projects) and “Let’s Talk” (scroll to Contact).
- **Optional:** Animated code editor snippet or a 3D orb (lightweight CSS/JS only).

### About / Story Section
- Two‑column layout on desktop: left – text (narrative with expandable “Read full story” toggle), right – profile image placeholder.
- Use `max-w-prose` for text columns to keep line length comfortable.

### Projects (Work) Section
- Responsive grid: 1 column (mobile) → 2 (tablet) → 3 (desktop).
- Each **Project Card** includes:
  - Image placeholder (CSS gradient or Unsplash URL)
  - Title + short description (2‑3 lines)
  - Tech stack tags (e.g., `.NET`, `Tailwind`)
  - Link: “Learn more →” (non‑functional for now, but styled as interactive)
- On hover: scale + subtle border glow.

### Skills Section
- “Pills” or tag cloud: `display: inline-flex`, rounded‑full, `bg-white/10`, `px-4 py-2`, `m-1`.
- Hover: accent background + white text.

### Contact Section
- Two‑part layout:
  - **Left (or top on mobile):** Contact form (Name, Email, Message) + submit button (non‑functional placeholder).
  - **Right (or bottom):** Social links (GitHub, LinkedIn, X/Twitter) + direct email `mailto:`.
- Form inputs: dark background (`bg-black/40`), border `white/20`, focus ring with accent color.

### Footer
- Copyright line: auto‑updates current year via JS.
- “Back to top” button: appears after scrolling > 300px, smooth scroll.

---

## 📱 Responsive Breakpoints (Tailwind defaults)

| Breakpoint | Min width | Target devices |
|------------|-----------|----------------|
| `sm`       | 640px     | Large phones    |
| `md`       | 768px     | Tablets         |
| `lg`       | 1024px    | Laptops         |
| `xl`       | 1280px    | Desktops        |

**Rule:** Always design mobile‑first. Use `flex-col` then `md:flex-row`.

---

## ✨ Interaction & Motion

- **Smooth scroll:** `html { scroll-behavior: smooth; }`
- **Section reveal:** fade‑up animation when section enters viewport (Intersection Observer).
- **Hover micro‑interactions:** buttons scale to `105%`, links underline with `::after`.
- **Loading state (optional):** minimal skeleton or spinner if dynamic content is added later.

**Performance:** Prefer `transform` and `opacity` for animations (GPU accelerated). No heavy libraries.

---

## 🧼 Code & File Hygiene

- **CSS:** Use Tailwind utility classes directly in HTML. Custom CSS only for complex animations or overrides → place in `wwwroot/css/site.css`.
- **JavaScript:** Keep in `wwwroot/js/site.js`. Avoid inline scripts unless trivial.
- **Images:** Optimize (WebP format) in `wwwroot/images/`. Provide low‑res placeholder blur.
- **Icons:** Use `<i class="fab fa-github"></i>` – always include `aria-hidden="true"` and a screen‑reader text.

**Naming convention (for any custom classes):** BEM or utility‑first. Example: `.card__title` or `.hover-glow`.

---

## 🧪 Testing Checklist (Frontend)

Before committing frontend changes:

- [ ] Page passes **Lighthouse** performance > 90 (mobile emulation).
- [ ] No horizontal overflow on any device size (test with Chrome DevTools).
- [ ] All interactive elements have `cursor: pointer`.
- [ ] Touch targets ≥ 44x44px on mobile.
- [ ] Hamburger menu closes after link click.
- [ ] Form inputs have visible focus states.
- [ ] No console errors (JavaScript).
- [ ] Smooth scroll works and doesn’t jump the navigation bar.

---

## 🔁 Integration with ASP.NET Core MVC

When converting static HTML to Razor:

- Replace static content with `@Model` properties (e.g., projects list, skills).
- Change `<a>` links to `asp-action` / `asp-controller` where needed.
- Move reusable components (project card, skill pill) to partial views (`_ProjectCard.cshtml`).
- Use `@await Html.PartialAsync()` in the main view.
- The `_Layout.cshtml` should include `@RenderSection("Scripts", required: false)` for page‑specific JS.

**Do not** commit `*.html` files inside the MVC project – keep them as design prototypes outside `/Views`.

---

## 📚 Resources & References

- [Tailwind CSS Docs](https://tailwindcss.com/docs)
- [Font Awesome Icons](https://fontawesome.com/icons)
- [Intersection Observer API](https://developer.mozilla.org/en-US/docs/Web/API/Intersection_Observer_API)
- [Inspiration: carlrosales.com](https://www.carlrosales.com/)

---

*Last updated: 2026-05-30*  
*Maintainer: Paul Alarba Frontend Team*
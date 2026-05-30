document.addEventListener("DOMContentLoaded", function () {
  const header = document.getElementById("site-header");
  const menuToggle = document.getElementById("menu-toggle");
  const mobileMenu = document.getElementById("mobile-menu");
  const backToTop = document.getElementById("back-to-top");
  const currentYear = document.getElementById("current-year");
  const storyToggle = document.getElementById("story-toggle");
  const storyExtra = document.getElementById("story-extra");
  const contactForm = document.getElementById("contact-form");
  const formNote = document.getElementById("form-note");

  if (currentYear) {
    currentYear.textContent = new Date().getFullYear();
  }

  function closeMobileMenu() {
    if (!mobileMenu || !menuToggle) return;
    mobileMenu.classList.remove("open");
    menuToggle.setAttribute("aria-expanded", "false");
    menuToggle.innerHTML = '<i class="fa-solid fa-bars" aria-hidden="true"></i>';
  }

  function openMobileMenu() {
    if (!mobileMenu || !menuToggle) return;
    mobileMenu.classList.add("open");
    menuToggle.setAttribute("aria-expanded", "true");
    menuToggle.innerHTML = '<i class="fa-solid fa-xmark" aria-hidden="true"></i>';
  }

  if (menuToggle) {
    menuToggle.addEventListener("click", function () {
      if (!mobileMenu) return;
      const isOpen = mobileMenu.classList.contains("open");
      isOpen ? closeMobileMenu() : openMobileMenu();
    });
  }

  document.querySelectorAll('a[href^="#"]').forEach(function (link) {
    link.addEventListener("click", function (event) {
      const targetId = link.getAttribute("href");
      const target = document.querySelector(targetId);

      if (!target) return;

      event.preventDefault();
      closeMobileMenu();

      const headerOffset = header ? header.offsetHeight : 0;
      const targetPosition = target.getBoundingClientRect().top + window.scrollY - headerOffset + 2;

      window.scrollTo({
        top: targetPosition,
        behavior: "smooth"
      });
    });
  });

  if (storyToggle && storyExtra) {
    storyToggle.addEventListener("click", function () {
      const isExpanded = storyToggle.getAttribute("aria-expanded") === "true";
      storyExtra.classList.toggle("hidden", isExpanded);
      storyToggle.setAttribute("aria-expanded", String(!isExpanded));
      const buttonText = storyToggle.querySelector("span");
      const buttonIcon = storyToggle.querySelector("i");

      if (buttonText) {
        buttonText.textContent = isExpanded ? "Read Full Story" : "Show Less";
      }

      if (buttonIcon) {
        buttonIcon.className = isExpanded ? "fa-solid fa-arrow-right-long" : "fa-solid fa-arrow-up-long";
      }
    });
  }

  if (contactForm && formNote) {
    contactForm.addEventListener("submit", function (event) {
      event.preventDefault();
      formNote.classList.remove("hidden");
    });
  }

  if (backToTop) {
    window.addEventListener("scroll", function () {
      backToTop.classList.toggle("visible", window.scrollY > 300);
    });

    backToTop.addEventListener("click", function () {
      window.scrollTo({
        top: 0,
        behavior: "smooth"
      });
    });
  }

  const revealObserver = new IntersectionObserver(
    function (entries, observer) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add("is-visible");
          observer.unobserve(entry.target);
        }
      });
    },
    {
      threshold: 0.12,
      rootMargin: "0px 0px -60px 0px"
    }
  );

  document.querySelectorAll(".section-reveal").forEach(function (element) {
    revealObserver.observe(element);
  });

  const navLinks = Array.from(document.querySelectorAll(".nav-link"));
  const sections = Array.from(document.querySelectorAll("main section[id]"));

  if (navLinks.length && sections.length) {
    const activeObserver = new IntersectionObserver(
      function (entries) {
        entries.forEach(function (entry) {
          if (!entry.isIntersecting) return;

          const id = entry.target.getAttribute("id");
          navLinks.forEach(function (link) {
            link.classList.toggle("active", link.getAttribute("href") === `#${id}`);
          });
        });
      },
      {
        threshold: 0.45,
        rootMargin: "-76px 0px -42% 0px"
      }
    );

    sections.forEach(function (section) {
      activeObserver.observe(section);
    });
  }

  document.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
      closeMobileMenu();
    }
  });
});

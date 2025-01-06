# Atelier
Next Generation Atelier is a platform for sharing, categorizing, and evaluating product development initiatives. It empowers users to propose and discuss ideas, fostering collaboration and driving innovation.

# Project Formulation & Initial Requirements: NextGenAtelier

## Project Description (Domain)

The DND course project focuses on developing a platform that enables users to share, categorize, and evaluate product development initiatives. The platform will facilitate user registration, authentication (login/logout), and submission of initiatives on product development. Each initiative will include a descriptive title and detailed information about the proposed idea. Users will also have the ability to assign initiatives to specific functional areas of product development.

### Why This Project?

This project was designed as an ad hoc solution with the future internship in the Next Generation Platform within the product development team in mind. A significant part of the internship focuses on exploring how Artificial Intelligence can be integrated across various functional areas. To support this research, I developed an application that empowers users to contribute meaningful ideas, leveraging their expertise and experience to drive change. Recognizing that people are the company’s greatest asset, this platform emphasizes the importance of capturing and amplifying their voices.

The project incorporates essential software development principles, including the implementation of RESTful APIs to ensure seamless communication between systems and using Blazor to provide an intuitive and engaging interface, fostering active participation and collaboration. Thus, the aim is to create a platform that is practical, user-driven, and scalable. The project also allows us to deal with real-world challenges like managing user-generated content and securing user data.

## Requirements (User Stories)

### User Registration & Login

- As a new user, I want to create an account, so I can submit my initiatives.
- As an existing user, I want to log in securely using my username and password, so I can access my profile and submit or manage my initiatives.

### Submit Product Development Initiatives

- As a logged-in user, I want to create a new initiative post where I can share my initiative, including details like title, content, and the area of product development.
- As a user, I want to edit or delete my previous initiatives, so I can manage and update my content.

### View Product Development Initiatives

- As a visitor or logged-in user, I want to view a list of all initiatives submitted by other users, so I can read about their initiatives.
- As a visitor, I want to browse initiatives by category, so I can easily find initiatives related to specific areas.

### Initiative Viewing for Guests

- As a visitor (without logging in), I want to browse and view initiatives, so I can read about initiatives even if I haven't created an account.

### Role-Based Access (Admin vs Regular User)

- As an admin, I want to moderate all submitted initiatives, so I can ensure inappropriate or irrelevant content is removed.
- As an admin, I want to delete or edit any user's initiative, so I can maintain the quality of the NextGenAtelier platform.

### User Authentication & Security

- As a user, I want my account to be protected with a secure password hashing mechanism, so my credentials are safe.

### Initiative Pagination

- As a user, I want to view the initiatives in a paginated format, so I can easily browse through multiple initiatives without overwhelming the interface.

## Delimitations

While the project concept includes a broad range of features and user stories, it's important to note that due to time constraints, resource limitations, and the complexity of some features, plus working solo, I may not be able to implement all the desired functionalities within the given project timeline.

The core focus of the project will be on the following required functionalities:
- User registration and login
- The ability to submit, view, edit, and delete product development initiatives
- Communication between the Blazor frontend and the ASP.NET Core Web API

### Potential Features Not Prioritized in the Initial Version:

- Advanced search and filtering of initiatives based on keywords.
- Profile management features beyond basic login and password change.
- Full mobile-responsive design or extensive UI/UX improvements.
- Uploading images or media within initiative posts (which could be considered as future enhancements).
- Extensive admin controls for moderating content beyond basic CRUD operations.

These additional features represent future extensions of the platform and would be considered if time and resources allow. However, for the initial phase, I will focus on meeting the primary technical requirements and delivering a working, secure, and user-friendly system.

---

!important! THE PROJECT DOCUMENTATION DEVELOPER GUIDE please check [DevelopersGuide](./DevelopersGuide.pdf)


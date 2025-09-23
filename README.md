# Tarzy

Tarzy is a full-stack **E-Commerce Management Platform** developed as a real-world solution for a local business.  
It provides online ordering, admin control, and delivery tracking with a secure, scalable architecture.

Built with **ASP.NET Core Web API**, **Entity Framework Core**, **PostgreSQL**, and modern frontend tools (**React + TypeScript + Tailwind CSS**), the platform follows best practices such as **SOLID principles**, **Dependency Injection**, **Repository Pattern**, and **Unit of Work**.

### Key Features

- **Customer Experience**: Place orders without registration; stock is reserved until admin approval for data integrity.
- **Admin Tools**: Manage products and variants, enforce pricing rules, track offer/price history, and assign orders to delivery companies with region-specific costs.
- **Order Workflow**: Approval system ensures accurate inventory and pricing.
- **Role-Based Access**: Secure roles for Admin, Employee, and Customer.
- **Frontend**: Responsive UI built in React + TypeScript with Hooks and Context for scalable state management.

---

## 🛠️ Technology Stack

- **Backend:** ASP.NET Core Web API + Entity Framework Core + ASP.NET Identity
- **Database:** PostgreSQL
- **Frontend:** React + Redux
- **Containerization:** Docker & Docker Compose

---

## 🚀 Getting Started

### Prerequisites

- Install [Docker](https://docs.docker.com/get-started/get-docker/)
- Install [Docker Compose](https://docs.docker.com/compose/install/)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/tarzy.git
   cd tarzy
   ```
2. Run the application with Docker Compose:
   `docker compose up`

# Personal Budget API

This is a personal budget API written in ***ASP.NET Core 7*** and unit-tested with tools like ***Moq***. It allows users to track their income and expenses, set budgets, and get insights into their spending habits.

## Features

- Track income and expenses
- Set budgets
- Get insights into spending habits
- Create reports
- Export data

## Requirements

- Microsoft.EntityFrameworkCore.Tools - v7.0.9
- Microsoft.AspNetCore.Authentication.JwtBearer - v7.0.9
- Microsoft.AspNetCore.Identity.EntityFrameworkCore - v7.0.9
- Microsoft.EntityFrameworkCore.SqlServer- v7.0.9
- Moq - v4.20.69
- xUnit - v2.4.2

## Installation

1. Clone the repository from GitHub.
2. Open the project in Visual Studio 2022.
3. Build and run the project.

## Usage

To use the API, make a HTTP request to the appropriate endpoint. For example, to get a list of all plans, you would make a GET request to the `/api/Plans` endpoint.

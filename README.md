# Weather Forecast Application

## Overview
This is a web application built using ASP.NET MVC 5 with VB.NET, designed to manage weather forecasts. It allows users to upload CSV files containing location data, fetch weather forecasts from the Open-Meteo API, and view recent forecasts. The application uses a Microsoft Access database to store user and forecast data, with a custom data access layer for interaction.

## Features
- User authentication with admin and regular user roles.
- CSV upload for location data (Latitude, Longitude, LocationName).
- Weather forecast retrieval and storage using the Open-Meteo API.
- Recent forecast display for admins.
- Responsive navigation with user info and logout options.

## Project Flow
1. **User Authentication**:
   - Users log in via the `/Account/Login` page.
   - Admin (UserId: 1) sees recent forecasts; other users are redirected to the upload page.
   - Logout clears the session and returns to the login page.
2. **CSV Upload**:
   - Users upload a CSV file via `/Weather/Upload`.
   - The app parses the CSV, fetches forecasts for each location using the Open-Meteo API, and saves them to the database.
3. **Forecast Display**:
   - Admins view recent forecasts at `/Weather/RecentForecasts`.
   - Forecasts include date, temperature, and a description with location name and username.
4. **Data Persistence**:
   - Data is stored in an Access database and cached for performance.

## Prerequisites
- Visual Studio 2019 or 2022
- .NET Framework 4.7
- Microsoft Access Database Engine (32-bit or 64-bit) - [Download](https://www.microsoft.com/en-us/download/details.aspx?id=13255)
- Git installed
- Internet connection for API calls

## Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/yourusername/WeatherForecastApp.git

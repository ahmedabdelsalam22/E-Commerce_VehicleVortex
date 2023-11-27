# VehicleVortex Ecommerce API

This repository contains the VehicleVortex Ecommerce API, a backend system built using C# and ASP.NET. The API provides functionalities for managing an ecommerce platform for vehicles.

## Features

- User authentication and authorization
- Vehicle catalog management
- Shopping cart functionality
- Order management
- Payment integration (Stripe)

## Installation

To run the VehicleVortex Ecommerce API locally, follow these steps:

1. Clone the repository:

   ````bash
   git clone https://github.com/ahmedabdelsalam22/VehicleVortex_EcommerceAPI.git
   ```

2. Open the solution file `VehicleVortex.sln` in Visual Studio.
3. Restore the NuGet packages.
4. Build the solution.
5. Configure the database connection string in the `appsettings.json` file.
6. Run the database migrations to create the required tables.
7. Start the API by running the project.

## Usage

Once the API is up and running, you can access the available endpoints to interact with the system. Refer to the API documentation for more detailed information about each endpoint and their request/response payloads.

Make sure you have the necessary authentication credentials to access protected endpoints.

## API Endpoints

The VehicleVortex Ecommerce API provides the following endpoints:

- `GET /api/vehicles` - Get a list of all vehicles.
- `GET /api/vehicles/{id}` - Get a specific vehicle by ID.
- `POST /api/vehicles` - Create a new vehicle.
- `PUT /api/vehicles/{id}` - Update an existing vehicle.
- `DELETE /api/vehicles/{id}` - Delete a vehicle.

Refer to the API documentation for more detailed information about each endpoint and their request/response payloads.

## Configuration

The API can be configured using the `appsettings.json` file. The following configurations are available:

- Database connection string
- JWT authentication settings
- Stripe payment integration settings
- Logging settings

Make sure to update the necessary configurations before running the API in a production environment.

## Contributing

Contributions to this repository are welcome. If you encounter any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.

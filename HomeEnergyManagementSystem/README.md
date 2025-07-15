This is a mockup for the backend API of a Home Energy Management System created for an assignment. It was a built in a single day.

Prerequisites:
- .NET 7 SDK (or compatible .NET SDK version)
- Visual Studio 2022 for easier experience

Running the application:
- Using Visual Studio:
- Open the solution (.sln file) in Visual Studio
- Press the green arrow with "https" at the top (or ctrl+F5)
- The API will launch and be available at https://localhost:7143, http://localhost:5296 or otherwise specified in Properties/launchSettings.json

- Using the .NET CLI
- Navigate to the project directory containing the .csproj file
- Run the application with: dotnet run
- The API will launch and be available at https://localhost:7143, http://localhost:5296 or otherwise specified in Properties/launchSettings.json

API Endpoints:
- GET /api/devices - retrieves all registered devices, or an empty array if no devices present
- POST /api/device - Adds a new device through a JSON body for example { Type : "Lights" }. Be aware the devices are case sensitive. Available devices are Refrigerator, Lights, HeatingSystem, EVCharger, SolarPanel and HamsterWheel. Returns the device created if successful.
- DELETE /api/device?id={id} - Deletes a device from the HEMS, requires device id in query. Returns the deleted device. For example: DELETE https://localhost:7143/api/Device?id=0132bbe9-1ca3-4025-959f-730f8b7afd15
- PUT /api/device - Changes the Enabled state of a device, turning it off or on for usage. Requires a JSON body for example { "Id": "<<GuidId>>", "State", false }. Returns the altered device.
- GET /api/energyusage?time={DateTimeOffset} - calculates all device output and consumption at {DateTimeOffset}, returns the DateTime, consumption, production and the difference between them. For example: GET https://localhost:7143/api/EnergyUsage?time=2025-01-31T12:00:00%2B01:00

Running integration tests:
- Use the Visual Studio test explorer OR
- Navigate to the project directory containing the .csproj file
- Run the tests with: dotnet test
- Alternatively, run the program and use the manual tests described in HomeEnergyManagementSystem.http in Visual Studio

Assumptions:
- Only 1 user (no roles required),
- Only sending one request at a time (foregone ConcurrentDictionary/mutexes/semaphores/async/await in favour of time),
- Permanent server uptime with no data loss (foregone database integration in favour of time)
- Devices follow the same consumption/production patterns daily
- The scope of this project remains small. If the scope would grow, I would subdivide controllers/services/models in their own subfolders

Future Improvements:
- Replace in-memory storage with a persistent Database
- Add concurrency control for handling simultaneous requests
- Implement authentication  and role-based access
- Expand the device profiles with actual device output/production
- Containerize for easier deployment
- Closer testing of the services, mocking them with NSubstitute or Moq and also trying failing cases

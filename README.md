# SmartCity System

## Overview
SmartCity is a modular .NET console application that simulates intelligent city management. It coordinates Transportation, Lighting, Security, and Energy subsystems through a single controller, while keeping each subsystem independent and extensible.

## Design Patterns Used
- Singleton + Facade: `Core/SmartCityController.cs` ensures a single controller instance and exposes a unified API to the UI.
- Factory Method: `Factories/SmartCitySubsystemFactory.cs` centralizes subsystem creation and decouples the controller from concrete types.
- Builder: `Builders/SmartCityConfigurationBuilder.cs` assembles complex configuration data step-by-step.
- Adapter: `Adapters/ExternalEnergyServiceAdapter.cs` bridges a legacy energy API to the subsystem interface.
- Proxy: `Proxies/SecurityProxy.cs` enforces security access control before delegating to the real subsystem.

## Project Structure
- `Program.cs`
- `Core/`
- `Factories/`
- `Builders/`
- `Adapters/`
- `Proxies/`
- `Modules/`
- `Tests/`

## Run the App
```bash
cd D:\projectAP
dotnet run
```

## Run Tests
```bash
cd D:\projectAP
dotnet test Tests\SmartCity.Tests.csproj
```

## Notes
- The system uses interfaces for subsystem interaction and keeps the console UI separate from subsystem logic.
- Security access is controlled via role-based policy configured at startup.

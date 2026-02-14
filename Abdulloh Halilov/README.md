# Smart City Management System

## Project Description

This is a comprehensive Smart City Management System built in C# that demonstrates the implementation of multiple software design patterns. The system simulates an intelligent city infrastructure with various interconnected subsystems for lighting, transportation, security, and energy management.

## Design Patterns Implemented

This project implements **6 design patterns** from the allowed list:

### 1. Singleton Pattern
- **Location**: `Core/Singleton/SmartCityController.cs`
- **Purpose**: Ensures only one instance of the central city controller exists throughout the application
- **Implementation**: Thread-safe double-check locking pattern
- **Usage**: `SmartCityController.Instance` provides global access to the controller

### 2. Factory Method Pattern
- **Location**: `Core/Factories/InfrastructureFactory.cs`
- **Purpose**: Creates different types of city infrastructure devices (lights, cameras, meters)
- **Implementation**: Abstract factory base class with concrete factories for each device type
- **Usage**: Used by subsystems to create infrastructure components

### 3. Builder Pattern
- **Location**: `Core/Builders/CityConfigurationBuilder.cs`
- **Purpose**: Constructs complex city configurations step-by-step
- **Implementation**: Builder interface with concrete builder and optional director
- **Usage**: Builds cities with different sizes and infrastructure combinations

### 4. Facade Pattern
- **Location**: `Core/SmartCityFacade.cs`
- **Purpose**: Provides a simplified interface to the complex subsystem interactions
- **Implementation**: Single entry point that coordinates all subsystems
- **Usage**: Main interface used by the console application

### 5. Proxy Pattern
- **Location**: `Core/Proxy/SecuritySubsystemProxy.cs`
- **Purpose**: Controls access to sensitive security subsystem operations
- **Implementation**: Access control with authorization checking and logging
- **Usage**: Protects security camera access and settings modification

### 6. Adapter Pattern
- **Location**: `Core/Adapters/ExternalServiceAdapter.cs`
- **Purpose**: Integrates external/legacy systems with incompatible interfaces
- **Implementation**: Adapters for weather service and traffic data service
- **Usage**: Converts external data formats to system-compatible formats

## Project Structure

```
Abdulloh Halilov/
├── SmartCitySystem.sln              # Solution file
├── SmartCitySystem/                 # Main project
│   ├── Program.cs                   # Entry point with console interface
│   ├── SmartCitySystem.csproj       # Project file
│   ├── Core/                        # Core components
│   │   ├── ISubsystem.cs            # Subsystem interface
│   │   ├── SmartCityFacade.cs       # Facade pattern
│   │   ├── Factories/               # Factory pattern
│   │   │   └── InfrastructureFactory.cs
│   │   ├── Builders/                # Builder pattern
│   │   │   └── CityConfigurationBuilder.cs
│   │   ├── Adapters/                # Adapter pattern
│   │   │   └── ExternalServiceAdapter.cs
│   │   ├── Proxy/                   # Proxy pattern
│   │   │   └── SecuritySubsystemProxy.cs
│   │   └── Singleton/               # Singleton pattern
│   │       └── SmartCityController.cs
│   └── Modules/                     # Subsystems
│       ├── Lighting/
│       │   └── LightingSubsystem.cs
│       ├── Transport/
│       │   └── TransportSubsystem.cs
│       ├── Security/
│       │   └── SecuritySubsystem.cs
│       └── Energy/
│           └── EnergySubsystem.cs
├── SmartCitySystem.Tests/           # Test project
│   ├── SmartCitySystem.Tests.csproj
│   └── SmartCitySystemTests.cs      # Comprehensive unit tests
├── .gitignore                       # Git ignore file
└── README.md                        # This file
```

## System Architecture

The system follows a modular architecture with clear separation of concerns:

```
┌─────────────────────────────────────────────┐
│           Console Interface                 │
│              (Program.cs)                   │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│         SmartCityFacade (Facade)            │
│     Simplified interface to subsystems      │
└─────────────────┬───────────────────────────┘
                  │
┌─────────────────▼───────────────────────────┐
│    SmartCityController (Singleton)          │
│     Central coordination of all systems     │
└─────────────────┬───────────────────────────┘
                  │
    ┌─────────────┼─────────────┬─────────────┐
    │             │             │             │
┌───▼────┐  ┌────▼────┐  ┌─────▼────┐  ┌────▼─────┐
│Lighting│  │Transport│  │Security  │  │Energy    │
│System  │  │System   │  │System    │  │System    │
│        │  │(Adapter)│  │(Proxy)   │  │(Adapter) │
└────────┘  └─────────┘  └──────────┘  └──────────┘
     │            │             │             │
     └────────────┴─────────────┴─────────────┘
                       │
            ┌──────────▼──────────┐
            │ Infrastructure      │
            │ Devices (Factory)   │
            └─────────────────────┘
```

## Features

### Console Interface
- Interactive menu-driven system
- View status of all subsystems
- Control individual subsystems
- Demonstrate each design pattern

### Lighting Management
- Turn lights on/off
- Adjust brightness levels
- Monitor individual lights
- Automated control based on time

### Transportation Management
- Monitor traffic status (via Adapter)
- Control traffic lights
- Optimize traffic flow
- Public transport integration

### Security System
- Access control with Proxy pattern
- Security camera management
- Alert monitoring
- Access logging

### Energy Monitoring
- Real-time consumption monitoring
- Energy optimization
- Weather-based adjustments (via Adapter)
- Multiple meter support

## How to Build and Run

### Prerequisites
- .NET 8.0 SDK or later
- Compatible with Windows, Linux, and macOS

### Building the Project

```bash
# Navigate to the project directory
cd "Abdulloh Halilov"

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Or build in Release mode
dotnet build -c Release
```

### Running the Application

```bash
# Run from the solution directory
dotnet run --project SmartCitySystem/SmartCitySystem.csproj

# Or run the built executable
cd SmartCitySystem/bin/Debug/net8.0
./SmartCitySystem
```

## How to Run Tests

### Run All Tests

```bash
# Navigate to the project directory
cd "Abdulloh Halilov"

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Run tests with coverage (if configured)
dotnet test --collect:"XPlat Code Coverage"
```

### Test Coverage

The test suite includes 15 comprehensive unit tests covering:
- Singleton pattern uniqueness (Test 1)
- Factory pattern object creation (Tests 2)
- Builder pattern construction (Tests 3, 4)
- Proxy pattern access control (Test 5)
- Adapter pattern integration (Tests 6, 7)
- Subsystem operations (Tests 8-11, 14)
- Facade pattern interface (Test 12)
- Infrastructure device functionality (Test 13)
- Energy optimization (Test 15)

All tests should pass successfully ✅

## Usage Examples

### Example 1: Viewing System Status

```
=== SmartCity Management System ===
1. View All Systems Status

=== Smart City System Status ===

Lighting Management System:
Status: Running
Total Lights: 5
Active Lights: 5
Brightness Level: 100%

Transportation Management System:
Status: Running
Traffic Lights: 4
Active Lights: 4
Traffic Status: Normal
...
```

### Example 2: Using Security with Proxy Pattern

```
4. Security System
> Access Security Camera
Enter user ID: admin
Enter camera ID: CAM-001

[PROXY LOG] Data access attempt by user: admin
[PROXY LOG] Access GRANTED for user: admin
[REAL SYSTEM] Sensitive security data accessed by admin
Camera Feed: Security Camera CAM-001: Recording
```

### Example 3: Builder Pattern for City Configuration

```
6. System Configuration

=== City Configuration: Metropolis ===
Population: 500,000
Area: 150 sq km
Lighting Devices: 50
Transport Devices: 30
Security Devices: 40
Energy Devices: 20
====================================
```

## Code Quality

The project follows best practices:
- ✅ Clean, readable code with consistent naming conventions
- ✅ C# OOP principles (inheritance, polymorphism, encapsulation)
- ✅ XML documentation comments for all public classes and methods
- ✅ Clear comments explaining design pattern usage
- ✅ Exception handling where appropriate
- ✅ SOLID principles adherence

## Technologies Used

- **Language**: C# 12
- **Framework**: .NET 8.0
- **Testing**: xUnit
- **Design**: Object-Oriented Programming
- **Patterns**: 6 design patterns implemented

## Design Pattern Summary

| Pattern | Category | Purpose |
|---------|----------|---------|
| Singleton | Creational | Single controller instance |
| Factory Method | Creational | Create infrastructure devices |
| Builder | Creational | Construct complex city configs |
| Facade | Structural | Simplify subsystem access |
| Proxy | Structural | Control security access |
| Adapter | Structural | Integrate external services |

## Author

**Abdulloh Halilov**  
Lab Work №1 - Smart City Management System  
Course: Software Design Patterns

## License

This project is created for educational purposes as part of a software engineering course.

---

**Note**: This system demonstrates software design patterns and best practices in action. Each pattern serves a specific purpose and shows how they can be combined to create a maintainable, extensible application architecture.

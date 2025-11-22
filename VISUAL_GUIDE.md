# 🎨 Visual Architecture Guide
## See How Everything Connects

> **Purpose:** Visual diagrams to understand the 8 design patterns and how they work together

---

## 🏗️ Big Picture: Complete System

```
                       👤 USER
                          ↓
        ╔═══════════════════════════════════╗
        ║   🎭 SmartCityFacade (FACADE)    ║  ← Simple interface
        ║   "Start city!" "Emergency!"      ║
        ╚═════════════╦═════════════════════╝
                      ↓
        ┌─────────────┼─────────────┐
        ↓             ↓             ↓
   🌤️ Weather   🛡️ Security   ⚡ Energy
    (ADAPTER)    (PROXY)        Monitor
        ↓             ↓
   External       Permission
   Weather API    Checking
   
                      ↓
        ╔═══════════════════════════════════╗
        ║ 🏛️ CityController (SINGLETON)    ║  ← ONE instance only
        ║ Controls everything               ║
        ╚═════════════╦═════════════════════╝
                      ↓
        ╔═══════════════════════════════════╗
        ║ 🏭 SubsystemFactory               ║  ← Creates families
        ║ (ABSTRACT FACTORY)                ║
        ╚═════════════╦═════════════════════╝
                      ↓
        ┌─────────────┼─────────────┬──────────┐
        ↓             ↓             ↓          ↓
   💡 Lighting  🔒 Security  🚦 Transport  ⚡ Energy
      System       System       System      System
```

---

## Pattern #1: SINGLETON (One Controller)

```
❌ Multiple Controllers = Chaos!
Controller A: "Turn lights ON"
Controller B: "Turn lights OFF"  ← CONFLICT!
Controller C: "Turn lights ON"

✅ Singleton = One Boss
           Controller
                ↓
    "I'm the ONLY controller!"
```

**Code:**
```typescript
private static instance: CityController | null = null;
private constructor() { }  // Can't use 'new'!

static getInstance() {     // Only way to get it
  if (!instance) {
    instance = new CityController();  // Create once
  }
  return instance;  // Always return same one
}
```

---

## Pattern #2: FACTORY METHOD (Create Lights)

```
User Request: "I need a light"
                ↓
         LightingFactory
                ↓
        What type do you want?
                ↓
     ┌──────────┼──────────┐
     ↓          ↓          ↓
   LED      Halogen     Solar
   10W        20W       5W + ☀️
```

**Code:**
```typescript
LightingFactory.createLight({ type: 'LED', power: 10 })
                        ↓
                  Returns LEDLight object

User doesn't need to know about:
- LEDLight class
- How to construct it
- What parameters it needs
```

---

## Pattern #3: ABSTRACT FACTORY (Create Families)

```
Need: Complete Lighting System
                ↓
   LightingSubsystemFactory
                ↓
        Creates EVERYTHING:
        ├─ Light controller
        ├─ Energy monitor
        ├─ Status display
        └─ Management tools
                ↓
      Complete Lighting System ✅

Same for:
- SecuritySubsystemFactory → Complete Security System
- TransportSubsystemFactory → Complete Transport System
```

---

## Pattern #4: BUILDER (Build Traffic Signal)

```
Simple Signal:
new TrafficSignalBuilder()
  .setId('TS-001')
  .setLocation('Main St')
  .build()
  ↓
Basic Signal ✅

Complex Signal:
new TrafficSignalBuilder()
  .setId('TS-002')
  .setLocation('5th Ave')
  .setTimings(45, 5, 40)       ← Custom timings
  .withPedestrianCrossing()    ← Add crossing
  .withCamera()                 ← Add camera
  .build()
  ↓
Advanced Signal with All Features ✅
```

**Why Builder?**
- Step-by-step = clear
- Optional features = flexible
- Method chaining = readable

---

## Pattern #5: COMPOSITE (Light Hierarchy)

```
         Downtown District (LightGroup)
                  │
     ┌────────────┼────────────┐
     ↓            ↓            ↓
Main Street  Park Avenue  Oak Blvd
(LightGroup) (LightGroup) (LightGroup)
     │            │            │
 ┌───┼───┐    ┌───┼───┐    ┌───┼───┐
 ↓   ↓   ↓    ↓   ↓   ↓    ↓   ↓   ↓
🔆  🔆  🔆    🔆  🔆  🔆    🔆  🔆  🔆
LED LED LED  LED LED LED  LED LED LED

Call: district.turnOn()
  → Turns on ALL 9 lights recursively!

Call: street1.turnOn()
  → Turns on 3 lights in that street

Call: light1.turnOn()
  → Turns on 1 light

SAME INTERFACE! ✨
```

---

## Pattern #6: ADAPTER (Weather Integration)

```
External Weather Service:
{
  temp_celsius: 25,
  humidity_percent: 60,
  weather_condition: "Rainy"
}
        ↓
   WeatherAdapter  ← Translates
        ↓
Our System Needs:
{
  temperature: 25,
  humidity: 60,
  condition: "Rainy"
}

Problem: Different field names!
Solution: Adapter converts between them
```

**Real-Life:** Like a power adapter for different countries

---

## Pattern #7: PROXY (Security Guard)

```
User: "Activate alarms!"
        ↓
   SecurityProxy ← Checks permission first
        ↓
   Check user role...
        ↓
   ┌────┴────┐
   ↓         ↓
Viewer?   Admin?
   ↓         ↓
  ❌       ✅ Allow
"Access    Forward to
Denied"    Real System
              ↓
         Alarms Activated!
```

**Permission Levels:**
- Viewer: Read-only ❌ Can't change anything
- Operator: Can control cameras ✅
- Admin: Full access ✅✅✅

---

## Pattern #8: FACADE (Simple Interface)

```
Without Facade (Complex):
user.startCityController();
user.initializeSubsystems();
user.activateEnergyMonitoring();
user.setupLighting();
user.checkWeather();
user.configureSecurityLevels();
// ... 20 more lines ...

With Facade (Simple):
city.startCity();  ← ONE method does everything!

Behind the scenes, Facade calls all those methods for you!
```

**Real-Life:** Like a TV remote "Power On" button - does many things with one press

---

## 🔗 How Patterns Work Together

### Example: Emergency Mode

```
1. User clicks "Emergency"
        ↓
2. FACADE receives command
   facade.activateEmergencyMode()
        ↓
3. SINGLETON controller coordinates
   controller.setEmergencyStatus()
        ↓
4. PROXY checks permission
   if (userRole === 'admin') ✅
        ↓
5. COMPOSITE activates all lights
   districtLights.turnOnHigh()
        ↓
6. FACTORY creates monitoring objects
   factory.createEmergencyMonitors()
        ↓
7. ADAPTER checks weather
   weatherAdapter.getCurrentConditions()
        ↓
8. BUILDER might add emergency signals
   builder.withEmergencyLights().build()
        ↓
Result: Everything coordinated! 🚨
Energy jumps from 850 → 2,295 kWh
```

---

## 📊 Data Flow Diagram

```
USER INPUT
    ↓
[FACADE] ← Entry point
    ↓
[SINGLETON] ← Coordinates
    ↓
    ├─→ [FACTORY] → Creates objects
    ├─→ [BUILDER] → Builds complex things
    ├─→ [COMPOSITE] → Organizes hierarchies
    ├─→ [ADAPTER] → Integrates external
    ├─→ [PROXY] → Controls access
    └─→ [ABSTRACT FACTORY] → Creates families
            ↓
    All work together!
            ↓
      RESULT OUTPUT
```

---

## 🎯 Quick Pattern Finder

**Need to create objects?**
- One instance only → SINGLETON
- Different types → FACTORY METHOD
- Complete families → ABSTRACT FACTORY
- Complex step-by-step → BUILDER

**Need to organize objects?**
- Tree structure → COMPOSITE
- Simple interface → FACADE
- Different format → ADAPTER
- Control access → PROXY

---

## 💡 Remember This!

Each pattern solves a specific problem:

| Problem | Pattern | Think of It As |
|---------|---------|---------------|
| Need ONE instance | Singleton | One president |
| Create many types | Factory | Restaurant menu |
| Create families | Abstract Factory | Furniture set |
| Build complex thing | Builder | Custom computer |
| Tree structure | Composite | Military hierarchy |
| Convert formats | Adapter | Power plug adapter |
| Control access | Proxy | Security guard |
| Hide complexity | Facade | TV remote |

**All work together to create a maintainable, extensible system!** 🚀

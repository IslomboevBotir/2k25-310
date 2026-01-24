# 🔗 How Systems Work Together
## Understanding Nested Logic & Integration

> **What is "Nested Logic"?** When one system's actions affect multiple other systems automatically - like a chain reaction!

---

## ⚡ The Energy System (Heart of Integration)

The Energy Monitor is the central hub that's affected by everything else in the city!

### 📊 Energy Calculation Formula

```
Total Energy = Base × Security Level × Lighting Status
```

**Example:**
```
Base Consumption = 850 kWh
Security = HIGH (1.3x multiplier)
Lighting = ON (1.0x multiplier)

Total = 850 × 1.3 × 1.0 = 1,105 kWh
```

---

## 🔗 How Actions Trigger Reactions

### 1. Security Changes → Energy Changes

| Security Level | Multiplier | Why? |
|---------------|------------|------|
| **LOW** | 0.8x | Fewer cameras, less power |
| **MEDIUM** | 1.0x | Normal operation |
| **HIGH** | 1.3x | More surveillance equipment |
| **CRITICAL** | 1.8x | All systems maximum! |

**Try this in the demo:**
```
1. Start city (option 1)
2. Check energy (option 9 → 1) → See baseline ~850 kWh
3. Change security to HIGH (option 7 → 3)
4. Check energy again → Now ~1,105 kWh!
```

---

### 2. Lighting Changes → Energy Changes

| Lighting Status | Multiplier | When This Happens |
|----------------|------------|-------------------|
| **OFF** | 0.3x | Eco mode or daytime |
| **ON Normal** | 1.0x | Regular operation |
| **ON High** | 1.5x | Emergency mode |

**Try this in the demo:**
```
1. Toggle lights OFF (option 8 → 1 → 1)
2. Check energy → Drops to ~255 kWh!
3. Toggle lights ON again
4. Check energy → Back to ~850 kWh
```

---

### 3. Weather → Lighting (Automatic!)

The weather adapter automatically controls lights based on conditions:

```typescript
if (weather === 'Rainy' || weather === 'Cloudy' || time < 6am || time > 6pm) {
  automaticallyTurnOnLights();
}
```

**This happens automatically when you start the city!**

---

### 4. Emergency Mode → Everything MAX!

```
Option 3: Emergency Alert

What happens:
1. Security → CRITICAL (1.8x)
2. Lighting → ON High (1.5x)
3. All cameras activated
4. All alarms armed

Result: Energy = 850 × 1.8 × 1.5 = 2,295 kWh! 🚨
```

---

### 5. Eco Mode → Everything MIN!

```
Option 4: Eco Mode

What happens:
1. Security → MEDIUM (1.0x)
2. Lighting → OFF if weather is good (0.3x)
3. Non-essential systems dimmed

Result: Energy = 850 × 1.0 × 0.3 = 255 kWh! 🌱
```

---

## 🎯 Interactive Demo Scenarios

### Scenario 1: Watch Energy Change
```
1. npm run dev
2. Option 1: Start City → Energy starts at ~850 kWh
3. Option 3: Emergency Mode → Energy jumps to ~2,295 kWh!
4. Option 4: Eco Mode → Energy drops to ~255 kWh!
5. Option 9: Check energy each time to see the changes
```

### Scenario 2: Nested Menu Exploration
```
1. Option 5: Traffic Control
   → Sub-menu appears!
   → 1. Add signal
   → 2. View all signals
   → 3. Stop a signal
   
2. Option 7: Security Access
   → Choose your level!
   → 1. Viewer (read-only)
   → 2. Operator (can control cameras)
   → 3. Admin (full access)
   
3. Option 8: Lighting Control
   → Multiple actions!
   → 1. Toggle all lights
   → 2. View status
   → 3. Set brightness (30%, 60%, 100%)
```

### Scenario 3: Time Simulation
```
1. Option 9: Energy Management → 2. Simulate time
2. Enter hours (e.g., 10)
3. Watch as the system:
   - Simulates 10 hours passing
   - Updates energy consumption
   - Shows new production values
   - Calculates efficiency
```

---

## 🔄 Complete Integration Map

```
         👤 USER INPUT
              ↓
      [SmartCityFacade]
              ↓
        ┌─────┴─────┐
        ↓           ↓
   [Security]   [Lighting]
        ↓           ↓
        └─────┬─────┘
              ↓
      [Energy Monitor] ← CENTRAL HUB
              ↓
    Calculates Total Energy
         (Base × Security × Lighting)
              ↓
        Updates Display
```

---

## 📝 Key Takeaways

1. **Everything is connected** - No action happens in isolation
2. **Energy is the metric** - Shows impact of all decisions
3. **Real-time calculations** - Changes happen immediately
4. **Nested menus** - Most options have sub-options
5. **Realistic simulation** - Mimics real smart city behavior

---

## 🧪 Test It Yourself!

**Challenge:** Get energy consumption to exactly 2,295 kWh

**Answer:**
```bash
1. Start city
2. Emergency mode (option 3)
3. Check energy (option 9 → 1)
Result: 850 × 1.8 (CRITICAL) × 1.5 (High Lights) = 2,295 kWh ✅
```

**Challenge:** Get energy consumption below 300 kWh

**Answer:**
```bash
1. Start city
2. Eco mode (option 4)
3. Turn off lights (option 8 → 1)
4. Check energy (option 9 → 1)
Result: 850 × 1.0 (MEDIUM) × 0.3 (Lights OFF) = 255 kWh ✅
```

---

## 💡 Why This Matters for Oral Exam

**Professor asks:** "How do your patterns interact?"

**You answer:** "Great question! Let me show you in the demo...

1. The **Facade** provides simple interface (SmartCityFacade)
2. When I call `activateEmergencyMode()`:
   - **Proxy** checks if user can change security
   - **Singleton** controller updates all subsystems
   - **Composite** light groups turn on recursively
   - **Adapter** checks weather conditions
   - **Factory** might create new monitoring objects
   - **Energy Monitor** recalculates consumption
   
Everything works together! The energy number you see (2,295 kWh) proves multiple systems are interacting."

**This shows you understand the INTEGRATION, not just individual patterns!** 🎯

---

## ✅ Summary

- **Nested Logic** = Actions trigger reactions across systems
- **Energy** = Central metric showing all interactions
- **Interactive Demo** = Best way to demonstrate integration
- **Real-World Behavior** = System mimics actual smart cities

**Run `npm run dev` and explore!** 🚀

# 🚀 How to Run the Smart City Project
## Complete Step-by-Step Guide

---

## ⚡ Quick Commands (Copy & Paste)

### 1. Install Everything (First Time Only)
```bash
npm install
```
**What it does:** Downloads all required packages  
**Expected:** Installs TypeScript, Jest, and other dependencies

---

### 2. Run Tests (See All 45 Tests Pass!)
```bash
npm test
```
**What it does:** Runs all unit tests for the 8 design patterns  
**Expected output:**
```
PASS tests/singleton.test.ts
PASS tests/factory.test.ts
PASS tests/builder.test.ts
PASS tests/composite.test.ts
PASS tests/adapter.test.ts
PASS tests/proxy.test.ts
PASS tests/facade.test.ts
PASS tests/controller.test.ts

Test Suites: 8 passed, 8 total
Tests:       45 passed, 45 total ✅
Time:        ~2-3 seconds
```

---

### 3. Build the Project (Compile TypeScript)
```bash
npm run build
```
**What it does:** Converts TypeScript (.ts) to JavaScript (.js)  
**Expected:** No errors, creates `dist/` folder  
**Output when successful:** (Nothing = good! No output means success)

---

### 4. Run Interactive Demo (See It in Action!)
```bash
npm run dev
```
**What it does:** Starts the interactive Smart City control panel  
**What you'll see:**
- Beautiful colored menu
- 10 interactive options
- All 8 design patterns working together
- Real-time energy calculations
- Nested sub-menus

**Example interaction:**
```
╔════════════════════════════════════════════════════════╗
║              🎮 CONTROL PANEL                          ║
╠════════════════════════════════════════════════════════╣
  1. 🚀 Start City                  [Initialize All Systems]
  2. 📊 City Dashboard              [View Live Status]
  3. 🚨 Emergency Alert             [CRITICAL MODE]
  4. 🌱 Eco Mode                    [Save Energy]
  5. 🚦 Traffic Control             [Manage Signals]
  6. 🌤️  Weather Station            [View Weather Data]
  7. 🔒 Security Access             [Choose Level]
  8. 💡 Lighting Control            [Manage Lights]
  9. ⚡ Energy Management           [View & Simulate]
 10. 🛑 Shutdown City               [Stop All Systems]
  0. 👋 Exit Program
╚════════════════════════════════════════════════════════╝

Status:  OFFLINE 

➤ Enter command: 1

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
🚀 INITIATING CITY STARTUP SEQUENCE...

✓ Booting power grid
✓ Activating street lights
✓ Starting security systems
✓ Initializing traffic control

✅ CITY IS NOW OPERATIONAL!
```

---

### 5. Run Compiled Version (Production Mode)
```bash
npm start
```
**What it does:** Runs the compiled JavaScript from `dist/` folder  
**Note:** Must run `npm run build` first!

---

## 📋 Complete Verification Checklist

### Before Submission or Demo

Run these commands in order:

```bash
# Step 1: Install (if you haven't already)
npm install

# Step 2: Build (check for errors)
npm run build

# Step 3: Test (check all patterns work)
npm test

# Step 4: Run demo (see it working)
npm run dev
```

**Everything should complete without errors!** ✅

---

## 🎯 What Each Test File Checks

| Test File | What It Tests | Tests Count |
|-----------|--------------|-------------|
| `singleton.test.ts` | One instance, state persistence | 7 |
| `factory.test.ts` | Light creation, subsystem factories | 12 |
| `builder.test.ts` | Traffic signal construction | 3 |
| `composite.test.ts` | Light hierarchy, group operations | 3 |
| `adapter.test.ts` | Weather API integration | 2 |
| `proxy.test.ts` | Security access control | 3 |
| `facade.test.ts` | Simple system interface | 14 |
| `controller.test.ts` | Redirect notice | 1 |
| **TOTAL** | **All 8 Patterns** | **45** ✅ |

---

## ❌ Troubleshooting Common Issues

### Problem: "npm: command not found"
**Solution:** Install Node.js from https://nodejs.org/

### Problem: "Cannot find module"
**Solution:** Run `npm install` first

### Problem: Test fails
**Solution:** 
1. Delete `node_modules` and `dist` folders
2. Run `npm install`
3. Run `npm run build`
4. Run `npm test`

### Problem: Port already in use
**Solution:** The demo doesn't use ports - it's a console app. Just press Ctrl+C and run again.

---

## 📊 Expected Results Summary

### ✅ Successful Build
```bash
$ npm run build
> tsc
(no output = success!)
```

### ✅ All Tests Pass
```bash
$ npm test
Test Suites: 8 passed, 8 total
Tests:       45 passed, 45 total
Snapshots:   0 total
Time:        2-3s
```

### ✅ Demo Runs
```bash
$ npm run dev
# Shows colorful menu and interactive options
```

---

## 🎓 For Oral Exam Preparation

### Show the Professor:

1. **Run Tests** - Shows all patterns are tested
```bash
npm test
```

2. **Run Demo** - Shows interactive system
```bash
npm run dev
```

3. **Show Code** - Point to pattern files:
- Singleton: `src/core/singleton/CityController.ts`
- Factory: `src/core/factories/LightingFactory.ts`
- Composite: `src/modules/lightning/LightGroup.ts`
- Others in their respective folders

4. **Explain Integration** - Show how patterns work together in `src/core/SmartCityFacade.ts`

---

## 📁 Project Structure Overview

```
AbdulazizYusupov/
├── src/                   ← Source code (TypeScript)
│   ├── main.ts           ← Interactive demo entry point
│   ├── core/             ← Design patterns implementation
│   └── modules/          ← Smart city subsystems
├── tests/                ← Unit tests (45 tests)
├── dist/                 ← Compiled code (created by build)
├── package.json          ← Dependencies & scripts
├── tsconfig.json         ← TypeScript configuration
└── jest.config.js        ← Test configuration
```

---

## 🚀 Quick Start for New Users

**Just 3 commands:**

```bash
npm install    # Install dependencies
npm test       # Verify everything works
npm run dev    # See it in action!
```

**That's it! Your Smart City is running!** 🏙️

---

## 💡 Tips

- **Demo is interactive** - Try all menu options!
- **Watch energy changes** - Security levels and lighting affect energy consumption
- **Nested menus** - Most options have sub-menus for deeper interaction
- **Read the output** - System explains what it's doing
- **Press 0 to exit** - Clean exit from the demo

**Enjoy exploring the Smart City!** 🌆

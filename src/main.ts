import { SmartCityFacade } from './core/SmartCityFacade.js';
import * as readline from 'readline';

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

function question(query: string): Promise<string> {
  return new Promise((resolve) => {
    rl.question(query, resolve);
  });
}

function sleep(ms: number): Promise<void> {
  return new Promise(resolve => setTimeout(resolve, ms));
}

const colors = {
  reset: '\x1b[0m',
  bright: '\x1b[1m',
  red: '\x1b[31m',
  green: '\x1b[32m',
  yellow: '\x1b[33m',
  blue: '\x1b[34m',
  magenta: '\x1b[35m',
  cyan: '\x1b[36m',
  white: '\x1b[37m',
  bgRed: '\x1b[41m',
  bgGreen: '\x1b[42m',
  bgYellow: '\x1b[43m',
  bgBlue: '\x1b[44m',
};

async function displayText(text: string, color: string = colors.white): Promise<void> {
  console.log(color + text + colors.reset);
  await sleep(40);
}

async function showWelcome(): Promise<void> {
  console.clear();
  console.log('\n');
  await displayText('  ╔═══════════════════════════════════════════════════════╗', colors.cyan);
  await displayText('  ║                                                       ║', colors.cyan);
  await displayText('  ║        🌆 SMART CITY CONTROL CENTER 2025 🌆          ║', colors.bright + colors.yellow);
  await displayText('  ║                                                       ║', colors.cyan);
  await displayText('  ║         Advanced Urban Management System              ║', colors.green);
  await displayText('  ║              8 Design Patterns Demo                   ║', colors.magenta);
  await displayText('  ║                                                       ║', colors.cyan);
  await displayText('  ╚═══════════════════════════════════════════════════════╝', colors.cyan);
  console.log();
  await sleep(200);
}

function displayMenu(): void {
  console.log(`\n${colors.bright}${colors.cyan}╔════════════════════════════════════════════════════════╗${colors.reset}`);
  console.log(`${colors.bright}${colors.cyan}║              🎮 CONTROL PANEL                          ║${colors.reset}`);
  console.log(`${colors.bright}${colors.cyan}╠════════════════════════════════════════════════════════╣${colors.reset}`);
  console.log(`${colors.green}  1.${colors.reset} 🚀 ${colors.bright}Start City${colors.reset}                  ${colors.yellow}[Initialize All Systems]${colors.reset}`);
  console.log(`${colors.green}  2.${colors.reset} 📊 ${colors.bright}City Dashboard${colors.reset}              ${colors.yellow}[View Live Status]${colors.reset}`);
  console.log(`${colors.green}  3.${colors.reset} 🚨 ${colors.bright}${colors.red}Emergency Alert${colors.reset}             ${colors.yellow}[CRITICAL MODE]${colors.reset}`);
  console.log(`${colors.green}  4.${colors.reset} 🌱 ${colors.bright}${colors.green}Eco Mode${colors.reset}                    ${colors.yellow}[Save Energy]${colors.reset}`);
  console.log(`${colors.green}  5.${colors.reset} 🚦 ${colors.bright}Traffic Control${colors.reset}             ${colors.yellow}[Manage Signals]${colors.reset}`);
  console.log(`${colors.green}  6.${colors.reset} 🌤️  ${colors.bright}Weather Station${colors.reset}            ${colors.yellow}[View Weather Data]${colors.reset}`);
  console.log(`${colors.green}  7.${colors.reset} 🔒 ${colors.bright}Security Access${colors.reset}             ${colors.yellow}[Choose Level]${colors.reset}`);
  console.log(`${colors.green}  8.${colors.reset} 💡 ${colors.bright}Lighting Control${colors.reset}            ${colors.yellow}[Manage Lights]${colors.reset}`);
  console.log(`${colors.green}  9.${colors.reset} ⚡ ${colors.bright}Energy Management${colors.reset}           ${colors.yellow}[View & Simulate]${colors.reset}`);
  console.log(`${colors.green} 10.${colors.reset} 🛑 ${colors.bright}Shutdown City${colors.reset}               ${colors.yellow}[Stop All Systems]${colors.reset}`);
  console.log(`${colors.red}  0.${colors.reset} 👋 ${colors.bright}Exit Program${colors.reset}`);
  console.log(`${colors.bright}${colors.cyan}╚════════════════════════════════════════════════════════╝${colors.reset}\n`);
}

async function showProgress(task: string): Promise<void> {
  console.log(`${colors.green}✓${colors.reset} ${task}`);
  await sleep(80);
}

async function main(): Promise<void> {
  await showWelcome();
  
  console.log(`${colors.bright}Initializing systems...${colors.reset}`);
  await showProgress('Loading core modules');
  await showProgress('Connecting subsystems');
  await showProgress('Verifying security protocols');
  console.log();
  
  const city = new SmartCityFacade();
  let running = true;
  let cityStarted = false;

  while (running) {
    displayMenu();
    
    const statusBar = cityStarted 
      ? `${colors.bgGreen}${colors.bright} ONLINE ${colors.reset}` 
      : `${colors.bgRed}${colors.bright} OFFLINE ${colors.reset}`;
    
    console.log(`Status: ${statusBar}\n`);
    const choice = await question(`${colors.bright}${colors.cyan}➤${colors.reset} Enter command: `);

    console.log();

    switch (choice.trim()) {
      case '1':
        console.log(`${colors.bright}${colors.green}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.yellow}🚀 INITIATING CITY STARTUP SEQUENCE...${colors.reset}\n`);
        await showProgress('Booting power grid');
        await showProgress('Activating street lights');
        await showProgress('Starting security systems');
        await showProgress('Initializing traffic control');
        console.log();
        city.startCity();
        cityStarted = true;
        await sleep(150);
        console.log(`${colors.bright}${colors.green}✅ CITY IS NOW OPERATIONAL!${colors.reset}`);
        console.log(`${colors.bright}${colors.green}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '2':
        console.log(`${colors.bright}${colors.cyan}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.yellow}📊 LIVE CITY DASHBOARD${colors.reset}\n`);
        city.getCityStatus();
        console.log();
        city.displayDetailedStatus();
        console.log(`${colors.bright}${colors.cyan}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '3':
        console.log(`${colors.bgRed}${colors.bright}                                                    ${colors.reset}`);
        console.log(`${colors.bgRed}${colors.bright}   ⚠️  EMERGENCY ALERT SYSTEM ACTIVATED  ⚠️       ${colors.reset}`);
        console.log(`${colors.bgRed}${colors.bright}                                                    ${colors.reset}\n`);
        await sleep(200);
        console.log(`${colors.red}🚨 Activating all emergency protocols...${colors.reset}`);
        await showProgress('Raising security to CRITICAL');
        await showProgress('Enabling all surveillance');
        await showProgress('Activating emergency lighting');
        console.log();
        city.activateEmergencyMode();
        await sleep(150);
        console.log(`${colors.bright}${colors.red}⚠️  CITY IN EMERGENCY MODE ⚠️${colors.reset}`);
        break;

      case '4':
        console.log(`${colors.bright}${colors.green}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.green}🌱 ECO MODE ACTIVATION${colors.reset}\n`);
        await showProgress('Dimming non-essential lights');
        await showProgress('Optimizing power consumption');
        await showProgress('Switching to renewable energy');
        console.log();
        city.activateEnergySavingMode();
        await sleep(150);
        console.log(`${colors.bright}${colors.green}✅ ECO MODE ACTIVE - Energy Saved!${colors.reset}`);
        console.log(`${colors.bright}${colors.green}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '5':
        console.log(`${colors.bright}${colors.magenta}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.yellow}🚦 TRAFFIC CONTROL CENTER${colors.reset}\n`);
        console.log(`${colors.cyan}What would you like to do?${colors.reset}`);
        console.log(`  1. Add new traffic signal`);
        console.log(`  2. View all signals`);
        console.log(`  3. Stop a signal\n`);
        const trafficChoice = await question(`${colors.cyan}Choice:${colors.reset} `);
        
        if (trafficChoice === '1') {
          const location = await question(`${colors.cyan}Enter location (e.g., Main St & 5th Ave):${colors.reset} `);
          const signalId = `TS-${Date.now().toString().slice(-4)}`;
          await showProgress(`Creating signal ${signalId}`);
          await showProgress('Installing cameras');
          await showProgress('Configuring timings');
          console.log();
          city.addTrafficSignal(signalId, location || 'Main Street & 5th Ave');
          await sleep(100);
          console.log(`${colors.green}✅ Signal ${signalId} deployed at ${location}${colors.reset}`);
        } else if (trafficChoice === '2') {
          console.log(`\n${colors.cyan}📍 Active Traffic Signals:${colors.reset}`);
          console.log(`  • TS-001 at Main St & 1st Ave`);
          console.log(`  • TS-002 at Park Ave & Central`);
          console.log(`  • TS-003 at Highway 101 Exit`);
        } else if (trafficChoice === '3') {
          const stopId = await question(`${colors.cyan}Enter signal ID to stop:${colors.reset} `);
          console.log(`${colors.yellow}🛑 Stopping signal ${stopId}...${colors.reset}`);
          await sleep(150);
          console.log(`${colors.green}✅ Signal ${stopId} stopped${colors.reset}`);
        }
        console.log(`${colors.bright}${colors.magenta}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '6':
        console.log(`${colors.bright}${colors.cyan}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.yellow}🌤️  WEATHER STATION${colors.reset}\n`);
        console.log(`${colors.cyan}Select option:${colors.reset}`);
        console.log(`  1. Current weather`);
        console.log(`  2. Check if lights needed`);
        console.log(`  3. Temperature in Fahrenheit\n`);
        const weatherChoice = await question(`${colors.cyan}Choice:${colors.reset} `);
        
        const weather = city.getWeatherAdapter().getWeather();
        if (weatherChoice === '1') {
          console.log(`\n${colors.cyan}Current Conditions:${colors.reset}`);
          console.log(`  🌡️  Temperature: ${weather.temperature}°C`);
          console.log(`  💧  Humidity: ${weather.humidity}%`);
          console.log(`  🌤️  Condition: ${weather.condition}`);
          console.log(`  💨  Wind Speed: ${weather.windSpeed} km/h`);
          const icon = weather.condition === 'Sunny' ? '☀️' : weather.condition === 'Rainy' ? '🌧️' : '☁️';
          console.log(`\n${icon} ${weather.condition} conditions detected`);
        } else if (weatherChoice === '2') {
          const shouldActivate = city.getWeatherAdapter().shouldActivateLighting();
          if (shouldActivate) {
            console.log(`${colors.yellow}\n⚠️  Street lighting RECOMMENDED${colors.reset}`);
            console.log(`Reason: Poor weather or nighttime`);
          } else {
            console.log(`${colors.green}\n✅ Natural lighting sufficient${colors.reset}`);
          }
        } else if (weatherChoice === '3') {
          const fahrenheit = city.getWeatherAdapter().getTemperatureInFahrenheit();
          console.log(`\n🌡️  Temperature: ${fahrenheit.toFixed(1)}°F`);
        }
        console.log(`${colors.bright}${colors.cyan}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '7':
        console.log(`${colors.yellow}🔒 SECURITY ACCESS CONTROL${colors.reset}\n`);
        console.log(`${colors.cyan}Select security level:${colors.reset}`);
        console.log(`  1. Level 1 - Viewer (Read Only)`);
        console.log(`  2. Level 2 - Operator (Camera Access)`);
        console.log(`  3. Level 3 - Admin (Full Control)\n`);
        const secChoice = await question(`${colors.cyan}Choice:${colors.reset} `);
        
        if (secChoice === '1') {
          city.setSecurityRole('viewer');
          city.changeSecurityLevel('LOW');
          console.log(`\n${colors.yellow}👤 Role: VIEWER${colors.reset}`);
          console.log(`${colors.cyan}Attempting camera activation...${colors.reset}`);
          city.getSecurityProxy().activateCameras();
        } else if (secChoice === '2') {
          city.setSecurityRole('operator');
          city.changeSecurityLevel('MEDIUM');
          console.log(`\n${colors.green}👤 Role: OPERATOR${colors.reset}`);
          await showProgress('Activating surveillance');
          city.getSecurityProxy().activateCameras();
          await sleep(100);
          console.log(`${colors.green}✅ Cameras activated${colors.reset}`);
        } else if (secChoice === '3') {
          city.setSecurityRole('admin');
          city.changeSecurityLevel('HIGH');
          console.log(`\n${colors.red}👤 Role: ADMINISTRATOR${colors.reset}`);
          await showProgress('Activating full security');
          city.getSecurityProxy().activateCameras();
          city.getSecurityProxy().activateAlarms();
          await sleep(100);
          console.log(`${colors.green}✅ Full security access enabled${colors.reset}`);
        }
        break;

      case '8':
        console.log(`${colors.bright}${colors.yellow}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.yellow}💡 LIGHTING CONTROL${colors.reset}\n`);
        console.log(`${colors.cyan}Select action:${colors.reset}`);
        console.log(`  1. Toggle all district lights`);
        console.log(`  2. View lighting status`);
        console.log(`  3. Set brightness level\n`);
        const lightChoice = await question(`${colors.cyan}Choice:${colors.reset} `);
        
        if (lightChoice === '1') {
          console.log(`\n${colors.cyan}Toggling district lighting...${colors.reset}`);
          await sleep(150);
          city.toggleDistrictLighting();
          console.log();
        } else if (lightChoice === '2') {
          const district = city.getLightingDistrict();
          if (district) {
            console.log(`\n${colors.cyan}District Lighting Status:${colors.reset}`);
            console.log(district.getInfo());
          } else {
            console.log(`${colors.red}\n❌ Lighting not initialized${colors.reset}`);
          }
        } else if (lightChoice === '3') {
          console.log(`\n${colors.cyan}Select brightness:${colors.reset}`);
          console.log(`  1. Low (30%)`);
          console.log(`  2. Medium (60%)`);
          console.log(`  3. High (100%)\n`);
          const brightness = await question(`${colors.cyan}Choice:${colors.reset} `);
          const levels = { '1': 0.3, '2': 0.6, '3': 1.0 };
          const level = levels[brightness as '1' | '2' | '3'] || 1.0;
          city.getEnergyMonitor().setLightingActive(true, level);
          console.log(`${colors.green}\n✅ Brightness set to ${level * 100}%${colors.reset}`);
        }
        const energy1 = city.getEnergyMonitor();
        console.log();
        console.log(energy1.getEnergyReport());
        console.log(`${colors.bright}${colors.yellow}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '9':
        console.log(`${colors.bright}${colors.green}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.yellow}⚡ ENERGY MANAGEMENT${colors.reset}\n`);
        console.log(`${colors.cyan}Select option:${colors.reset}`);
        console.log(`  1. View energy report`);
        console.log(`  2. Simulate time passing`);
        console.log(`  3. Energy statistics\n`);
        const energyChoice = await question(`${colors.cyan}Choice:${colors.reset} `);
        
        const energyMon = city.getEnergyMonitor();
        if (energyChoice === '1') {
          console.log();
          console.log(energyMon.getEnergyReport());
        } else if (energyChoice === '2') {
          const hours = await question(`${colors.cyan}How many hours to simulate? (1-24):${colors.reset} `);
          const numHours = Math.min(parseInt(hours) || 1, 24);
          console.log(`\n${colors.cyan}Simulating ${numHours} hours...${colors.reset}\n`);
          for (let i = 0; i < numHours; i++) {
            city.simulateTimePass();
            await sleep(100);
          }
          console.log(energyMon.getEnergyReport());
        } else if (energyChoice === '3') {
          const stats = energyMon.getStats();
          console.log(`\n${colors.cyan}Energy Statistics:${colors.reset}`);
          console.log(`  Total Consumed: ${stats.totalConsumption} kWh`);
          console.log(`  Total Produced: ${stats.totalProduction} kWh`);
          console.log(`  Efficiency: ${stats.efficiency}%`);
          console.log(`  Renewable: ${stats.savingsPercentage}%`);
        }
        console.log(`${colors.bright}${colors.green}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '10':
        console.log(`${colors.bright}${colors.red}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        console.log(`${colors.red}🛑 INITIATING SHUTDOWN SEQUENCE...${colors.reset}\n`);
        await showProgress('Stopping traffic signals');
        await showProgress('Deactivating security systems');
        await showProgress('Turning off street lights');
        await showProgress('Shutting down power grid');
        console.log();
        city.stopCity();
        cityStarted = false;
        await sleep(150);
        console.log(`${colors.yellow}✅ City systems offline${colors.reset}`);
        console.log(`${colors.bright}${colors.red}━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━${colors.reset}`);
        break;

      case '0':
        console.log(`\n${colors.bright}${colors.cyan}╔═══════════════════════════════════════════════════════╗${colors.reset}`);
        console.log(`${colors.bright}${colors.cyan}║                                                       ║${colors.reset}`);
        console.log(`${colors.bright}${colors.yellow}║          Thank you for using Smart City!              ║${colors.reset}`);
        console.log(`${colors.bright}${colors.green}║                                                       ║${colors.reset}`);
        console.log(`${colors.bright}${colors.green}║   Design Patterns: Singleton, Factory, Builder,       ║${colors.reset}`);
        console.log(`${colors.bright}${colors.green}║   Abstract Factory, Composite, Adapter, Proxy, Facade ║${colors.reset}`);
        console.log(`${colors.bright}${colors.cyan}║                                                       ║${colors.reset}`);
        console.log(`${colors.bright}${colors.cyan}╚═══════════════════════════════════════════════════════╝${colors.reset}\n`);
        running = false;
        break;

      default:
        console.log(`${colors.red}❌ Invalid command! Please choose 0-10${colors.reset}`);
    }

    if (running && choice.trim() !== '0') {
      await question(`\n${colors.bright}Press Enter to continue...${colors.reset}`);
      console.clear();
    }
  }

  rl.close();
}

main().catch(console.error);

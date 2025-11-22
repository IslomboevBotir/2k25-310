import { CityController } from './singleton/CityController.js';
import { LightingFactory } from './factories/LightingFactory.js';
import { TrafficSignalBuilder } from './builders/TrafficSignalBuilder.js';
import { LightGroup, LightingHierarchyBuilder } from '../modules/lightning/LightGroup.js';
import { WeatherAdapter } from './adapters/WeatherAdapter.js';
import { SecurityProxy } from './proxy/SecurityProxy.js';
import { EnergyMonitor } from '../modules/energy/EnergyMonitor.js';
import { SubsystemFactoryProvider } from './factories/SubsystemFactory.js';

export class SmartCityFacade {
  private controller: CityController;
  private weatherAdapter: WeatherAdapter;
  private securityProxy: SecurityProxy;
  private energyMonitor: EnergyMonitor;
  private lightingDistrict: LightGroup | null = null;

  constructor() {
    console.log('\n🏙️  Initializing Smart City System...\n');

    this.controller = CityController.getInstance();

    this.weatherAdapter = new WeatherAdapter();
    this.securityProxy = new SecurityProxy('admin');
    this.energyMonitor = new EnergyMonitor();

    this.registerSubsystems();

    console.log('✅ Smart City System initialized!\n');
  }

  private registerSubsystems(): void {
    const lighting = SubsystemFactoryProvider.createSubsystem('lighting');
    const security = SubsystemFactoryProvider.createSubsystem('security');
    const transport = SubsystemFactoryProvider.createSubsystem('transport');
    const energy = SubsystemFactoryProvider.createSubsystem('energy');

    this.controller.registerSubsystem(lighting);
    this.controller.registerSubsystem(security);
    this.controller.registerSubsystem(transport);
    this.controller.registerSubsystem(energy);
  }

  startCity(): void {
    console.log('\n🚀 Starting Smart City...\n');

    this.controller.startAllSystems();

    this.energyMonitor.startMonitoring();

    this.initializeLighting();

    this.adjustForWeather();

    console.log('✅ Smart City is now fully operational!\n');
  }

  stopCity(): void {
    console.log('\n🛑 Shutting down Smart City...\n');

    if (this.lightingDistrict) {
      this.lightingDistrict.turnOff();
    }

    this.energyMonitor.stopMonitoring();

    this.controller.stopAllSystems();

    console.log('✅ Smart City shutdown complete.\n');
  }

  private initializeLighting(): void {
    this.lightingDistrict = LightingHierarchyBuilder.createDistrict(
      'Downtown District',
      ['Main Street', 'Park Avenue', 'Oak Boulevard'],
      8
    );

    this.energyMonitor.setLightingActive(false);

    console.log('💡 Lighting infrastructure initialized');
  }

  private adjustForWeather(): void {
    const weather = this.weatherAdapter.getWeather();
    
    console.log(`🌤️  Weather: ${weather.condition}, ${weather.temperature}°C`);

    if (this.weatherAdapter.shouldActivateLighting() && this.lightingDistrict) {
      console.log('🌧️  Weather requires lighting - activating street lights');
      this.lightingDistrict.turnOn();
      this.energyMonitor.setLightingActive(true, 1.0);
    }
  }

  getCityStatus(): void {
    console.log('\n╔════════════════════════════════════════════════════╗');
    console.log('║          SMART CITY STATUS DASHBOARD               ║');
    console.log('╚════════════════════════════════════════════════════╝\n');

    const stats = this.controller.getSystemStats();
    console.log(`📊 Subsystems: ${stats.activeSubsystems}/${stats.totalSubsystems} active`);
    console.log(`⚡ Energy Efficiency: ${stats.energyEfficiency}%`);
    console.log(`🔒 Security Level: ${stats.securityLevel}\n`);

    console.log(this.weatherAdapter.getWeatherReport());
    console.log('');

    console.log(this.energyMonitor.getEnergyReport());
    console.log('');

    if (this.lightingDistrict) {
      console.log(this.lightingDistrict.getInfo());
      console.log('');
    }

    console.log('🔒 ' + this.securityProxy.getStatus());
    console.log('');
  }

  activateEmergencyMode(): void {
    console.log('\n🚨 ACTIVATING EMERGENCY MODE 🚨\n');

    this.securityProxy.setSecurityLevel('CRITICAL');
    this.energyMonitor.setSecurityLevel('CRITICAL');

    this.securityProxy.activateCameras();
    this.securityProxy.activateAlarms();

    if (this.lightingDistrict) {
      this.lightingDistrict.turnOn();
      this.energyMonitor.setLightingActive(true, 1.5);
    }

    this.energyMonitor.simulateUsage();

    console.log('✅ Emergency mode activated!\n');
  }

  activateEnergySavingMode(): void {
    console.log('\n🌱 ACTIVATING ENERGY SAVING MODE 🌱\n');

    if (this.lightingDistrict && !this.weatherAdapter.shouldActivateLighting()) {
      this.lightingDistrict.turnOff();
      this.energyMonitor.setLightingActive(false);
      console.log('💡 Non-essential lighting deactivated');
    }

    this.securityProxy.setSecurityLevel('MEDIUM');
    this.energyMonitor.setSecurityLevel('MEDIUM');

    this.energyMonitor.simulateUsage();

    console.log('✅ Energy saving mode activated!\n');
  }

  addTrafficSignal(id: string, location: string): void {
    const signal = new TrafficSignalBuilder()
      .setId(id)
      .setLocation(location)
      .setTimings(45, 5, 40)
      .withPedestrianCrossing()
      .withCamera()
      .build();

    signal.start();
    console.log(`✅ Traffic signal ${id} added at ${location}\n`);
  }

  setSecurityRole(role: 'admin' | 'operator' | 'viewer'): void {
    this.securityProxy.setUserRole(role);
  }

  displayDetailedStatus(): void {
    this.controller.displaySystemStatus();
  }

  simulateTimePass(): void {
    this.energyMonitor.simulateUsage();
    console.log('⏰ Time simulation: Energy data updated\n');
  }

  toggleDistrictLighting(): void {
    if (!this.lightingDistrict) {
      console.log('❌ Lighting district not initialized');
      return;
    }

    const info = this.lightingDistrict.getInfo();
    const isOn = info.includes('Lights On: 24');

    if (isOn) {
      this.lightingDistrict.turnOff();
      this.energyMonitor.setLightingActive(false);
      console.log('💡 District lights turned OFF');
    } else {
      this.lightingDistrict.turnOn();
      this.energyMonitor.setLightingActive(true, 1.0);
      console.log('💡 District lights turned ON');
    }

    this.energyMonitor.simulateUsage();
  }

  changeSecurityLevel(level: 'LOW' | 'MEDIUM' | 'HIGH' | 'CRITICAL'): void {
    this.securityProxy.setSecurityLevel(level);
    this.energyMonitor.setSecurityLevel(level);
    this.energyMonitor.simulateUsage();
    console.log(`✅ Security level changed to ${level} (affects energy consumption)`);
  }

  getWeatherAdapter(): WeatherAdapter {
    return this.weatherAdapter;
  }

  getSecurityProxy(): SecurityProxy {
    return this.securityProxy;
  }

  getEnergyMonitor(): EnergyMonitor {
    return this.energyMonitor;
  }

  getLightingDistrict(): LightGroup | null {
    return this.lightingDistrict;
  }
}

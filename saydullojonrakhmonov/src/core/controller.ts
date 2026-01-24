import { SmartCityFactory } from './factories/index.ts';
import { SmartCityBuilder } from './builders/index.ts';
import { SecuritySystem } from '../modules/security/index.ts';
import { SecurityProxy } from './proxy/index.ts';
import { CityMonitor } from './singleton/index.ts';
import { weatherService } from './adapters/index.ts';

export class SmartCityController {
  private static instance: SmartCityController;
  private monitor = CityMonitor.getInstance();
  private city: ReturnType<SmartCityBuilder['build']>;

  private constructor() {
    const factory = new SmartCityFactory();
    const securityProxy = new SecurityProxy(new SecuritySystem());

    this.city = new SmartCityBuilder(factory)
      .addLighting()
      .addTransport()
      .addSecurity(securityProxy)
      .addEnergy()
      .build();

    this.monitor.logStatus("SmartCity Controller (Facade) ready");
  }

  static getInstance(): SmartCityController {
    if (!SmartCityController.instance) {
      SmartCityController.instance = new SmartCityController();
    }
    return SmartCityController.instance;
  }

  startDay() {
    console.log("\n MORNING ROUTINE === \n");
    this.city.lighting?.turnOff();
    this.city.transport?.startTraffic();
    this.city.energy?.monitorUsage();
  }

  startNight() {
    console.log("\n === NIGHT ROUTINE === \n");
    const isNight = weatherService.isNight();
    this.city.lighting?.autoMode(isNight);
    this.city.security?.login();
    this.city.security?.activateCameras();
    this.city.energy?.saveEnergy();
  }

  emergency() {
    console.log("\n === EMERGENCY MODE === \n");
    this.city.transport?.stopTraffic();
    this.city.security?.detectThreat();
  }
}
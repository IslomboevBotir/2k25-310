import { SubsystemFactory } from '../factories/index.ts';
import type { ILightingSystem, ITransportSystem, IEnergySystem } from '../../modules/index.ts';
import { SecurityProxy } from '../proxy/index.ts';

export class SmartCityBuilder {
  private factory: SubsystemFactory;
  public lighting?: ILightingSystem;
  public transport?: ITransportSystem;
  public security?: SecurityProxy;
  public energy?: IEnergySystem;

  constructor(factory: SubsystemFactory) {
    this.factory = factory;
  }

  addLighting() {
    this.lighting = this.factory.createLighting();
    return this;
  }

  addTransport() {
    this.transport = this.factory.createTransport();
    return this;
  }

  addSecurity(proxy: SecurityProxy) {
    this.security = proxy;
    return this;
  }

  addEnergy() {
    this.energy = this.factory.createEnergy();
    return this;
  }

  build() {
    return {
      lighting: this.lighting,
      transport: this.transport,
      security: this.security,
      energy: this.energy
    };
  }
}
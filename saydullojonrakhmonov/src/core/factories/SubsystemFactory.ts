import type { ILightingSystem } from "../../modules/lighting/index.ts";
import type { ITransportSystem } from "../../modules/transport/index.ts";
import type { ISecuritySystem } from "../../modules/security/index.ts";
import type { IEnergySystem } from "../../modules/energy/index.ts";

import { LightingSystem } from "../../modules/lighting/index.ts";
import { TransportSystem } from "../../modules/transport/index.ts";
import { SecuritySystem } from "../../modules/security/index.ts";
import { EnergySystem } from "../../modules/energy/index.ts";


export type { ILightingSystem, ITransportSystem, ISecuritySystem, IEnergySystem };

export abstract class SubsystemFactory {
  abstract createLighting(): ILightingSystem;
  abstract createTransport(): ITransportSystem;
  abstract createSecurity(): ISecuritySystem;
  abstract createEnergy(): IEnergySystem;
}

export class SmartCityFactory extends SubsystemFactory {
  createLighting() {
    return new LightingSystem();
  }
  createTransport() {
    return new TransportSystem();
  }
  createSecurity() {
    return new SecuritySystem();
  }
  createEnergy() {
    return new EnergySystem();
  }
}

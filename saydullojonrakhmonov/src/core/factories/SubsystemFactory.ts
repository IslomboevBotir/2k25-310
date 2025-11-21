import type LightingSystem = require("../../modules/lighting/LightingSystem");
import type TransportSystem = require("../../modules/transport/TransportSystem");


export abstract class SubsystemFactory {
   abstract createLighting(): LightingSystem;
   abstract createTransport(): TransportSystem.ITransportSystem;
  abstract createSecurity(): ISecuritySystem;
  abstract createEnergy(): IEnergySystem;
}
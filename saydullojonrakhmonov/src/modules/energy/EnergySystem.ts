export interface IEnergySystem {
  saveEnergy(): void;
  monitorUsage(): void;
}

export class EnergySystem implements IEnergySystem {
  saveEnergy(): void {
    console.log("Energy saving mode on");
  }
  monitorUsage(): void {
    console.log("Energy usage 58% normal");
  }
}



export interface ILightingSystem {
  turnOn(): void;
  turnOff(): void;
  autoMode(isNight: boolean): void;
}

export class LightingSystem implements ILightingSystem {
  turnOn(): void {
    console.log("Lights ON across the city");
  }

  turnOff(): void {
    console.log("Lights OFF");
  }

  autoMode(isNight: boolean): void {
    if (isNight) {
      this.turnOn();
    } else {
      this.turnOff();
    }
  }
}
 
export interface ILightSystem {
  tunrOn(): void;
  tunrOff(): void;
  autoMode(isNight: boolean): void;
}

export class LightingSystem implements ILightSystem {
  tunrOn() {
    console.log("Lights in accross the city");
  }
  tunrOff() {
    console.log("Ligthts off");
  }
  autoMode(isNight: boolean): void {
    isNight ? this.tunrOn() : this.tunrOff();
  }
}

 
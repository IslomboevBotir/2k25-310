import type { LightType, LightConfig, Controllable } from '../types.js';

export abstract class Light implements Controllable {
  protected isOn: boolean = false;
  protected id: string;
  protected brightness: number;
  protected location: string;

  constructor(config: LightConfig) {
    this.id = config.id;
    this.brightness = config.brightness || 100;
    this.location = config.location || 'Unknown';
  }

  abstract getType(): LightType;
  abstract getEnergyConsumption(): number;

  turnOn(): void {
    this.isOn = true;
  }

  turnOff(): void {
    this.isOn = false;
  }

  getState(): boolean {
    return this.isOn;
  }

  setBrightness(level: number): void {
    this.brightness = Math.max(0, Math.min(100, level));
  }

  getInfo(): string {
    return `${this.getType()} Light [${this.id}] at ${this.location} - ${
      this.isOn ? `ON (${this.brightness}%)` : 'OFF'
    }`;
  }
}

export class LEDLight extends Light {
  getType(): LightType {
    return 'LED';
  }

  getEnergyConsumption(): number {
    return this.isOn ? (10 * this.brightness) / 100 : 0;
  }
}

export class HalogenLight extends Light {
  getType(): LightType {
    return 'Halogen';
  }

  getEnergyConsumption(): number {
    return this.isOn ? (50 * this.brightness) / 100 : 0;
  }
}

export class SolarLight extends Light {
  getType(): LightType {
    return 'Solar';
  }

  getEnergyConsumption(): number {
    return this.isOn ? (5 * this.brightness) / 100 : 0;
  }
}

export class LightingFactory {
  static createLight(config: LightConfig): Light {
    switch (config.type) {
      case 'LED':
        return new LEDLight(config);
      case 'Halogen':
        return new HalogenLight(config);
      case 'Solar':
        return new SolarLight(config);
      default:
        return new LEDLight(config);
    }
  }

  static createLights(configs: LightConfig[]): Light[] {
    return configs.map(config => this.createLight(config));
  }

  static createStreetLighting(
    streetName: string,
    lightCount: number,
    type: LightType = 'LED'
  ): Light[] {
    const lights: Light[] = [];
    for (let i = 1; i <= lightCount; i++) {
      lights.push(
        this.createLight({
          id: `${streetName}-L${i}`,
          type,
          location: `${streetName}, Position ${i}`,
          brightness: 100
        })
      );
    }
    return lights;
  }
}

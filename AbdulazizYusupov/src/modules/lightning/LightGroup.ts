import { Light, LightingFactory } from '../../core/factories/LightingFactory.js';
import type { Controllable } from '../../core/types.js';

export interface LightComponent extends Controllable {
  getInfo(): string;
  getEnergyConsumption(): number;
}

export class LightGroup implements LightComponent {
  private name: string;
  private children: LightComponent[] = [];

  constructor(name: string) {
    this.name = name;
  }

  add(component: LightComponent): void {
    this.children.push(component);
  }

  remove(component: LightComponent): void {
    const index = this.children.indexOf(component);
    if (index > -1) {
      this.children.splice(index, 1);
    }
  }

  getChildren(): LightComponent[] {
    return [...this.children];
  }

  turnOn(): void {
    console.log(`💡 Turning on light group: ${this.name}`);
    this.children.forEach(child => child.turnOn());
  }

  turnOff(): void {
    console.log(`💡 Turning off light group: ${this.name}`);
    this.children.forEach(child => child.turnOff());
  }

  getState(): boolean {
    return this.children.some(child => child.getState());
  }

  getEnergyConsumption(): number {
    return this.children.reduce(
      (total, child) => total + child.getEnergyConsumption(),
      0
    );
  }

  getInfo(): string {
    const totalLights = this.countTotalLights();
    const onLights = this.countLightsOn();
    const energy = this.getEnergyConsumption();

    return `
      Light Group: ${this.name}
      Total Lights: ${totalLights}
      Lights On: ${onLights}
      Energy Consumption: ${energy.toFixed(2)}W
      Status: ${this.getState() ? 'Active' : 'Inactive'}
    `.trim();
  }

  private countTotalLights(): number {
    return this.children.reduce((count, child) => {
      if (child instanceof LightGroup) {
        return count + child.countTotalLights();
      }
      return count + 1;
    }, 0);
  }

  private countLightsOn(): number {
    return this.children.reduce((count, child) => {
      if (child instanceof LightGroup) {
        return count + child.countLightsOn();
      }
      return count + (child.getState() ? 1 : 0);
    }, 0);
  }

  getName(): string {
    return this.name;
  }

  getChildCount(): number {
    return this.children.length;
  }
}

export class LightingHierarchyBuilder {
  static createDistrict(
    districtName: string,
    streets: string[],
    lightsPerStreet: number = 10
  ): LightGroup {
    const district = new LightGroup(districtName);

    streets.forEach(streetName => {
      const streetGroup = new LightGroup(streetName);
      
      const lights = LightingFactory.createStreetLighting(
        streetName,
        lightsPerStreet,
        'LED'
      );

      lights.forEach(light => streetGroup.add(light));

      district.add(streetGroup);
    });

    return district;
  }

  static createStreet(
    streetName: string,
    lightCount: number = 10,
    lightType: 'LED' | 'Halogen' | 'Solar' = 'LED'
  ): LightGroup {
    const streetGroup = new LightGroup(streetName);
    const lights = LightingFactory.createStreetLighting(
      streetName,
      lightCount,
      lightType
    );
    
    lights.forEach(light => streetGroup.add(light));
    return streetGroup;
  }
}
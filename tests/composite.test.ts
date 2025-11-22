import { LightGroup, LightingHierarchyBuilder } from '../src/modules/lightning/LightGroup';
import { LightingFactory } from '../src/core/factories/LightingFactory';

describe('Composite Pattern - LightGroup', () => {
  test('should manage lights in hierarchical structure', () => {
    const district = new LightGroup('District');
    const street = new LightGroup('Street');
    const light = LightingFactory.createLight({ id: 'L1', type: 'LED' });

    street.add(light);
    district.add(street);
    district.turnOn();

    expect(light.getState()).toBe(true);
  });

  test('should calculate total energy for all lights', () => {
    const group = new LightGroup('Street');
    const light1 = LightingFactory.createLight({ id: 'L1', type: 'LED' });
    const light2 = LightingFactory.createLight({ id: 'L2', type: 'LED' });

    group.add(light1);
    group.add(light2);
    group.turnOn();

    const energy = group.getEnergyConsumption();
    expect(energy).toBeGreaterThan(0);
  });

  test('should use builder to create complex hierarchies', () => {
    const district = LightingHierarchyBuilder.createDistrict('Downtown', ['Main St', 'Park Ave'], 4);
    expect(district.getChildCount()).toBeGreaterThan(0);
  });
});

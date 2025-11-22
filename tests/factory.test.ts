import { LightingFactory, LEDLight, HalogenLight, SolarLight } from '../src/core/factories/LightingFactory';
import { SubsystemFactoryProvider } from '../src/core/factories/SubsystemFactory';

/**
 * Tests for Factory Pattern - LightingFactory and SubsystemFactory
 */
describe('Factory Pattern - LightingFactory', () => {
  test('should create LED light', () => {
    const light = LightingFactory.createLight({
      id: 'LED-001',
      type: 'LED',
      location: 'Test Street'
    });

    expect(light).toBeInstanceOf(LEDLight);
    expect(light.getType()).toBe('LED');
  });

  test('should create Halogen light', () => {
    const light = LightingFactory.createLight({
      id: 'HAL-001',
      type: 'Halogen',
      location: 'Test Street'
    });

    expect(light).toBeInstanceOf(HalogenLight);
    expect(light.getType()).toBe('Halogen');
  });

  test('should create Solar light', () => {
    const light = LightingFactory.createLight({
      id: 'SOL-001',
      type: 'Solar',
      location: 'Test Street'
    });

    expect(light).toBeInstanceOf(SolarLight);
    expect(light.getType()).toBe('Solar');
  });

  test('should create multiple lights', () => {
    const configs = [
      { id: 'L1', type: 'LED' as const },
      { id: 'L2', type: 'Halogen' as const },
      { id: 'L3', type: 'Solar' as const }
    ];

    const lights = LightingFactory.createLights(configs);
    expect(lights).toHaveLength(3);
  });

  test('should create street lighting', () => {
    const lights = LightingFactory.createStreetLighting('Main St', 5, 'LED');
    
    expect(lights).toHaveLength(5);
    lights.forEach(light => {
      expect(light.getType()).toBe('LED');
    });
  });

  test('LED should have lower energy consumption than Halogen', () => {
    const led = LightingFactory.createLight({ id: 'LED', type: 'LED' });
    const halogen = LightingFactory.createLight({ id: 'HAL', type: 'Halogen' });

    led.turnOn();
    halogen.turnOn();

    expect(led.getEnergyConsumption()).toBeLessThan(halogen.getEnergyConsumption());
  });
});

describe('Abstract Factory Pattern - SubsystemFactory', () => {
  test('should create lighting subsystem', () => {
    const subsystem = SubsystemFactoryProvider.createSubsystem('lighting');
    expect(subsystem.name).toBe('Lighting System');
  });

  test('should create security subsystem', () => {
    const subsystem = SubsystemFactoryProvider.createSubsystem('security');
    expect(subsystem.name).toBe('Security System');
  });

  test('should create transport subsystem', () => {
    const subsystem = SubsystemFactoryProvider.createSubsystem('transport');
    expect(subsystem.name).toBe('Transport System');
  });

  test('should create energy subsystem', () => {
    const subsystem = SubsystemFactoryProvider.createSubsystem('energy');
    expect(subsystem.name).toBe('Energy Management System');
  });

  test('subsystems should be initially inactive', () => {
    const subsystem = SubsystemFactoryProvider.createSubsystem('lighting');
    expect(subsystem.isActive).toBe(false);
  });

  test('subsystems should be activatable', () => {
    const subsystem = SubsystemFactoryProvider.createSubsystem('lighting');
    subsystem.start();
    expect(subsystem.isActive).toBe(true);
  });
});

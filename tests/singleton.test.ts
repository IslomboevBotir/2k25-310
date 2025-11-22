import { CityController } from '../src/core/singleton/CityController';
import { SubsystemFactoryProvider } from '../src/core/factories/SubsystemFactory';

/**
 * Tests for Singleton Pattern - CityController
 */
describe('Singleton Pattern - CityController', () => {
  // Reset singleton before each test
  beforeEach(() => {
    CityController.resetInstance();
  });

  test('should return the same instance on multiple calls', () => {
    const instance1 = CityController.getInstance();
    const instance2 = CityController.getInstance();

    expect(instance1).toBe(instance2);
  });

  test('should maintain state across instances', () => {
    const instance1 = CityController.getInstance();
    const subsystem = SubsystemFactoryProvider.createSubsystem('lighting');
    instance1.registerSubsystem(subsystem);

    const instance2 = CityController.getInstance();
    expect(instance2.getSubsystemCount()).toBe(1);
  });

  test('should register and retrieve subsystems', () => {
    const controller = CityController.getInstance();
    const lightingSystem = SubsystemFactoryProvider.createSubsystem('lighting');

    controller.registerSubsystem(lightingSystem);

    const retrieved = controller.getSubsystem('Lighting System');
    expect(retrieved).toBeDefined();
    expect(retrieved?.name).toBe('Lighting System');
  });

  test('should start all systems', () => {
    const controller = CityController.getInstance();
    const lighting = SubsystemFactoryProvider.createSubsystem('lighting');
    const security = SubsystemFactoryProvider.createSubsystem('security');

    controller.registerSubsystem(lighting);
    controller.registerSubsystem(security);

    controller.startAllSystems();

    expect(lighting.isActive).toBe(true);
    expect(security.isActive).toBe(true);
  });

  test('should stop all systems', () => {
    const controller = CityController.getInstance();
    const lighting = SubsystemFactoryProvider.createSubsystem('lighting');

    controller.registerSubsystem(lighting);
    controller.startAllSystems();
    controller.stopAllSystems();

    expect(lighting.isActive).toBe(false);
  });

  test('should get system statistics', () => {
    const controller = CityController.getInstance();
    const lighting = SubsystemFactoryProvider.createSubsystem('lighting');
    const security = SubsystemFactoryProvider.createSubsystem('security');

    controller.registerSubsystem(lighting);
    controller.registerSubsystem(security);
    controller.startAllSystems();

    const stats = controller.getSystemStats();
    expect(stats.totalSubsystems).toBe(2);
    expect(stats.activeSubsystems).toBe(2);
  });

  test('should return all subsystem names', () => {
    const controller = CityController.getInstance();
    controller.registerSubsystem(SubsystemFactoryProvider.createSubsystem('lighting'));
    controller.registerSubsystem(SubsystemFactoryProvider.createSubsystem('security'));

    const names = controller.getAllSubsystemNames();
    expect(names).toContain('Lighting System');
    expect(names).toContain('Security System');
  });
});

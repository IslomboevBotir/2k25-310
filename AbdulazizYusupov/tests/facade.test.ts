import { SmartCityFacade } from '../src/core/SmartCityFacade';
import { CityController } from '../src/core/singleton/CityController';

/**
 * Tests for Facade Pattern - SmartCityFacade
 */
describe('Facade Pattern - SmartCityFacade', () => {
  let facade: SmartCityFacade;

  beforeEach(() => {
    CityController.resetInstance();
    facade = new SmartCityFacade();
  });

  test('should initialize with all subsystems', () => {
    const controller = CityController.getInstance();
    expect(controller.getSubsystemCount()).toBeGreaterThan(0);
  });

  test('should start city successfully', () => {
    facade.startCity();
    const controller = CityController.getInstance();
    const stats = controller.getSystemStats();
    expect(stats.activeSubsystems).toBeGreaterThan(0);
  });

  test('should stop city successfully', () => {
    facade.startCity();
    facade.stopCity();
    const controller = CityController.getInstance();
    const stats = controller.getSystemStats();
    expect(stats.activeSubsystems).toBe(0);
  });

  test('should provide weather adapter', () => {
    const weatherAdapter = facade.getWeatherAdapter();
    expect(weatherAdapter).toBeDefined();
    const weather = weatherAdapter.getWeather();
    expect(weather).toBeDefined();
  });

  test('should provide security proxy', () => {
    const securityProxy = facade.getSecurityProxy();
    expect(securityProxy).toBeDefined();
  });

  test('should provide energy monitor', () => {
    const energyMonitor = facade.getEnergyMonitor();
    expect(energyMonitor).toBeDefined();
  });

  test('should simulate time passing', () => {
    facade.startCity();
    facade.simulateTimePass();
    const energyMonitor = facade.getEnergyMonitor();
    const data = energyMonitor.getLatestData();
    expect(data).toBeDefined();
  });

  test('should activate emergency mode', () => {
    facade.startCity();
    facade.activateEmergencyMode();
    const securityProxy = facade.getSecurityProxy();
    expect(securityProxy.getSecurityLevel()).toBe('CRITICAL');
  });

  test('should activate energy saving mode', () => {
    facade.startCity();
    facade.activateEnergySavingMode();
    const securityProxy = facade.getSecurityProxy();
    expect(securityProxy.getSecurityLevel()).toBe('MEDIUM');
  });

  test('should add traffic signal', () => {
    facade.startCity();
    facade.addTrafficSignal('TS-TEST', 'Test Location');
    // Should not throw error
  });

  test('should change security role', () => {
    facade.setSecurityRole('operator');
    const securityProxy = facade.getSecurityProxy();
    expect(securityProxy.getUserRole()).toBe('operator');
  });

  test('should get city status without error', () => {
    facade.startCity();
    expect(() => facade.getCityStatus()).not.toThrow();
  });

  test('should display detailed status without error', () => {
    facade.startCity();
    expect(() => facade.displayDetailedStatus()).not.toThrow();
  });

  test('should have lighting district after start', () => {
    facade.startCity();
    const lightingDistrict = facade.getLightingDistrict();
    expect(lightingDistrict).toBeDefined();
  });
});

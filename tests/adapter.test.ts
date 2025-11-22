import { WeatherAdapter } from '../src/core/adapters/WeatherAdapter';

describe('Adapter Pattern - WeatherAdapter', () => {
  let weatherAdapter: WeatherAdapter;

  beforeEach(() => {
    weatherAdapter = new WeatherAdapter();
  });

  test('should adapt external weather API to internal format', () => {
    const weather = weatherAdapter.getWeather();

    expect(weather).toBeDefined();
    expect(weather.temperature).toBeDefined();
    expect(weather.humidity).toBeDefined();
    expect(weather.condition).toBeDefined();
  });

  test('should determine lighting activation based on weather', () => {
    const shouldActivate = weatherAdapter.shouldActivateLighting();
    expect(typeof shouldActivate).toBe('boolean');
  });
});

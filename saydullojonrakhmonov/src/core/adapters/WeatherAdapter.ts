interface OldWeatherAPI {
  getTempInF(): number;
}

// Our interface
export interface IWeatherService {
  isNight(): boolean;
}

export class WeatherAdapter implements IWeatherService {
  private api: OldWeatherAPI;

  constructor(api: OldWeatherAPI) {
    this.api = api;
  }

  isNight(): boolean {
    const tempF = this.api.getTempInF();
    const isCold = tempF < 50;
    console.log(`🌡️ Adapted temp: ${tempF}°F → Night mode: ${isCold}`);
    return isCold;
  }
}

class MockWeatherAPI implements OldWeatherAPI {
  getTempInF() { return 45; }
}

export const weatherService = new WeatherAdapter(new MockWeatherAPI());
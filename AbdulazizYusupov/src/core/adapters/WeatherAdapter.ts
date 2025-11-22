import type { WeatherData, ExternalWeatherResponse } from '../types.js';

class ExternalWeatherService {
  fetchWeather(): ExternalWeatherResponse {
    return {
      temp_celsius: 22 + Math.random() * 10,
      humidity_percent: 45 + Math.random() * 30,
      weather_desc: this.getRandomWeatherCondition(),
      wind_kmh: Math.random() * 30,
      recorded_at: new Date().toISOString()
    };
  }

  private getRandomWeatherCondition(): string {
    const conditions = [
      'Clear Sky',
      'Partly Cloudy',
      'Overcast',
      'Light Rain',
      'Heavy Rain',
      'Sunny'
    ];
    return conditions[Math.floor(Math.random() * conditions.length)] as string;
  }
}

export class WeatherAdapter {
  private externalService: ExternalWeatherService;

  constructor() {
    this.externalService = new ExternalWeatherService();
  }

  getWeather(): WeatherData {
    const externalData = this.externalService.fetchWeather();

    const adaptedData: WeatherData = {
      temperature: Math.round(externalData.temp_celsius * 10) / 10,
      humidity: Math.round(externalData.humidity_percent),
      condition: this.normalizeWeatherCondition(externalData.weather_desc),
      windSpeed: Math.round(externalData.wind_kmh * 10) / 10,
      timestamp: new Date(externalData.recorded_at)
    };

    return adaptedData;
  }

  private normalizeWeatherCondition(externalCondition: string): string {
    const conditionMap: { [key: string]: string } = {
      'Clear Sky': 'Clear',
      'Partly Cloudy': 'Partly Cloudy',
      'Overcast': 'Cloudy',
      'Light Rain': 'Rainy',
      'Heavy Rain': 'Rainy',
      'Sunny': 'Clear'
    };

    return conditionMap[externalCondition] || externalCondition;
  }

  getWeatherReport(): string {
    const weather = this.getWeather();
    
    return `
╔════════════════════════════════════════╗
║         WEATHER REPORT                 ║
╚════════════════════════════════════════╝
  🌡️  Temperature: ${weather.temperature}°C
  💧  Humidity: ${weather.humidity}%
  🌤️  Condition: ${weather.condition}
  💨  Wind Speed: ${weather.windSpeed} km/h
  🕐  Updated: ${weather.timestamp.toLocaleTimeString()}
    `.trim();
  }

  shouldActivateLighting(): boolean {
    const weather = this.getWeather();
    
    return weather.condition === 'Rainy' || 
           weather.condition === 'Cloudy';
  }

  getTemperatureInFahrenheit(): number {
    const weather = this.getWeather();
    return Math.round((weather.temperature * 9/5 + 32) * 10) / 10;
  }
}
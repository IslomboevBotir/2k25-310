export interface Subsystem {
  name: string;
  isActive: boolean;
  start(): void;
  stop(): void;
  getStatus(): string;
}

export interface Controllable {
  turnOn(): void;
  turnOff(): void;
  getState(): boolean;
}

export type LightType = 'LED' | 'Halogen' | 'Solar';

export interface LightConfig {
  id: string;
  type: LightType;
  brightness?: number;
  location?: string;
}

export type SignalState = 'RED' | 'YELLOW' | 'GREEN';

export interface TrafficSignalConfig {
  id?: string;
  location?: string;
  redDuration?: number;
  yellowDuration?: number;
  greenDuration?: number;
  hasPedestrianCrossing?: boolean;
  hasCamera?: boolean;
}

export type SecurityLevel = 'LOW' | 'MEDIUM' | 'HIGH' | 'CRITICAL';

export interface SecurityEvent {
  timestamp: Date;
  type: string;
  location: string;
  severity: SecurityLevel;
  description: string;
}

export interface EnergyData {
  consumption: number;
  production: number;
  timestamp: Date;
}

export interface EnergyStats {
  totalConsumption: number;
  totalProduction: number;
  efficiency: number;
  savingsPercentage: number;
}

export interface WeatherData {
  temperature: number;
  humidity: number;
  condition: string;
  windSpeed: number;
  timestamp: Date;
}

export interface ExternalWeatherResponse {
  temp_celsius: number;
  humidity_percent: number;
  weather_desc: string;
  wind_kmh: number;
  recorded_at: string;
}

export interface SystemStats {
  totalSubsystems: number;
  activeSubsystems: number;
  energyEfficiency: number;
  securityLevel: SecurityLevel;
  weatherCondition: string;
}

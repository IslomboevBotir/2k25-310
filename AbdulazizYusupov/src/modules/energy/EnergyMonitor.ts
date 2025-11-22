import type { EnergyData, EnergyStats } from '../../core/types.js';

export class EnergyMonitor {
  private dataPoints: EnergyData[] = [];
  private isMonitoring: boolean = false;
  private securityMultiplier: number = 1.0;
  private lightingMultiplier: number = 1.0;
  private baseConsumption: number = 850;
  private baseProduction: number = 600;

  startMonitoring(): void {
    this.isMonitoring = true;
    console.log('⚡ Energy monitoring started');
    
    this.recordData(this.baseConsumption, this.baseProduction);
  }

  stopMonitoring(): void {
    this.isMonitoring = false;
    console.log('⚡ Energy monitoring stopped');
  }

  recordData(consumption: number, production: number): void {
    const dataPoint: EnergyData = {
      consumption,
      production,
      timestamp: new Date()
    };
    
    this.dataPoints.push(dataPoint);
    
    if (this.dataPoints.length > 100) {
      this.dataPoints.shift();
    }
  }

  simulateUsage(): void {
    if (!this.isMonitoring) {
      console.log('⚠️  Cannot simulate - monitoring not active');
      return;
    }

    const baseConsumption = 800 + Math.random() * 400;
    const baseProduction = 500 + Math.random() * 300;
    
    const totalConsumption = baseConsumption * this.securityMultiplier * this.lightingMultiplier;
    const totalProduction = baseProduction;
    
    this.recordData(totalConsumption, totalProduction);
  }

  setSecurityLevel(level: 'LOW' | 'MEDIUM' | 'HIGH' | 'CRITICAL'): void {
    switch(level) {
      case 'LOW':
        this.securityMultiplier = 0.8;
        break;
      case 'MEDIUM':
        this.securityMultiplier = 1.0;
        break;
      case 'HIGH':
        this.securityMultiplier = 1.3;
        break;
      case 'CRITICAL':
        this.securityMultiplier = 1.8;
        break;
    }
    this.updateEnergyUsage();
  }

  setLightingActive(active: boolean, intensity: number = 1.0): void {
    this.lightingMultiplier = active ? (0.5 + intensity * 0.5) : 0.3;
    this.updateEnergyUsage();
  }

  private updateEnergyUsage(): void {
    if (this.isMonitoring) {
      const consumption = this.baseConsumption * this.securityMultiplier * this.lightingMultiplier;
      this.recordData(consumption, this.baseProduction);
    }
  }

  getStats(): EnergyStats {
    if (this.dataPoints.length === 0) {
      return {
        totalConsumption: 0,
        totalProduction: 0,
        efficiency: 0,
        savingsPercentage: 0
      };
    }

    const totalConsumption = this.dataPoints.reduce(
      (sum, data) => sum + data.consumption,
      0
    );
    
    const totalProduction = this.dataPoints.reduce(
      (sum, data) => sum + data.production,
      0
    );

    const avgConsumption = totalConsumption / this.dataPoints.length;
    const avgProduction = totalProduction / this.dataPoints.length;
    
    const efficiency = avgConsumption > 0 
      ? (avgProduction / avgConsumption) * 100 
      : 0;
    
    const savingsPercentage = avgConsumption > 0
      ? ((avgProduction / avgConsumption) * 100)
      : 0;

    return {
      totalConsumption: Math.round(totalConsumption),
      totalProduction: Math.round(totalProduction),
      efficiency: Math.round(efficiency * 10) / 10,
      savingsPercentage: Math.round(savingsPercentage * 10) / 10
    };
  }

  getLatestData(): EnergyData | null {
    if (this.dataPoints.length === 0) return null;
    return this.dataPoints[this.dataPoints.length - 1] as EnergyData;
  }

  getAllData(): EnergyData[] {
    return [...this.dataPoints];
  }

  getStatus(): string {
    const latest = this.getLatestData();
    const stats = this.getStats();

    if (!latest) {
      return 'No energy data recorded yet';
    }

    return `
      Energy Monitoring: ${this.isMonitoring ? 'Active' : 'Inactive'}
      Latest Consumption: ${latest.consumption.toFixed(1)} kWh
      Latest Production: ${latest.production.toFixed(1)} kWh
      Overall Efficiency: ${stats.efficiency}%
      Renewable Energy Contribution: ${stats.savingsPercentage}%
    `.trim();
  }

  isActive(): boolean {
    return this.isMonitoring;
  }

  clearData(): void {
    this.dataPoints = [];
    console.log('🗑️  Energy data cleared');
  }

  getEnergyReport(): string {
    const stats = this.getStats();
    const latest = this.getLatestData();

    return `
╔════════════════════════════════════════╗
║        ENERGY REPORT                   ║
╚════════════════════════════════════════╝
  ⚡ Total Consumption: ${stats.totalConsumption} kWh
  🌱 Total Production: ${stats.totalProduction} kWh
  📊 Efficiency: ${stats.efficiency}%
  💚 Renewable Contribution: ${stats.savingsPercentage}%
  🕐 Last Updated: ${latest ? latest.timestamp.toLocaleTimeString() : 'N/A'}
    `.trim();
  }
}

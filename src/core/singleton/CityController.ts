import type { Subsystem, SystemStats, SecurityLevel } from '../types.js';

export class CityController {
  private static instance: CityController | null = null;

  private subsystems: Map<string, Subsystem> = new Map();

  private constructor() {
    console.log('🏙️  Initializing Smart City Controller...');
  }

  public static getInstance(): CityController {
    if (!CityController.instance) {
      CityController.instance = new CityController();
    }
    return CityController.instance;
  }

  public registerSubsystem(subsystem: Subsystem): void {
    this.subsystems.set(subsystem.name, subsystem);
    console.log(`✅ Subsystem "${subsystem.name}" registered`);
  }

  public getSubsystem(name: string): Subsystem | undefined {
    return this.subsystems.get(name);
  }

  public startAllSystems(): void {
    console.log('\n🚀 Starting all city systems...');
    this.subsystems.forEach((subsystem) => {
      subsystem.start();
    });
    console.log('✅ All systems started successfully!\n');
  }

  public stopAllSystems(): void {
    console.log('\n🛑 Stopping all city systems...');
    this.subsystems.forEach((subsystem) => {
      subsystem.stop();
    });
    console.log('✅ All systems stopped successfully!\n');
  }

  public getSystemStats(): SystemStats {
    const totalSubsystems = this.subsystems.size;
    const activeSubsystems = Array.from(this.subsystems.values()).filter(
      (s) => s.isActive
    ).length;

    return {
      totalSubsystems,
      activeSubsystems,
      energyEfficiency: 85,
      securityLevel: 'MEDIUM' as SecurityLevel,
      weatherCondition: 'Clear'
    };
  }

  public displaySystemStatus(): void {
    console.log('\n╔════════════════════════════════════════════════════╗');
    console.log('║          SMART CITY SYSTEM STATUS                 ║');
    console.log('╚════════════════════════════════════════════════════╝\n');

    if (this.subsystems.size === 0) {
      console.log('⚠️  No subsystems registered yet.\n');
      return;
    }

    this.subsystems.forEach((subsystem, name) => {
      const status = subsystem.isActive ? '🟢 ACTIVE' : '🔴 INACTIVE';
      console.log(`📌 ${name}: ${status}`);
      console.log(`   ${subsystem.getStatus()}\n`);
    });
  }

  public static resetInstance(): void {
    CityController.instance = null;
  }

  public getSubsystemCount(): number {
    return this.subsystems.size;
  }

  public getAllSubsystemNames(): string[] {
    return Array.from(this.subsystems.keys());
  }
}

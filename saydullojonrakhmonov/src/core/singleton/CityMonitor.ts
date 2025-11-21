export class CityMonitor {
  private static instance: CityMonitor;
  private constructor() {
    console.log("🌆 City Monitor initialized (Singleton)");
  }

  static getInstance(): CityMonitor {
    if (!CityMonitor.instance) {
      CityMonitor.instance = new CityMonitor();
    }
    return CityMonitor.instance;
  }

  logStatus(message: string) {
    console.log(`[MONITOR] ${message}`);
  }
}
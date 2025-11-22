import type { SecurityLevel, SecurityEvent } from '../../core/types.js';

export class SecuritySystem {
  private events: SecurityEvent[] = [];
  private currentLevel: SecurityLevel = 'MEDIUM';
  private camerasActive: boolean = false;
  private alarmsActive: boolean = false;

  activateCameras(): void {
    this.camerasActive = true;
    this.logEvent('LOW', 'All Locations', 'Security cameras activated');
    console.log('📹 Security cameras are now active');
  }

  deactivateCameras(): void {
    this.camerasActive = false;
    this.logEvent('LOW', 'All Locations', 'Security cameras deactivated');
    console.log('📹 Security cameras deactivated');
  }

  activateAlarms(): void {
    this.alarmsActive = true;
    this.logEvent('LOW', 'All Locations', 'Alarm systems activated');
    console.log('🚨 Alarm systems are now active');
  }

  deactivateAlarms(): void {
    this.alarmsActive = false;
    this.logEvent('LOW', 'All Locations', 'Alarm systems deactivated');
    console.log('🚨 Alarm systems deactivated');
  }

  setSecurityLevel(level: SecurityLevel): void {
    this.currentLevel = level;
    this.logEvent('MEDIUM', 'System', `Security level changed to ${level}`);
    console.log(`🔒 Security level set to: ${level}`);
  }

  getSecurityLevel(): SecurityLevel {
    return this.currentLevel;
  }

  logEvent(severity: SecurityLevel, location: string, description: string): void {
    const event: SecurityEvent = {
      timestamp: new Date(),
      type: severity === 'CRITICAL' ? 'ALERT' : 'INFO',
      location,
      severity,
      description
    };
    this.events.push(event);
  }

  getEvents(): SecurityEvent[] {
    return [...this.events];
  }

  getRecentEvents(): SecurityEvent[] {
    return this.events.slice(-10);
  }

  clearEvents(): void {
    this.events = [];
    console.log('🗑️  Security event log cleared');
  }

  getStatus(): string {
    return `
      Security Level: ${this.currentLevel}
      Cameras: ${this.camerasActive ? 'Active' : 'Inactive'}
      Alarms: ${this.alarmsActive ? 'Active' : 'Inactive'}
      Total Events: ${this.events.length}
    `.trim();
  }

  areCamerasActive(): boolean {
    return this.camerasActive;
  }

  areAlarmsActive(): boolean {
    return this.alarmsActive;
  }
}

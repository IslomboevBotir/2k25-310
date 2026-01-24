import { SecuritySystem } from '../../modules/security/SecuritySystem.js';
import type { SecurityLevel, SecurityEvent } from '../types.js';

type UserRole = 'admin' | 'operator' | 'viewer';

export class SecurityProxy {
  private realSecuritySystem: SecuritySystem;
  private currentUserRole: UserRole;
  private accessLog: string[] = [];

  constructor(userRole: UserRole = 'viewer') {
    this.realSecuritySystem = new SecuritySystem();
    this.currentUserRole = userRole;
    this.log(`Security proxy initialized for user role: ${userRole}`);
  }

  setUserRole(role: UserRole): void {
    this.currentUserRole = role;
    this.log(`User role changed to: ${role}`);
    console.log(`👤 Logged in as: ${role.toUpperCase()}`);
  }

  getUserRole(): UserRole {
    return this.currentUserRole;
  }

  activateCameras(): void {
    if (!this.hasPermission('operator')) {
      this.denyAccess('activate cameras');
      return;
    }
    this.log('Activating cameras');
    this.realSecuritySystem.activateCameras();
  }

  deactivateCameras(): void {
    if (!this.hasPermission('operator')) {
      this.denyAccess('deactivate cameras');
      return;
    }
    this.log('Deactivating cameras');
    this.realSecuritySystem.deactivateCameras();
  }

  activateAlarms(): void {
    if (!this.hasPermission('admin')) {
      this.denyAccess('activate alarms');
      return;
    }
    this.log('Activating alarms (ADMIN action)');
    this.realSecuritySystem.activateAlarms();
  }

  deactivateAlarms(): void {
    if (!this.hasPermission('admin')) {
      this.denyAccess('deactivate alarms');
      return;
    }
    this.log('Deactivating alarms (ADMIN action)');
    this.realSecuritySystem.deactivateAlarms();
  }

  setSecurityLevel(level: SecurityLevel): void {
    if (!this.hasPermission('admin')) {
      this.denyAccess(`set security level to ${level}`);
      return;
    }
    this.log(`Setting security level to ${level} (ADMIN action)`);
    this.realSecuritySystem.setSecurityLevel(level);
  }

  getSecurityLevel(): SecurityLevel {
    this.log('Viewing security level');
    return this.realSecuritySystem.getSecurityLevel();
  }

  getStatus(): string {
    this.log('Viewing security status');
    return this.realSecuritySystem.getStatus();
  }

  getEvents(): SecurityEvent[] {
    this.log('Viewing security events');
    return this.realSecuritySystem.getEvents();
  }

  getRecentEvents(): SecurityEvent[] {
    this.log('Viewing recent security events');
    return this.realSecuritySystem.getRecentEvents();
  }

  clearEvents(): void {
    if (!this.hasPermission('admin')) {
      this.denyAccess('clear security events');
      return;
    }
    this.log('Clearing security events (ADMIN action)');
    this.realSecuritySystem.clearEvents();
  }

  areCamerasActive(): boolean {
    return this.realSecuritySystem.areCamerasActive();
  }

  areAlarmsActive(): boolean {
    return this.realSecuritySystem.areAlarmsActive();
  }

  getAccessLog(): string[] {
    if (!this.hasPermission('admin')) {
      this.denyAccess('view access log');
      return [];
    }
    return [...this.accessLog];
  }

  private hasPermission(requiredRole: UserRole): boolean {
    const roleHierarchy: { [key in UserRole]: number } = {
      viewer: 1,
      operator: 2,
      admin: 3
    };

    return roleHierarchy[this.currentUserRole] >= roleHierarchy[requiredRole];
  }

  private denyAccess(action: string): void {
    const message = `❌ ACCESS DENIED: Cannot ${action} - requires higher privileges (current role: ${this.currentUserRole})`;
    this.log(message);
    console.log(message);
  }

  private log(message: string): void {
    const timestamp = new Date().toLocaleString();
    this.accessLog.push(`[${timestamp}] ${message}`);
  }

  displayAccessLog(): void {
    if (!this.hasPermission('admin')) {
      this.denyAccess('view access log');
      return;
    }

    console.log('\n╔════════════════════════════════════════╗');
    console.log('║        SECURITY ACCESS LOG             ║');
    console.log('╚════════════════════════════════════════╝\n');

    if (this.accessLog.length === 0) {
      console.log('No access log entries.\n');
      return;
    }

    this.accessLog.forEach(entry => {
      console.log(entry);
    });
    console.log('');
  }
}
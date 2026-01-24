import type { ISecuritySystem } from '../../modules/security/index.ts';

export class SecurityProxy implements ISecuritySystem {
  private realSystem: ISecuritySystem;
  private isAuthorized: boolean = false;

  constructor(realSystem: ISecuritySystem) {
    this.realSystem = realSystem;
  }

  login() {
    this.isAuthorized = true;
    console.log("\n Admin logged in \n");
  }

  activateCameras() {
    if (this.isAuthorized) {
      this.realSystem.activateCameras();
    } else {
      console.log("\n Access denied: Login required \n");
    }
  }

  detectThreat() {
    this.realSystem.detectThreat(); 
  }
}
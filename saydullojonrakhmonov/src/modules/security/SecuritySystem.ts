export interface ISecuritySystem {
  activateCameras(): void;
  detectThreat(): void;
}

export class SecuritySystem implements ISecuritySystem {
  activateCameras(): void {
    console.log("Security cameras activated");
  }
  detectThreat(): void {
    console.log("Threat detected! Alert sent");
  }
}

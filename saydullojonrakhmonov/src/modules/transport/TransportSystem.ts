export interface ITransportSystem {
  startTraffic(): void;
  stopTraffic(): void;
  optimizeRoutes(): void;
}

export class TransportSystem implements ITransportSystem {
  startTraffic(): void {
    console.log("Traffic light: Green");
  }
  stopTraffic(): void {
    console.log("Traffic light: Red");
  }
  optimizeRoutes(): void {
    console.log("AI rerouting traffic..");
  }
}

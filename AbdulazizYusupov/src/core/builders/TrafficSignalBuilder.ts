import type { SignalState, TrafficSignalConfig } from '../types.js';

export class TrafficSignal {
  private id: string;
  private location: string;
  private currentState: SignalState;
  private redDuration: number;
  private yellowDuration: number;
  private greenDuration: number;
  private hasPedestrianCrossing: boolean;
  private hasCamera: boolean;
  private isActive: boolean = false;

  constructor(
    id: string,
    location: string,
    redDuration: number,
    yellowDuration: number,
    greenDuration: number,
    hasPedestrianCrossing: boolean,
    hasCamera: boolean
  ) {
    this.id = id;
    this.location = location;
    this.currentState = 'RED';
    this.redDuration = redDuration;
    this.yellowDuration = yellowDuration;
    this.greenDuration = greenDuration;
    this.hasPedestrianCrossing = hasPedestrianCrossing;
    this.hasCamera = hasCamera;
  }

  start(): void {
    this.isActive = true;
    console.log(`🚦 Traffic signal ${this.id} at ${this.location} is now active`);
  }

  stop(): void {
    this.isActive = false;
    this.currentState = 'RED';
    console.log(`🚦 Traffic signal ${this.id} stopped`);
  }

  changeState(newState: SignalState): void {
    if (!this.isActive) {
      console.log(`⚠️  Cannot change state - signal ${this.id} is not active`);
      return;
    }
    this.currentState = newState;
  }

  getState(): SignalState {
    return this.currentState;
  }

  getInfo(): string {
    const features: string[] = [];
    if (this.hasPedestrianCrossing) features.push('Pedestrian Crossing');
    if (this.hasCamera) features.push('Camera Surveillance');

    return `
      Traffic Signal: ${this.id}
      Location: ${this.location}
      Current State: ${this.currentState}
      Status: ${this.isActive ? 'Active' : 'Inactive'}
      Timings: R:${this.redDuration}s, Y:${this.yellowDuration}s, G:${this.greenDuration}s
      Features: ${features.length > 0 ? features.join(', ') : 'None'}
    `.trim();
  }

  getId(): string {
    return this.id;
  }

  isSignalActive(): boolean {
    return this.isActive;
  }
}

export class TrafficSignalBuilder {
  private config: TrafficSignalConfig = {
    redDuration: 30,
    yellowDuration: 5,
    greenDuration: 30,
    hasPedestrianCrossing: false,
    hasCamera: false
  };

  setId(id: string): TrafficSignalBuilder {
    this.config.id = id;
    return this;
  }

  setLocation(location: string): TrafficSignalBuilder {
    this.config.location = location;
    return this;
  }

  setRedDuration(seconds: number): TrafficSignalBuilder {
    this.config.redDuration = seconds;
    return this;
  }

  setYellowDuration(seconds: number): TrafficSignalBuilder {
    this.config.yellowDuration = seconds;
    return this;
  }

  setGreenDuration(seconds: number): TrafficSignalBuilder {
    this.config.greenDuration = seconds;
    return this;
  }

  withPedestrianCrossing(): TrafficSignalBuilder {
    this.config.hasPedestrianCrossing = true;
    return this;
  }

  withCamera(): TrafficSignalBuilder {
    this.config.hasCamera = true;
    return this;
  }

  setTimings(red: number, yellow: number, green: number): TrafficSignalBuilder {
    this.config.redDuration = red;
    this.config.yellowDuration = yellow;
    this.config.greenDuration = green;
    return this;
  }

  build(): TrafficSignal {
    if (!this.config.id) {
      throw new Error('Traffic signal ID is required');
    }
    if (!this.config.location) {
      throw new Error('Traffic signal location is required');
    }

    return new TrafficSignal(
      this.config.id,
      this.config.location,
      this.config.redDuration || 30,
      this.config.yellowDuration || 5,
      this.config.greenDuration || 30,
      this.config.hasPedestrianCrossing || false,
      this.config.hasCamera || false
    );
  }

  reset(): TrafficSignalBuilder {
    this.config = {
      redDuration: 30,
      yellowDuration: 5,
      greenDuration: 30,
      hasPedestrianCrossing: false,
      hasCamera: false
    };
    return this;
  }
}
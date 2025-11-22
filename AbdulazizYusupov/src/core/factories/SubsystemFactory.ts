import type { Subsystem } from '../types.js';

export interface SubsystemFactory {
  createSubsystem(): Subsystem;
  getSubsystemType(): string;
}

export class LightingSubsystemFactory implements SubsystemFactory {
  createSubsystem(): Subsystem {
    return {
      name: 'Lighting System',
      isActive: false,
      start() {
        this.isActive = true;
        console.log('💡 Lighting system activated');
      },
      stop() {
        this.isActive = false;
        console.log('💡 Lighting system deactivated');
      },
      getStatus() {
        return this.isActive 
          ? 'All street lights operational' 
          : 'Lighting system offline';
      }
    };
  }

  getSubsystemType(): string {
    return 'Lighting';
  }
}

export class SecuritySubsystemFactory implements SubsystemFactory {
  createSubsystem(): Subsystem {
    return {
      name: 'Security System',
      isActive: false,
      start() {
        this.isActive = true;
        console.log('🔒 Security system activated');
      },
      stop() {
        this.isActive = false;
        console.log('🔒 Security system deactivated');
      },
      getStatus() {
        return this.isActive 
          ? 'All security cameras and sensors online' 
          : 'Security system offline';
      }
    };
  }

  getSubsystemType(): string {
    return 'Security';
  }
}

export class TransportSubsystemFactory implements SubsystemFactory {
  createSubsystem(): Subsystem {
    return {
      name: 'Transport System',
      isActive: false,
      start() {
        this.isActive = true;
        console.log('🚦 Transport system activated');
      },
      stop() {
        this.isActive = false;
        console.log('🚦 Transport system deactivated');
      },
      getStatus() {
        return this.isActive 
          ? 'Traffic signals synchronized, flow optimized' 
          : 'Transport system offline';
      }
    };
  }

  getSubsystemType(): string {
    return 'Transport';
  }
}

export class EnergySubsystemFactory implements SubsystemFactory {
  createSubsystem(): Subsystem {
    return {
      name: 'Energy Management System',
      isActive: false,
      start() {
        this.isActive = true;
        console.log('⚡ Energy management system activated');
      },
      stop() {
        this.isActive = false;
        console.log('⚡ Energy management system deactivated');
      },
      getStatus() {
        return this.isActive 
          ? 'Energy consumption optimized, renewable sources active' 
          : 'Energy system offline';
      }
    };
  }

  getSubsystemType(): string {
    return 'Energy';
  }
}

export class SubsystemFactoryProvider {
  static getFactory(type: 'lighting' | 'security' | 'transport' | 'energy'): SubsystemFactory {
    switch (type) {
      case 'lighting':
        return new LightingSubsystemFactory();
      case 'security':
        return new SecuritySubsystemFactory();
      case 'transport':
        return new TransportSubsystemFactory();
      case 'energy':
        return new EnergySubsystemFactory();
      default:
        throw new Error(`Unknown subsystem type: ${type}`);
    }
  }

  static createSubsystem(type: 'lighting' | 'security' | 'transport' | 'energy'): Subsystem {
    const factory = this.getFactory(type);
    return factory.createSubsystem();
  }
}

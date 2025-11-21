import { SmartCityController } from './core/controller.ts';
import { CityMonitor } from './core/singleton/index.ts';

console.log("\n Running SmartCity Tests...\n");

const ctrl1 = SmartCityController.getInstance();
const ctrl2 = SmartCityController.getInstance();
console.log("Facade Singleton test:", ctrl1 === ctrl2 ? "PASSED" : "FAILED");

const mon1 = CityMonitor.getInstance();
const mon2 = CityMonitor.getInstance();
console.log("Monitor Singleton test:", mon1 === mon2 ? "PASSED" : "FAILED");

console.log("\nAll tests completed! \n");




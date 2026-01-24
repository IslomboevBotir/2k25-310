import  {SmartCityController}  from './core/controller.ts';

console.log(" \n SmartCity System Starting...\n");

const controller = SmartCityController.getInstance();

// Simulate day cycle
controller.startDay();
setTimeout(() => controller.startNight(), 2000);
setTimeout(() => controller.emergency(), 4000);
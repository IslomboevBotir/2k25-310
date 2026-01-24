import { TrafficSignalBuilder } from '../src/core/builders/TrafficSignalBuilder';

describe('Builder Pattern - TrafficSignalBuilder', () => {
  test('should build traffic signal step by step', () => {
    const signal = new TrafficSignalBuilder()
      .setId('TS-001')
      .setLocation('Main Street')
      .setTimings(45, 5, 40)
      .withPedestrianCrossing()
      .withCamera()
      .build();

    expect(signal.getId()).toBe('TS-001');
  });

  test('should require mandatory fields', () => {
    expect(() => {
      new TrafficSignalBuilder().build();
    }).toThrow();
  });

  test('should support method chaining', () => {
    const builder = new TrafficSignalBuilder();
    const result = builder.setId('TS-002');
    expect(result).toBe(builder);
  });
});

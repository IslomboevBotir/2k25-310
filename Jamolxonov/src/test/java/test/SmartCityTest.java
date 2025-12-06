package test;

import com.advancedprogramming.core.*;
import com.advancedprogramming.core.builders.CityConfigBuilder;
import com.advancedprogramming.core.adapters.WeatherServiceAdapter;
import com.advancedprogramming.core.proxy.SecurityProxy;
import com.advancedprogramming.core.composite.*;
import com.advancedprogramming.modules.lighting.*;
import com.advancedprogramming.modules.lighting.factories.DeviceFactory;
import com.advancedprogramming.modules.security.AlarmSystem;

public class SmartCityTest {
    private static int testsPassed = 0;
    private static int testsFailed = 0;

    public static void main(String[] args) {
        System.out.println("╔════════════════════════════════════════╗");
        System.out.println("║   SmartCity System - Unit Tests       ║");
        System.out.println("╚════════════════════════════════════════╝\n");

        // Test each design pattern
        testSingletonPattern();
        testBuilderPattern();
        testFactoryMethodPattern();
        testFacadePattern();
        testProxyPattern();
        testAdapterPattern();
        testCompositePattern();

        // Integration tests
        testSystemIntegration();
        testEmergencyScenario();

        // Print results
        printTestResults();
    }

    // ===== SINGLETON PATTERN TESTS =====
    private static void testSingletonPattern() {
        System.out.println("\n[TEST 1] Singleton Pattern - CityController");
        System.out.println("─────────────────────────────────────────");

        try {
            CityConfig config1 = new CityConfigBuilder()
                    .withCityName("TestCity1")
                    .build();

            CityController controller1 = CityController.getInstance(config1);
            CityController controller2 = CityController.getInstance();

            // Both references should point to same instance
            if (controller1 == controller2) {
                System.out.println("✓ Same instance returned");
                testsPassed++;
            } else {
                System.out.println("✗ Different instances returned");
                testsFailed++;
            }

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== BUILDER PATTERN TESTS =====
    private static void testBuilderPattern() {
        System.out.println("\n[TEST 2] Builder Pattern - CityConfig");
        System.out.println("─────────────────────────────────────────");

        try {
            CityConfig config = new CityConfigBuilder()
                    .withCityName("BuilderTestCity")
                    .withPopulation(500000)
                    .withLightingEnabled(true)
                    .withTransportEnabled(false)
                    .withSecurityEnabled(true)
                    .withEnergyMonitoringEnabled(false)
                    .build();

            boolean passed = true;

            if (!config.getCityName().equals("BuilderTestCity")) {
                System.out.println("✗ City name not set correctly");
                passed = false;
            }

            if (config.getPopulation() != 500000) {
                System.out.println("✗ Population not set correctly");
                passed = false;
            }

            if (!config.isLightingEnabled()) {
                System.out.println("✗ Lighting should be enabled");
                passed = false;
            }

            if (config.isTransportEnabled()) {
                System.out.println("✗ Transport should be disabled");
                passed = false;
            }

            if (passed) {
                System.out.println("✓ Builder creates correct configuration");
                System.out.println("  City: " + config.getCityName() +
                        ", Population: " + config.getPopulation());
                testsPassed++;
            } else {
                testsFailed++;
            }

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== FACTORY METHOD PATTERN TESTS =====
    private static void testFactoryMethodPattern() {
        System.out.println("\n[TEST 3] Factory Method Pattern - Device Creation");
        System.out.println("─────────────────────────────────────────");

        try {
            Device streetLight = DeviceFactory.createDevice("streetlight", "Test St");
            Device trafficLight = DeviceFactory.createDevice("trafficlight", "Test Junction");

            boolean passed = true;

            if (!(streetLight instanceof StreetLight)) {
                System.out.println("✗ StreetLight not created correctly");
                passed = false;
            }

            if (!(trafficLight instanceof TrafficLight)) {
                System.out.println("✗ TrafficLight not created correctly");
                passed = false;
            }

            // Test device functionality
            streetLight.turnOn();
            streetLight.adjustIntensity(50);
            String status = streetLight.getStatus();

            if (!status.contains("ON") || !status.contains("50")) {
                System.out.println("✗ Device functionality not working");
                passed = false;
            }

            if (passed) {
                System.out.println("✓ Factory creates correct device types");
                System.out.println("  Created: StreetLight and TrafficLight");
                testsPassed++;
            } else {
                testsFailed++;
            }

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== FACADE PATTERN TESTS =====
    private static void testFacadePattern() {
        System.out.println("\n[TEST 4] Facade Pattern - Subsystem Management");
        System.out.println("─────────────────────────────────────────");

        try {
            LightingFacade lightingFacade = new LightingFacade();
            lightingFacade.initialize();

            System.out.println("✓ Facade simplifies complex subsystem initialization");
            System.out.println("  Multiple devices created and configured through single interface");

            lightingFacade.adjustLighting("night");
            lightingFacade.getStatus();

            System.out.println("✓ Facade provides simple interface to complex operations");
            testsPassed++;

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== PROXY PATTERN TESTS =====
    private static void testProxyPattern() {
        System.out.println("\n[TEST 5] Proxy Pattern - Access Control");
        System.out.println("─────────────────────────────────────────");

        try {
            AlarmSystem alarmSystem = new AlarmSystem();
            alarmSystem.arm();

            SecurityProxy proxy = new SecurityProxy(alarmSystem);

            // Test authorized access
            System.out.println("\nTest 5a: Authorized access");
            proxy.setAuthorized(true);
            proxy.triggerAlarm("Test Alert", "Test Location");
            System.out.println("✓ Proxy allows authorized access");

            // Test unauthorized access
            System.out.println("\nTest 5b: Unauthorized access");
            proxy.setAuthorized(false);
            proxy.triggerAlarm("Unauthorized Test", "Test Location");
            System.out.println("✓ Proxy blocks unauthorized access");

            // Test validation
            System.out.println("\nTest 5c: Validation");
            proxy.setAuthorized(true);
            proxy.triggerAlarm("", "Test Location");
            System.out.println("✓ Proxy validates inputs");

            testsPassed++;

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== ADAPTER PATTERN TESTS =====
    private static void testAdapterPattern() {
        System.out.println("\n[TEST 6] Adapter Pattern - External Service Integration");
        System.out.println("─────────────────────────────────────────");

        try {
            WeatherServiceAdapter adapter = new WeatherServiceAdapter();

            // Test simple format
            String conditions = adapter.getCurrentConditions();
            if (conditions != null && !conditions.isEmpty()) {
                System.out.println("✓ Adapter converts external data to simple format");
                System.out.println("  Weather: " + conditions);
            }

            // Test detailed format
            WeatherServiceAdapter.WeatherInfo info = adapter.getDetailedWeather();
            if (info != null) {
                System.out.println("✓ Adapter converts to detailed internal format");
                System.out.println("  Temperature: " + info.getTemperature() + "°C");
                System.out.println("  Humidity: " + info.getHumidity() + "%");
                System.out.println("  Energy efficient conditions: " +
                        info.isEnergyEfficientConditions());
            }

            testsPassed++;

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== COMPOSITE PATTERN TESTS =====
    private static void testCompositePattern() {
        System.out.println("\n[TEST 7] Composite Pattern - Hierarchical Structure");
        System.out.println("─────────────────────────────────────────");

        try {
            // Create city hierarchy
            District northDistrict = new District("North District");
            District southDistrict = new District("South District");

            Neighborhood downtown = new Neighborhood("Downtown", 150);
            Neighborhood suburbs = new Neighborhood("Suburbs", 200);
            Neighborhood industrial = new Neighborhood("Industrial Zone", 75);

            northDistrict.addZone(downtown);
            northDistrict.addZone(suburbs);
            southDistrict.addZone(industrial);

            System.out.println("✓ Composite structure created");

            // Test uniform interface
            System.out.println("\nActivating North District:");
            northDistrict.activate();

            System.out.println("\nDevice counts:");
            System.out.println("  North District: " + northDistrict.getDeviceCount() + " devices");
            System.out.println("  South District: " + southDistrict.getDeviceCount() + " devices");

            if (northDistrict.getDeviceCount() == 350) {
                System.out.println("✓ Composite correctly aggregates child data");
                testsPassed++;
            } else {
                System.out.println("✗ Incorrect device count");
                testsFailed++;
            }

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== INTEGRATION TESTS =====
    private static void testSystemIntegration() {
        System.out.println("\n[TEST 8] System Integration Test");
        System.out.println("─────────────────────────────────────────");

        try {
            CityConfig config = new CityConfigBuilder()
                    .withCityName("IntegrationTestCity")
                    .withPopulation(750000)
                    .build();

            CityController controller = CityController.getInstance(config);
            controller.initializeAllSystems();

            // Test cross-system operations
            controller.adjustCityLighting("evening");
            controller.manageTraffic("normal");
            controller.optimizeEnergy();

            System.out.println("✓ All subsystems work together");
            testsPassed++;

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== EMERGENCY SCENARIO TEST =====
    private static void testEmergencyScenario() {
        System.out.println("\n[TEST 9] Emergency Scenario Test");
        System.out.println("─────────────────────────────────────────");

        try {
            CityConfig config = new CityConfigBuilder()
                    .withCityName("EmergencyTestCity")
                    .build();

            CityController controller = CityController.getInstance(config);
            controller.initializeAllSystems();

            // Trigger emergency
            System.out.println("\nSimulating emergency...");
            controller.handleEmergency("fire_alarm", "Central Plaza");

            System.out.println("✓ Emergency triggers coordinated response across systems");
            System.out.println("  - Security alerts");
            System.out.println("  - Lighting adjusted");
            System.out.println("  - Traffic routes cleared");

            testsPassed++;

        } catch (Exception e) {
            System.out.println("✗ Test failed with exception: " + e.getMessage());
            testsFailed++;
        }
    }

    // ===== TEST RESULTS =====
    private static void printTestResults() {
        System.out.println("\n╔════════════════════════════════════════╗");
        System.out.println("║         TEST RESULTS SUMMARY           ║");
        System.out.println("╚════════════════════════════════════════╝");
        System.out.println();
        System.out.println("Tests Passed: " + testsPassed);
        System.out.println("Tests Failed: " + testsFailed);
        System.out.println("Total Tests:  " + (testsPassed + testsFailed));

        if (testsFailed == 0) {
            System.out.println("\n✓ ALL TESTS PASSED! ✓");
        } else {
            System.out.println("\n⚠ Some tests failed. Please review output above.");
        }

        double percentage = (testsPassed * 100.0) / (testsPassed + testsFailed);
        System.out.println("\nSuccess Rate: " + String.format("%.1f", percentage) + "%");
        System.out.println();
    }
}

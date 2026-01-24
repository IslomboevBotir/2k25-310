<?php
require_once __DIR__ . '/core/autoload.php';

use core\factories\DefaultSubsystemFactory;
use core\SmartCityController;

class TestRunner {
    private int $passed = 0;
    private int $failed = 0;

    public function assertTrue(bool $cond, string $msg): void {
        if ($cond) { $this->passed++; echo "[PASS] $msg\n"; }
        else { $this->failed++; echo "[FAIL] $msg\n"; }
    }

    public function assertContains(string $needle, string $haystack, string $msg): void {
        $this->assertTrue(strpos($haystack, $needle) !== false, $msg);
    }

    public function summary(): void {
        echo "-----------------\n";
        echo "Passed: {$this->passed}\n";
        echo "Failed: {$this->failed}\n";
        echo "-----------------\n";
        if ($this->failed > 0) exit(1);
    }
}

$T = new TestRunner();

$factory = new DefaultSubsystemFactory();
$controller = SmartCityController::getInstance($factory);
$controller->addLightingZoneDemo();

/**
 * Tests cover:
 * - Singleton
 * - Proxy access control
 * - Composite lighting operations
 * - Adapter weather usage inside energy optimization
 * - Builder report generation
 */

// Singleton test
$controller2 = SmartCityController::getInstance($factory);
$T->assertTrue($controller === $controller2, "Singleton: same instance returned");

// Proxy test: user cannot arm/disarm
$controller->loginAs('user');
$res = $controller->armSecurity();
$T->assertContains("ACCESS DENIED", $res, "Proxy: user cannot arm security");

// Admin can arm
$controller->loginAs('admin');
$res = $controller->armSecurity();
$T->assertContains("Security ARMED", $res, "Proxy: admin can arm security");

// Composite: toggle zone ON affects status
$controller->toggleZone("Center", true);
$lightingStatus = $controller->lightingStatus();
$T->assertContains("Zone: Center", $lightingStatus, "Composite: zone exists in status");
$T->assertContains("ON? yes", $lightingStatus, "Composite: Center ON after toggle");

// Decorator: cap brightness to <=70
$controller->enableEnergySavingMode(true);
$controller->setLightBrightness("Center", "MainStreet-1", 99);
$lightingStatus2 = $controller->lightingStatus();
$T->assertTrue(strpos($lightingStatus2, "brightness=70") !== false || strpos($lightingStatus2, "brightness=69") !== false || strpos($lightingStatus2, "brightness=0") !== false, "Decorator: brightness capped (max 70)");

// Adapter + Energy optimize returns weather line
$energyRes = $controller->optimizeEnergy();
$T->assertContains("Weather input:", $energyRes, "Adapter used in energy optimization");

// Builder report includes sections
$report = $controller->buildCityReport();
$T->assertContains("SmartCity Report", $report, "Builder: report header exists");
$T->assertContains("--- Subsystems ---", $report, "Builder: subsystems section exists");

$T->summary();

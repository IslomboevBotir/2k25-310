<?php
namespace core\decorators;

use modules\lighting\LightingSystem;

/**
 * PATTERN: Decorator
 * Purpose: Add energy-saving behavior to LightingSystem without modifying its code.
 */
class EnergySavingLightingDecorator extends LightingSystem
{
    private LightingSystem $inner;

    public function __construct(LightingSystem $inner)
    {
        $this->inner = $inner;
    }

    public function toggleZone(string $zoneName, bool $on): string
    {
        $msg = $this->inner->toggleZone($zoneName, $on);

        // When turned ON, auto-limit brightness to 60 for energy saving
        if ($on) {
            $this->inner->limitBrightnessInZone($zoneName, 60);
            $msg .= "Decorator: brightness limited to 60 in zone '{$zoneName}'.\n";
        }
        return $msg;
    }

    public function setLightBrightness(string $zoneName, string $lightName, int $value): string
    {
        // Energy saving: cap max brightness
        $value = min($value, 70);
        return $this->inner->setLightBrightness($zoneName, $lightName, $value) .
            "Decorator: cap applied (max 70).\n";
    }

    public function status(): string
    {
        return $this->inner->status() . " (Decorated: EnergySaving)\n";
    }

    public function seedDemo(): void { $this->inner->seedDemo(); }
    public function limitBrightnessInZone(string $zoneName, int $cap): void { $this->inner->limitBrightnessInZone($zoneName, $cap); }
}

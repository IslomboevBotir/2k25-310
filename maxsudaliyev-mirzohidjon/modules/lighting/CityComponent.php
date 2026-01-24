<?php
namespace modules\lighting;

/**
 * PATTERN: Composite (Component)
 * Purpose: Treat single light and group(zone) uniformly.
 */
interface CityComponent
{
    public function getName(): string;
    public function turnOn(): void;
    public function turnOff(): void;
    public function isOn(): bool;

    public function setBrightness(int $value): void;
    public function getBrightness(): int;

    public function status(int $indent = 0): string;
}

<?php
namespace modules\lighting;

/**
 * PATTERN: Composite (Leaf)
 * Purpose: StreetLight is a single element in a zone.
 */
class StreetLight implements CityComponent
{
    private string $name;
    private bool $on = false;
    private int $brightness = 50; // 0..100

    public function __construct(string $name) { $this->name = $name; }

    public function getName(): string { return $this->name; }

    public function turnOn(): void { $this->on = true; }
    public function turnOff(): void { $this->on = false; }
    public function isOn(): bool { return $this->on; }

    public function setBrightness(int $value): void
    {
        $value = max(0, min(100, $value));
        $this->brightness = $value;
    }

    public function getBrightness(): int { return $this->brightness; }

    public function status(int $indent = 0): string
    {
        $pad = str_repeat(' ', $indent);
        return $pad . "- Light: {$this->name}, " . ($this->on ? "ON" : "OFF") . ", brightness={$this->brightness}\n";
    }
}

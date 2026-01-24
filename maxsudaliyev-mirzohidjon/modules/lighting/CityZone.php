<?php
namespace modules\lighting;

/**
 * PATTERN: Composite (Composite)
 * Purpose: Zone contains multiple lights (children).
 */
class CityZone implements CityComponent
{
    private string $name;
    /** @var CityComponent[] */
    private array $children = [];

    public function __construct(string $name) { $this->name = $name; }

    public function add(CityComponent $component): void
    {
        $this->children[] = $component;
    }

    public function getName(): string { return $this->name; }

    public function turnOn(): void { foreach ($this->children as $c) $c->turnOn(); }
    public function turnOff(): void { foreach ($this->children as $c) $c->turnOff(); }

    public function isOn(): bool
    {
        foreach ($this->children as $c) {
            if ($c->isOn()) return true;
        }
        return false;
    }

    public function setBrightness(int $value): void
    {
        foreach ($this->children as $c) $c->setBrightness($value);
    }

    public function getBrightness(): int
    {
        // Average brightness of children
        if (count($this->children) === 0) return 0;
        $sum = 0;
        foreach ($this->children as $c) $sum += $c->getBrightness();
        return (int)round($sum / count($this->children));
    }

    public function findChild(string $name): ?CityComponent
    {
        foreach ($this->children as $c) {
            if (strcasecmp($c->getName(), $name) === 0) return $c;
        }
        return null;
    }

    public function status(int $indent = 0): string
    {
        $pad = str_repeat(' ', $indent);
        $s = $pad . "Zone: {$this->name} (ON? " . ($this->isOn() ? "yes" : "no") . ")\n";
        foreach ($this->children as $c) {
            $s .= $c->status($indent + 2);
        }
        return $s;
    }
}

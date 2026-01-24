<?php
namespace modules\lighting;

/**
 * Lighting subsystem.
 *
 * PATTERN: Factory Method (small)
 * Purpose: createLight() can be overridden later (e.g., SmartLamp, LEDLamp) without changing calling code.
 */
class LightingSystem
{
    /** @var CityZone[] */
    protected array $zones = [];

    public function seedDemo(): void
    {
        if (!empty($this->zones)) return;

        $center = new CityZone("Center");
        $center->add($this->createLight("MainStreet-1"));
        $center->add($this->createLight("MainStreet-2"));

        $park = new CityZone("Park");
        $park->add($this->createLight("Park-1"));
        $park->add($this->createLight("Park-2"));
        $park->add($this->createLight("Park-3"));

        $this->zones = [
            "Center" => $center,
            "Park"   => $park
        ];
    }

    protected function createLight(string $name): CityComponent
    {
        return new StreetLight($name);
    }

    public function toggleZone(string $zoneName, bool $on): string
    {
        $zone = $this->getZone($zoneName);
        if (!$zone) return "Zone topilmadi: {$zoneName}\n";

        if ($on) $zone->turnOn(); else $zone->turnOff();
        return "Zone '{$zoneName}' => " . ($on ? "ON" : "OFF") . "\n";
    }

    public function setLightBrightness(string $zoneName, string $lightName, int $value): string
    {
        $zone = $this->getZone($zoneName);
        if (!$zone) return "Zone topilmadi: {$zoneName}\n";

        $light = $zone->findChild($lightName);
        if (!$light) return "Light topilmadi: {$lightName}\n";

        $light->setBrightness($value);
        return "Brightness set: {$zoneName}/{$lightName} => {$light->getBrightness()}\n";
    }

    public function limitBrightnessInZone(string $zoneName, int $cap): void
    {
        $zone = $this->getZone($zoneName);
        if (!$zone) return;
        $zone->setBrightness(min($zone->getBrightness(), $cap));
        // For simplicity, we just set zone brightness to its current avg capped;
        // You can extend to iterate each child if you want more strict enforcement.
    }

    public function status(): string
    {
        if (empty($this->zones)) return "No zones (run seedDemo).\n";
        $s = "";
        foreach ($this->zones as $z) $s .= $z->status(0);
        return $s;
    }

    protected function getZone(string $zoneName): ?CityZone
    {
        foreach ($this->zones as $name => $zone) {
            if (strcasecmp($name, $zoneName) === 0) return $zone;
        }
        return null;
    }
}

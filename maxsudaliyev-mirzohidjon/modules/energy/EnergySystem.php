<?php
namespace modules\energy;

/**
 * Energy subsystem: monitors & optimizes based on weather.
 */
class EnergySystem
{
    private int $consumption = 100; // demo value
    private string $mode = "normal";

    public function optimize(string $weather): string
    {
        // Simple logic: if cold -> heating increases; if sunny -> solar helps
        $msg = "Weather input: {$weather}\n";

        if (stripos($weather, "Sunny") !== false) {
            $this->mode = "eco";
            $this->consumption = max(60, $this->consumption - 20);
            $msg .= "Solar gain detected => ECO mode, consumption reduced.\n";
        } elseif (preg_match('/(-?\d+)/', $weather, $m) && (int)$m[1] < 5) {
            $this->mode = "heating";
            $this->consumption = min(160, $this->consumption + 30);
            $msg .= "Cold detected => HEATING mode, consumption increased.\n";
        } else {
            $this->mode = "normal";
            $msg .= "Normal conditions => NORMAL mode.\n";
        }

        return $msg;
    }

    public function status(): string
    {
        return "mode={$this->mode}, consumption={$this->consumption}\n";
    }
}

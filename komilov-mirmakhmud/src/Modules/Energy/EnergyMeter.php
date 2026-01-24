<?php
class EnergyMeter
{
    private $consumption;

    public function __construct($initial = 1000)
    {
        $this->consumption = $initial;
    }

    public function adjustConsumption($weatherImpact)
    {
        if ($weatherImpact === 'High energy consumption expected.') {
            $this->consumption += 200;
        } else {
            $this->consumption += 50;
        }
    }

    public function status()
    {
        return "Current energy consumption: {$this->consumption} kWh";
    }
}

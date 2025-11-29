<?php
declare(strict_types=1);

namespace SmartCity\Patterns\Builder;

class EmergencyBuilder
{
    private EmergencyPlan $plan;

    public function __construct()
    {
        $this->reset();
    }

    public function reset(): void
    {
        $this->plan = new EmergencyPlan();
    }

    public function addPolice(): self
    {
        $this->plan->units[] = "Police Patrol";
        return $this;
    }

    public function addFireTruck(): self
    {
        $this->plan->units[] = "Fire Engine";
        return $this;
    }

    public function addAmbulance(): self
    {
        $this->plan->units[] = "Medical Unit";
        return $this;
    }

    public function addSwatTeam(): self
    {
        $this->plan->units[] = "SWAT Tactical Team";
        return $this;
    }

    public function setPriority(string $level): self
    {
        $this->plan->priority = $level;
        return $this;
    }

    public function build(): EmergencyPlan
    {
        $result = $this->plan;
        $this->reset();
        return $result;
    }
}
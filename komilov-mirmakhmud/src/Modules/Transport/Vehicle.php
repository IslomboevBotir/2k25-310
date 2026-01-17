<?php
class Vehicle
{
    private $type;
    private $capacity;

    public function __construct($type, $capacity)
    {
        $this->type = $type;
        $this->capacity = $capacity;
    }

    public function status()
    {
        return "Vehicle type: {$this->type}, capacity: {$this->capacity}";
    }
}

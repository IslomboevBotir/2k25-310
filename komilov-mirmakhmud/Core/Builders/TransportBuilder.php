<?php
require_once __DIR__ . '/../../modules/Transport/Vehicle.php';
class TransportBuilder
{
    private $type;
    private $capacity;

    public function setType($type)
    {
        $this->type = $type;
        return $this;
    }

    public function setCapacity($capacity)
    {
        $this->capacity = $capacity;
        return $this;
    }

    public function build()
    {
        return new Vehicle($this->type, $this->capacity);
    }
}

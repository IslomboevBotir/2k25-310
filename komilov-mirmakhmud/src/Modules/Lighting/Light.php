<?php
class Light
{
    private $type;
    private $lumens;

    public function __construct($type, $lumens)
    {
        $this->type = $type;
        $this->lumens = $lumens;
    }

    public function status()
    {
        return "Light type: {$this->type}, brightness: {$this->lumens} lumens";
    }
}

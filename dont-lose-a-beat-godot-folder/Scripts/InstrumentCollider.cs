using Godot;
using System;

public class InstrumentCollider : Area
{
    public Section section;
    public override void _Ready()
    {
        this.section = GetParent<Section>();

        Connect("area_entered", section, "AreaEntered", new Godot.Collections.Array() { "InstrumentCollider" });
        Connect("area_exited", section, "AreaExited", new Godot.Collections.Array() { "InstrumentCollider" });
    }
}

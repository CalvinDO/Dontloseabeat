using Godot;
using System;

public class TempoChangerScriptTransmitter : Spatial
{
    public PlayerSection section;
    public override void _Ready()
    {
        this.section = GetParent<PlayerSection>();

        CollisionConnector left = (CollisionConnector)this.GetChild(0);
        CollisionConnector right = (CollisionConnector)this.GetChild(1);

        left.SetSignals(this.section);
        right.SetSignals(this.section);
    }
}

using Godot;
using System;

public class TempoChangerScriptTransmitter : Spatial
{
    public Section section;
    public override void _Ready()
    {
        this.section = GetParent<Section>();

        CollisionConnector left = (CollisionConnector)this.GetChild(0).GetChild(0);
        CollisionConnector right = (CollisionConnector)this.GetChild(1).GetChild(0);

        left.SetSignals(this.section);
        right.SetSignals(this.section);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

using Godot;
using System.Collections.Generic;

public class SimonsOrchestraManager : Spatial
{

    [Export]
    bool loadDynamically = true;

    [Export]
    public int currentLevel;

    public float currentBPM = 130;

    Dictionary<string, PackedScene> sections;


    private PackedScene thrownChairScene;
    private ThrownChair thrownChair;

    private bool chairThrown = false;

    private float delta;

    //Valentins
    //------
    [Export]
    public float max = 1.1f;
    [Export]
    public float min = 0.9f;
    bool isInThreshold;
    bool checkNow;
    [Export]
    public float timeBeforeStartingChecking = 20;
    [Export]
    public float checkingDuration = 30;
    float thresholdTime;
    ///------

    public override void _Ready()
    {
        LoadPrefabs();
        LoadLevel();
    }
    public void LoadLevel()
    {

        //Get Intruments
        List<string> files = new List<string>();
        Directory dir = new Directory();
        dir.Open($"res://Audio/lvl{currentLevel}");
        dir.ListDirBegin();
        while (true)
        {
            string fileName = dir.GetNext();
            if (fileName == "") break;
            else if (fileName.EndsWith(".ogg") && loadDynamically)
            {
                files.Add(fileName);
                fileName = fileName.Remove(fileName.Length - 4);
                Section cSection = (Section)sections[fileName].Instance();
                AddChild(cSection);
            }
            else if (!fileName.EndsWith(".ogg.import"))
            {
                GD.PrintErr($"Folder res://Audio/lvl{currentLevel} contains wrongly named file {fileName}. SHAME!");
            }
        }
        dir.ListDirEnd();

        foreach (Section cSection in this.GetChildren())
        {
            cSection.Play();
        }

        this.thresholdTime = this.checkingDuration;
    }

    public override void _Process(float delta)
    {
        this.delta = delta;

        this.CheckKeyboardInput();
        this.CheckThreshholdAndPitch();
    }

    public void CheckThreshholdAndPitch()
    {
        if (timeBeforeStartingChecking >= 0f)
            timeBeforeStartingChecking -= this.delta;
        if (timeBeforeStartingChecking <= 0f)
        {
            if (!isInThreshold)
            {
                if (this.IsInThreshold())
                {
                    GD.Print("Start checking duration of threshhold keeping!");
                    isInThreshold = true;
                }
            }
            if (isInThreshold)
            {
                if (this.IsInThreshold())
                {
                    thresholdTime -= this.delta;
                }
                else
                {
                    thresholdTime = this.checkingDuration;
                }
            }

            if (thresholdTime <= 0f)
                GD.Print("-----------WIN-Placeholder-----------");
        }
    }

    public bool IsInThreshold()
    {
        foreach (Section cSection in this.GetChildren())
        {
            if (cSection.currentTempo >= min || cSection.currentPitch >= min && cSection.currentTempo <= max || cSection.currentPitch <= max)
            {
                return true;
            }
        }
        return false;
    }

    void LoadPrefabs()
    {
        sections = new Dictionary<string, PackedScene>();
        sections.Add("strings", ResourceLoader.Load<PackedScene>("res://prefabs/sections/strings.tscn"));
        sections.Add("deepstrings", ResourceLoader.Load<PackedScene>("res://prefabs/sections/deepstrings.tscn"));
        sections.Add("horn", ResourceLoader.Load<PackedScene>("res://prefabs/sections/horn.tscn"));
        sections.Add("trombone", ResourceLoader.Load<PackedScene>("res://prefabs/sections/trombone.tscn"));
        sections.Add("trumpet", ResourceLoader.Load<PackedScene>("res://prefabs/sections/trumpet.tscn"));
        sections.Add("tuba", ResourceLoader.Load<PackedScene>("res://prefabs/sections/tuba.tscn"));
        sections.Add("oboe", ResourceLoader.Load<PackedScene>("res://prefabs/sections/oboe.tscn"));
        sections.Add("clarinet", ResourceLoader.Load<PackedScene>("res://prefabs/sections/clarinet.tscn"));
        sections.Add("percussion", ResourceLoader.Load<PackedScene>("res://prefabs/sections/percussion.tscn"));
        sections.Add("timpani", ResourceLoader.Load<PackedScene>("res://prefabs/sections/timpani.tscn"));
        sections.Add("piano", ResourceLoader.Load<PackedScene>("res://prefabs/sections/piano.tscn"));
        sections.Add("harp", ResourceLoader.Load<PackedScene>("res://prefabs/sections/harp.tscn"));
        sections.Add("flute", ResourceLoader.Load<PackedScene>("res://prefabs/sections/flute.tscn"));

        this.thrownChairScene = ResourceLoader.Load<PackedScene>("res://prefabs/ThrownChair.tscn");
    }

    public void CheckKeyboardInput()
    {

        if (Input.IsActionJustPressed("ThrowChair"))
        {
            GD.Print("Hello");
            this.thrownChair = (ThrownChair)this.thrownChairScene.Instance();
            AddChild(this.thrownChair);
            this.chairThrown = true;

        }
    }
}

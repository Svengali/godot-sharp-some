using Godot;
using System.Linq;

public sealed partial class ExampleList : Godot.Control
{
    public static readonly Color LightBack = Colors.White;
    public static readonly Color DarkBack = Colors.Black;
    public static readonly Color LightStroke = Colors.White;
    public static readonly Color DarkStroke = Colors.Black;
    public static readonly Color LightRegion = Colors.LightGray;
    public static readonly Color DarkRegion = Colors.DarkGray;

    public bool AnimateAll { get; set; }

    public bool InvertAll { get; set; }

    public void _on_AnimateAll_pressed()
    {
        AnimateAll = !AnimateAll;
        var nodes = GetTree().GetNodesInGroup("ExampleContents").OfType<ExampleNodeBase>();
        foreach (var node in nodes)
            node.Animate = AnimateAll;
    }

    public void _on_InvertAll_pressed()
    {
        InvertAll = !InvertAll;
        if (!InvertAll)
        {
            var nodes = GetTree().GetNodesInGroup("ExampleContents").OfType<ExampleNodeBase>();
            foreach (var node in nodes)
            {
                node.Color = LightBack;
                node.LineColor = DarkStroke;
                node.AreaColor = LightRegion;
                node.TextColor = DarkStroke;
            }
        }
        else
        {
            var nodes = GetTree().GetNodesInGroup("ExampleContents").OfType<ExampleNodeBase>();
            foreach (var node in nodes)
            {
                node.Color = DarkBack;
                node.LineColor = LightStroke;
                node.AreaColor = DarkRegion;
                node.TextColor = LightStroke;
            }
        }
    }
}
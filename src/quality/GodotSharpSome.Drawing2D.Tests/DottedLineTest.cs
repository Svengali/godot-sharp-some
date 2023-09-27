namespace GodotSharpSome.Drawing2D.Tests;

public class DottedLineTest
{
    [Fact]
    public void AppendLine_X_ContainsSixPoints()
    {
        var points = new List<Vector2>();

        new DottedLine(4).AppendLine(points, Vector2.Zero, Vector2.Right * 11);

        Assert.Equal(6, points.Count);
        Assert.Equal(0, points.ElementAt(1).X);
        Assert.Equal(5, points.ElementAt(2).X);
        Assert.Equal(5, points.ElementAt(3).X);
        Assert.Equal(10, points.ElementAt(4).X);
    }

    [Fact]
    public void AppendLine_XAdapted_ContainsSixPoints()
    {
        var points = new List<Vector2>();

        new DottedLine(4).AppendLine(points, Vector2.Zero, Vector2.Right * 10);

        Assert.Equal(6, points.Count);
        Assert.Equal(9, points.ElementAt(4).X);
    }

    [Fact]
    public void AppendLine_Y_ContainsSixPoints()
    {
        var points = new List<Vector2>();

        new DottedLine(4).AppendLine(points, Vector2.Zero, Vector2.Down * 11);

        Assert.Equal(6, points.Count);
        Assert.Equal(0, points.ElementAt(0).Y);
        Assert.Equal(11, points.ElementAt(5).Y);
    }
}
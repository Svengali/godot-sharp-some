﻿namespace GodotSharpSome.Drawing2D;

using System.Linq;
using static Godot.Mathf;

public static class CanvasItemExtension
{
    /// <summary>
    /// Draw a single dot.
    /// </summary>
    public static CanvasItem DrawDot(this CanvasItem canvas, Vector2 position, Color color)
    {
        canvas.DrawMultiline(SolidLine.Dot(position), color, 1);

        return canvas;
    }

    /// <summary>
    /// Draw series of dots.
    /// </summary>
    public static CanvasItem DrawDots(this CanvasItem canvas, IList<Vector2> positions, Color color)
    {
        canvas.DrawMultiline(SolidLine.Dots(positions), color, 1);

        return canvas;
    }

    /// <summary>
    /// Draw a circumference of a circle.
    /// </summary>
    public static CanvasItem DrawCircleOutline(this CanvasItem canvas, Vector2 center, float radius, Color lineColor,
        float lineWidth = 1, bool antialiased = false)
    {
        canvas.DrawArc(center, radius, 0, Pi * 2, (int)radius * 16, lineColor, lineWidth, antialiased);

        return canvas;
    }

    /// <summary>
    /// Draw a plane region of a circle or disc.
    /// </summary>
    public static CanvasItem DrawCircleRegion(this CanvasItem canvas, Vector2 center, float radius, Color regionColor)
    {
        canvas.DrawCircle(center, radius, regionColor);

        return canvas;
    }

    /// <summary>
    /// Draw a circumference and plane region of a circle.
    /// </summary>
    public static CanvasItem DrawCircle(this CanvasItem canvas, Vector2 center, float radius, Color lineColor, Color regionColor,
        float lineWidth = 1, bool antialiased = false)
    {
        canvas.DrawCircleRegion(center, radius, regionColor);
        canvas.DrawCircleOutline(center, radius, lineColor, lineWidth, antialiased);

        return canvas;
    }

    /// <summary>
    /// Draw a circumference of an ellipse.
    /// </summary>
    public static CanvasItem DrawEllipseOutline(this CanvasItem canvas, Vector2 center, float radiusA, float radiusB, float angle, Color lineColor,
        float lineWidth = 1, bool antialiased = false)
    {
        var originTransform = canvas.GetCanvasTransform();

        Transform2D t = Transform2D.Identity;
        t.Origin = center;
        t.X.X = t.Y.Y = Cos(angle);
        t.X.Y = t.Y.X = Sin(angle);
        t.Y.X *= -1;
        t.Y *= radiusB / radiusA;

        canvas.DrawSetTransformMatrix(t);
        canvas.DrawCircleOutline(Vector2.Zero, radiusA, lineColor, lineWidth, antialiased);
        canvas.DrawSetTransformMatrix(originTransform);

        return canvas;
    }

    /// <summary>
    /// Draw a plane region of an ellipse.
    /// </summary>
    public static CanvasItem DrawEllipseRegion(this CanvasItem canvas, Vector2 center, float radiusA, float radiusB, float angle, Color regionColor)
    {
        var originTransform = canvas.GetCanvasTransform();

        Transform2D t = Transform2D.Identity;
        t.Origin = center;
        t.X.X = t.Y.Y = Cos(angle);
        t.X.Y = t.Y.X = Sin(angle);
        t.Y.X *= -1;
        t.Y *= radiusB / radiusA;

        canvas.DrawSetTransformMatrix(t);
        canvas.DrawCircleRegion(Vector2.Zero, radiusA, regionColor);
        canvas.DrawSetTransformMatrix(originTransform);

        return canvas;
    }

    /// <summary>
    /// Draw a circumference and plane region of an ellipse.
    /// </summary>
    public static CanvasItem DrawEllipse(this CanvasItem canvas, Vector2 center, float radiusA, float radiusB, float angle, Color lineColor, Color regionColor,
        float lineWidth = 1, bool antialiased = false)
    {
        canvas.DrawEllipseRegion(center, radiusA, radiusB, angle, regionColor);
        canvas.DrawEllipseOutline(center, radiusA, radiusB, angle, lineColor, lineWidth, antialiased);

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter of a triangle.
    /// </summary>
    public static CanvasItem DrawTriangleOutline(this CanvasItem canvas, Vector2 a, Vector2 b, Vector2 c, Color lineColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        lineType ??= new SolidLine();

        canvas.DrawMultiline(
            new Multiline(3 * 2, lineType).AppendTriangle(a, b, c).Points(),
            lineColor, lineWidth);

        return canvas;
    }

    /// <summary>
    /// Draw a plane region of a triangle.
    /// </summary>
    public static CanvasItem DrawTriangleRegion(this CanvasItem canvas, Vector2 a, Vector2 b, Vector2 c, Color regionColor)
    {
        canvas.DrawPolygon(
            new Multiline(3 * 2).AppendTriangle(a, b, c).Points(),
            new Color[] { regionColor });

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter and plane region of a triangle.
    /// </summary>
    public static CanvasItem DrawTriangle(this CanvasItem canvas, Vector2 a, Vector2 b, Vector2 c, Color lineColor, Color regionColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        canvas.DrawTriangleRegion(a, b, c, regionColor);
        canvas.DrawTriangleOutline(a, b, c, lineColor, lineWidth, lineType);

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter of a rectangle.
    /// </summary>
    public static CanvasItem DrawRectangleOutline(this CanvasItem canvas, Vector2 center, float length, float width, float rotationAngle, Color lineColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        lineType ??= new SolidLine();

        canvas.DrawMultiline(
            new Multiline(4 * 2, lineType).AppendRectangle(center, length / 2, width / 2, rotationAngle).Points(),
            lineColor, lineWidth);

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter of a rectangle.
    /// </summary>
    public static CanvasItem DrawRectangleOutline(this CanvasItem canvas, Vector2 vertex1, Vector2 vertex2, float height, Color lineColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        lineType ??= new SolidLine();

        canvas.DrawMultiline(
            new Multiline(4 * 2, lineType).AppendRectangle(vertex1, vertex2, height).Points(),
            lineColor, lineWidth);

        return canvas;
    }

    /// <summary>
    /// Draw a plane region of a rectangle.
    /// </summary>
    public static CanvasItem DrawRectangleRegion(this CanvasItem canvas, Vector2 center, float length, float width, float rotationAngle, Color regionColor)
    {
        canvas.DrawPolygon(
            new Multiline(4 * 2).AppendRectangle(center, length / 2, width / 2, rotationAngle).Points(),
            new Color[] { regionColor });

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter of a rectangle.
    /// </summary>
    public static CanvasItem DrawRectangleRegion(this CanvasItem canvas, Vector2 vertex1, Vector2 vertex2, float height, Color regionColor)
    {
        canvas.DrawPolygon(
            new Multiline(4 * 2)
            .AppendRectangle(vertex1, vertex2, height)
            .Points(),
            new Color[] { regionColor });

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter and plane region of a rectangle.
    /// </summary>
    public static CanvasItem DrawRectangle(this CanvasItem canvas, Vector2 center, float length, float width, float rotationAngle, Color lineColor, Color regionColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        canvas.DrawRectangleRegion(center, length, width, rotationAngle, regionColor);
        canvas.DrawRectangleOutline(center, length, width, rotationAngle, lineColor, lineWidth, lineType);

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter and plane region of a rectangle.
    /// </summary>
    public static CanvasItem DrawRectangle(this CanvasItem canvas, Vector2 vertex1, Vector2 vertex2, float height, Color lineColor, Color regionColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        canvas.DrawRectangleRegion(vertex1, vertex2, height, regionColor);
        canvas.DrawRectangleOutline(vertex1, vertex2, height, lineColor, lineWidth, lineType);

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter of a regular convex polygon.
    /// </summary>
    public static CanvasItem DrawRegularConvexPolygonOutline(this CanvasItem canvas, Vector2 center, float radius, int verticesCount, float rotationAngle, Color lineColor,
        float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        lineType ??= new SolidLine();

        canvas.DrawMultiline(
            new Multiline((verticesCount + 1) * 2, lineType)
            .AppendRegularConvexPolygon(center, radius, verticesCount, rotationAngle)
            .Points(),
            lineColor, lineWidth);

        return canvas;
    }

    /// <summary>
    /// Draw a plane region of a regular convex polygon.
    /// </summary>
    public static CanvasItem DrawRegularConvexPolygonRegion(this CanvasItem canvas, Vector2 center, float radius, int verticesCount, float rotationAngle, Color regionColor)
    {
        canvas.DrawPolygon(
            Multiline.RegularConvexPolygonVertices(center, radius, verticesCount, rotationAngle)
            .ToArray(),
            new Color[] { regionColor });

        return canvas;
    }

    /// <summary>
    /// Draw a perimeter and plane region of a regular convex polygon.
    /// </summary>
    public static CanvasItem DrawRegularConvexPolygon(this CanvasItem canvas, Vector2 center, float radius, int verticesCount, float rotationAngle, Color lineColor, Color regionColor,
            float lineWidth = 1, IStraightLineAppender? lineType = null)
    {
        canvas.DrawRegularConvexPolygonRegion(center, radius, verticesCount, rotationAngle, regionColor);
        canvas.DrawRegularConvexPolygonOutline(center, radius, verticesCount, rotationAngle, lineColor, lineWidth, lineType);

        return canvas;
    }

    /// <summary>
    /// Draw a string in a position of left bottom point and with rotation angle.
    /// </summary>
    public static CanvasItem DrawString(this CanvasItem canvas, Font font, Vector2 position, string text, float angle, Color? color = null)
    {
        var originTransform = canvas.GetCanvasTransform();

        Transform2D t = Transform2D.Identity;
        t.Origin = position;
        t.X.X = t.Y.Y = Cos(angle);
        t.X.Y = t.Y.X = Sin(angle);
        t.Y.X *= -1;

        canvas.DrawSetTransformMatrix(t);
        if (color.HasValue)
            canvas.DrawString(font, Vector2.Zero, text, HorizontalAlignment.Left, -1, 16, color);
        else
            canvas.DrawString(font, Vector2.Zero, text);
        canvas.DrawSetTransformMatrix(originTransform);

        return canvas;
    }

    /// <summary>
    /// Draw a string in a position of text center and with rotation angle.
    /// </summary>
    public static CanvasItem DrawCenteredString(this CanvasItem canvas, Font font, Vector2 position, string text, float angle, Color? color = null)
    {
        var originTransform = canvas.GetCanvasTransform();
        var textSize = font.GetStringSize(text);

        Transform2D t = Transform2D.Identity;
        t.Origin = position;
        t.X.X = t.Y.Y = Cos(angle);
        t.X.Y = t.Y.X = Sin(angle);
        t.Y.X *= -1;

        canvas.DrawSetTransformMatrix(t);
        if (color.HasValue)
            canvas.DrawString(font, new Vector2(-textSize.X / 2f, 0), text, HorizontalAlignment.Left, -1, 16, color);
        else
            canvas.DrawString(font, new Vector2(-textSize.X / 2f, 0), text);
        canvas.DrawSetTransformMatrix(originTransform);

        return canvas;
    }
}
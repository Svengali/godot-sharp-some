﻿using System.Collections.Generic;
using System.Linq;
using Godot;
using GodotSharpSome.Drawing2D;

public class Vectors : ExampleNodeBase
{
    private const float DistanceInterpolationDelta = 2;

    private readonly List<Vector2> _vectors;
    private readonly List<Vector2> _targetVectors;

    private Vector2 _origin1 = new Vector2(100, 200);

    private Vector2 _origin2 = new Vector2(300, 200);

    private Vector2 _origin3 = new Vector2(500, 200);

    public Vectors()
    {
        _vectors = new List<Vector2>
        {
            new Vector2(40, 40),
            new Vector2(30, 60),
            new Vector2(60, 20),
            new Vector2(40, -40),
            new Vector2(0, 30),
            new Vector2(-30, 20),
            new Vector2(-20, -50),
        };

        _targetVectors = new List<Vector2>(_vectors.Count / 2);
        for (int i = 0; i < _vectors.Count / 2; i++)
            _targetVectors.Add(_vectors[i]);
    }

    protected override void NextState()
    {
        for (int i = 0; i < _vectors.Count; i++)
        {
            if (i < _vectors.Count / 2) // position interpolations
            {
                if (_vectors[i].DistanceTo(_targetVectors[i]) <= DistanceInterpolationDelta)
                {
                    if (_vectors[i] == _targetVectors[i])
                        _targetVectors[i] = NextVectorBetween(new Vector2(-80, -80), new Vector2(80, 80));
                    else
                        _vectors[i] = _targetVectors[i];
                }
                _vectors[i] += _vectors[i].DirectionTo(_targetVectors[i]) * DistanceInterpolationDelta;
            }
            else // rotation interpolations
            {
                _vectors[i] = _vectors[i].Rotated(NextFloat(0, i % 2 == 0 ? 0.01f : -0.02f));
            }
        }
    }

    public override void _Draw()
    {
        DrawMultiline(
            Multiline.VectorsAbsolutely(_origin1, _vectors),
            LineColor);

        DrawMultiline(
            Multiline.VectorsRelatively(_origin2, _vectors),
            LineColor);

        var sumVector = _vectors.Aggregate((a, b) => a + b);

        DrawMultiline(
            Multiline.Arrow(_origin2, _origin2 + sumVector),
            LineColor2);

        DrawMultiline(
          Multiline.VectorsRelatively(_origin3, _vectors.OrderBy(x => x.Angle()).ToArray()),
          LineColor);

        DrawMultiline(
            Multiline.Arrow(_origin3, _origin3 + sumVector),
            LineColor2);
    }

    public void _on_Animate_pressed() => Animate = !Animate;
}

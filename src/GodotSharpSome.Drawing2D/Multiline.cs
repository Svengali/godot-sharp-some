﻿namespace GodotSharpSome.Drawing2D
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Godot;
    using static Godot.Mathf;

    public class Multiline
    {
        private const float DefaultArrowHeadAngle = Pi / 14;
        private const float DefaultArrowHeadRadius = 20;

        private List<Vector2> _points;

        public Multiline(int capacity)
        {
            _points = new List<Vector2>(capacity);
        }

        public Multiline(List<Vector2> points = null)
        {
            _points = points ?? new List<Vector2>();
        }

        public Vector2[] Points => _points.ToArray();

        public Multiline AppendDot(Vector2 position)
        {
            AppendDot(_points, position);
            return this;
        }

        public Multiline AppendDots(IEnumerable<Vector2> positions)
        {
            AppendDots(_points, positions);
            return this;
        }

        public Multiline AppendLine(Vector2 start, Vector2 end)
        {
            AppendLine(_points, start, end);
            return this;
        }

        public Multiline AppendCross(Vector2 center, float radius)
        {
            AppendCross(_points, center, radius);
            return this;
        }

        public Multiline AppendCross2(Vector2 center, float outerRadius, float innerRadius)
        {
            AppendCross2(_points, center, outerRadius, innerRadius);
            return this;
        }

        public Multiline AppendArrow(Vector2 start, Vector2 top, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendArrow(_points, start, top, headRadius, arrowAngle);
            return this;
        }

        public Multiline AppendDoubleArrow(Vector2 start, Vector2 top, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendDoubleArrow(_points, start, top, headRadius, arrowAngle);
            return this;
        }

        public Multiline AppendSegmentedLine(Vector2 start, Vector2 direction, IList<float> distances)
        {
            AppendSegmentedLine(_points, start, direction, distances);
            return this;
        }

        public Multiline AppendSegmentedArrow(Vector2 start, Vector2 direction, IList<float> distances, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendSegmentedArrow(_points, start, direction, distances, headRadius, arrowAngle);
            return this;
        }

        public Multiline AppendVectorsRelatively(Vector2 zero, IEnumerable<Vector2> vectors, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendVectorsRelatively(_points, zero, vectors, arrowAngle);
            return this;
        }

        public Multiline AppendVectorsAbsolutely(Vector2 zero, IEnumerable<Vector2> vectors, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendVectorsAbsolutely(_points, zero, vectors, arrowAngle);
            return this;
        }

        public Multiline AppendAxes(Vector2 origin, Vector2 xDirection, float xUnitLength, int xUnitCount, float yUnitLength, int yUnitCount,
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendAxes(_points, origin, xDirection, xUnitLength, xUnitCount, yUnitLength, yUnitCount, headRadius, arrowAngle);
            return this;
        }

        public Multiline AppendTriangle(Vector2 a, Vector2 b, Vector2 c)
        {
            AppendTriangle(_points, a, b, c);
            return this;
        }

        public Multiline AppendRectangle(Vector2 originVertex, Vector2 directionVertex, float height)
        {
            AppendRectangle(_points, originVertex, directionVertex, height);
            return this;
        }

        public Multiline AppendRegularConvexPolygon(Vector2 center, float radius, int verticesCount, float rotationAngle)
        {
            AppendRegularConvexPolygon(_points, center, radius, verticesCount, rotationAngle);
            return this;
        }

        public Multiline AppendCandleBar(Vector2 bottom, float bottomOffset, Vector2 top, float topOffset, float bodyHalfWidth)
        {
            AppendCandleBar(_points, bottom, bottomOffset, top, topOffset, bodyHalfWidth);
            return this;
        }

        public Multiline AppendConnection(Vector2 aCenter, float aRadius, Vector2 bCenter, float bRadius, float? aHeadRadius = default, float? bHeadRadius = default)
        {
            AppendLine(_points, aCenter, aRadius, bCenter, bRadius);
            if (aHeadRadius is not null)
                AppendArrowHead(_points, bCenter.DirectionTo(aCenter), aCenter, aHeadRadius.Value);
            if (bHeadRadius is not null)
                AppendArrowHead(_points, aCenter.DirectionTo(bCenter), bCenter, bHeadRadius.Value);

            return this;
        }

        public Multiline Clear()
        {
            _points.Clear();
            return this;
        }

        #region static 

        public static Vector2[] Dot(Vector2 position)
        {
            var points = new List<Vector2>(2);
            AppendDot(points, position);
            return points.ToArray();
        }

        public static Vector2[] Dots(IEnumerable<Vector2> positions)
        {
            var points = new List<Vector2>(2 + positions.Count());
            AppendDots(points, positions);
            return points.ToArray();
        }

        public static Vector2[] Line(Vector2 start, Vector2 end)
        {
            var points = new List<Vector2>(2);
            AppendLine(points, start, end);
            return points.ToArray();
        }

        public static Vector2[] Cross(Vector2 center, float radius)
        {
            var points = new List<Vector2>(2 * 2);
            AppendCross(points, center, radius);
            return points.ToArray();
        }

        public static Vector2[] Cross2(Vector2 center, float outerRadius, float innerRadius)
        {
            var points = new List<Vector2>(2 * 4);
            AppendCross2(points, center, outerRadius, innerRadius);
            return points.ToArray();
        }

        public static Vector2[] Arrow(Vector2 start, Vector2 top, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var points = new List<Vector2>(2 * 3);
            AppendArrow(points, start, top, headRadius, arrowAngle);
            return points.ToArray();
        }

        public static Vector2[] DoubleArrow(Vector2 start, Vector2 top, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var points = new List<Vector2>(2 * 5);
            AppendDoubleArrow(points, start, top, headRadius, arrowAngle);
            return points.ToArray();
        }

        public static Vector2[] SegmentedLine(Vector2 start, Vector2 end, int segmentCount)
        {
            var segmentLength = (end - start).Length() / segmentCount;
            var points = new List<Vector2>(2 + 2 * (segmentCount + 1));
            var distances = Enumerable.Range(0, segmentCount + 1).Select(i => Min(1,i) * segmentLength).ToArray();

            AppendSegmentedLine(points, start, start.DirectionTo(end), distances);
            return points.ToArray();
        }

        public static Vector2[] SegmentedArrow(Vector2 start, Vector2 top, float segmentLength, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var segmentCount = (int)((top - start).Length() / segmentLength);

            return SegmentedArrow(start, start.DirectionTo(top),
                Enumerable.Repeat(segmentLength, segmentCount - 1).ToArray(),
                headRadius, arrowAngle);
        }

        public static Vector2[] SegmentedArrow(Vector2 start, Vector2 direction, IList<float> distances, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var points = new List<Vector2>(6 + 2 * (distances.Count + 1));
            AppendSegmentedArrow(points, start, direction, distances, headRadius, arrowAngle);
            return points.ToArray();
        }

        public static Vector2[] VectorsRelatively(Vector2 zero, IList<Vector2> vectors, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            var points = new List<Vector2>(2 * 3 * vectors.Count);
            AppendVectorsRelatively(points, zero, vectors, arrowAngle);
            return points.ToArray();
        }

        public static Vector2[] VectorsAbsolutely(Vector2 zero, IList<Vector2> vectors, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            var points = new List<Vector2>(2 * 3 * vectors.Count);
            AppendVectorsAbsolutely(points, zero, vectors, arrowAngle);
            return points.ToArray();
        }

        public static Vector2[] Axes(Vector2 origin, Vector2 xDirection, float xUnitLength, int xUnitCount, float yUnitLength, int yUnitCount,
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var points = new List<Vector2>(2 * 3 * 2);
            AppendAxes(points, origin, xDirection, xUnitLength, xUnitCount, yUnitLength, yUnitCount, headRadius, arrowAngle);
            return points.ToArray();
        }

        public static Vector2[] Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            var points = new List<Vector2>(2 * 3);
            AppendTriangle(points, a, b, c);
            return points.ToArray();
        }

        public static Vector2[] Rectangle(Vector2 center, float halfLength, float halfWidth, float rotationAngle)
        {
            var points = new List<Vector2>(2 * 4);
            AppendRectangle(points, center, halfLength, halfWidth, rotationAngle);
            return points.ToArray();
        }

        public static IEnumerable<Vector2> RegularConvexPolygonVertices(Vector2 center, float radius, int verticesCount, float rotationAngle)
        {
            var segmentAngle = 2 * Pi / verticesCount;
            float angle;
            for (int i = 0; i < verticesCount; i++)
            {
                angle = rotationAngle + segmentAngle * i;
                yield return new Vector2(radius * Cos(angle) + center.x, radius * Sin(angle) + center.y);
            }
        }

        public static Vector2[] RegularConvexPolygon(Vector2 center, float radius, int verticesCount, float rotationAngle)
        {
            var points = new List<Vector2>((verticesCount + 1) * 2);
            AppendRegularConvexPolygon(points, center, radius, verticesCount, rotationAngle);
            return points.ToArray();
        }

        public static Vector2[] CandleBar(Vector2 bottom, float bottomOffset, Vector2 top, float topOffset, float bodyHalfWidth)
        {
            var points = new List<Vector2>(6 * 2);
            AppendCandleBar(points, bottom, bottomOffset, top, topOffset, bodyHalfWidth);
            return points.ToArray();
        }

        #endregion

        #region static appending

        public static void AppendDot(IList<Vector2> points, Vector2 position)
        {
            points.Add(position);
            points.Add(position + Vector2.Down);
        }

        public static void AppendDots(IList<Vector2> points, IEnumerable<Vector2> positions)
        {
            foreach (var position in positions)
            {
                points.Add(position);
                points.Add(position + Vector2.Down);
            }
        }

        public static void AppendLine(IList<Vector2> points, Vector2 start, float startOffset, Vector2 end, float endOffset)
        {
            var dirVector = (end - start).Normalized();
            points.Add(start + dirVector * startOffset);
            points.Add(end - dirVector * endOffset);
        }

        public static void AppendLine(IList<Vector2> points, Vector2 start, Vector2 end)
        {
            points.Add(start);
            points.Add(end);
        }

        public static void AppendLine(IList<Vector2> points, float startX, float startY, float endX, float endY)
            => AppendLine(points, new Vector2(startX, startY), new Vector2(endX, endY));

        public static void AppendSeparators(IList<Vector2> points, Vector2 start, Vector2 direction, IList<float> distances)
        {
            var dir = direction.Normalized();
            var normal = new Vector2(dir.y, -dir.x);

            var distSum = 0f;
            foreach (var distance in distances)
            {
                distSum += distance;
                AppendLine(points,
                    (start + dir * distSum) + normal * 2,
                    (start + dir * distSum) - normal * 2);
            }
        }

        public static void AppendCross(IList<Vector2> points, Vector2 center, float radius)
        {
            AppendLine(points, center.x - radius, center.y, center.x + radius, center.y);
            AppendLine(points, center.x, center.y - radius, center.x, center.y + radius);
        }

        public static void AppendCross2(IList<Vector2> points, Vector2 center, float outerRadius, float innerRadius)
        {
            AppendLine(points, center.x - innerRadius, center.y, center.x - outerRadius, center.y);
            AppendLine(points, center.x + innerRadius, center.y, center.x + outerRadius, center.y);
            AppendLine(points, center.x, center.y - innerRadius, center.x, center.y - outerRadius);
            AppendLine(points, center.x, center.y + innerRadius, center.x, center.y + outerRadius);
        }

        public static void AppendArrow(IList<Vector2> points, Vector2 start, Vector2 top, float headRadius, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendLine(points, start, top);
            AppendArrowHead(points, start.DirectionTo(top), top, headRadius, arrowAngle);
        }

        public static void AppendDoubleArrow(IList<Vector2> points, Vector2 start, Vector2 top, float headRadius, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            AppendLine(points, start, top);
            AppendArrowHead(points, start.DirectionTo(top), top, headRadius, arrowAngle);
            AppendArrowHead(points, top.DirectionTo(start), start, headRadius, arrowAngle);
        }

        public static void AppendArrowHead(IList<Vector2> points, Vector2 direction, Vector2 top, float headRadius, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            //side line 1
            AppendLine(points, top, top + direction.Rotated(Pi + arrowAngle) * headRadius);
            //side line 2
            AppendLine(points, top, top + direction.Rotated(Pi - arrowAngle) * headRadius);
        }

        public static void AppendSegmentedLine(IList<Vector2> points, Vector2 start, Vector2 direction, IList<float> distances)
        {
            var dir = direction.Normalized();
            AppendLine(points, start, start + dir * distances.Sum());
            AppendSeparators(points, start, dir, distances);
        }

        public static void AppendSegmentedArrow(IList<Vector2> points, Vector2 start, Vector2 direction, IList<float> distances, 
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var dir = direction.Normalized();
            AppendArrow(points, start, start + dir * (distances.Sum() + 2 * headRadius), headRadius, arrowAngle);
            AppendSeparators(points, start, dir, distances);
        }

        public static void AppendVectorsRelatively(IList<Vector2> points, Vector2 zero, IEnumerable<Vector2> vectors, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            var offset = zero;
            foreach (var vector in vectors)
                AppendArrow(points, offset, offset += vector, Clamp(vector.Length() / 4f, 14, 20), arrowAngle);
        }

        public static void AppendVectorsAbsolutely(IList<Vector2> points, Vector2 zero, IEnumerable<Vector2> vectors, 
            float arrowAngle = DefaultArrowHeadAngle)
        {
            foreach (var vector in vectors)
                AppendArrow(points, zero, zero + vector, Clamp(vector.Length() / 4f, 14, 20), arrowAngle);
        }

        public static void AppendAxes(IList<Vector2> points, Vector2 origin, Vector2 xDirection, float xUnitLength, int xUnitCount, float yUnitLength, int yUnitCount,
            float headRadius = DefaultArrowHeadRadius, float arrowAngle = DefaultArrowHeadAngle)
        {
            var xDistances = Enumerable.Range(0, xUnitCount).Select(i => xUnitLength).ToArray();
            var yDistances = Enumerable.Range(0, yUnitCount).Select(i => yUnitLength).ToArray();

            AppendSegmentedArrow(points, origin, xDirection, xDistances, headRadius, arrowAngle);
            var yDirection = new Vector2(-xDirection.y, xDirection.x);
            AppendSegmentedArrow(points, origin, yDirection, yDistances, headRadius, arrowAngle);
        }

        /// <summary>
        /// Append rectangle by center, half sizes and orientation.
        /// </summary>
        /// <param name="points"> Existing points collection. </param>
        /// <param name="center"> Rectangle center. </param>
        /// <param name="halfLength"> Half size of rectangle length. </param>
        /// <param name="halfWidth"> Half size of rectangle width. </param>
        /// <param name="rotationAngle"> Orientation in radians. </param>
        public static void AppendRectangle(IList<Vector2> points, Vector2 center, float halfLength, float halfWidth, float rotationAngle)
        {
            var vertex1 = center + new Vector2(halfLength, -halfWidth).Rotated(rotationAngle);
            var vertex2 = center + new Vector2(halfLength, halfWidth).Rotated(rotationAngle);
            var vertex3 = center + new Vector2(-halfLength, halfWidth).Rotated(rotationAngle);
            var vertex4 = center + new Vector2(-halfLength, -halfWidth).Rotated(rotationAngle);

            AppendLine(points, vertex1, vertex2);
            AppendLine(points, vertex2, vertex3);
            AppendLine(points, vertex3, vertex4);
            AppendLine(points, vertex4, vertex1);
        }

        /// <summary>
        /// Append rectangle by two vertices and height.
        /// </summary>
        /// <param name="points"> Existing points collection. </param>
        /// <param name="originVertex"> Primary vertex. </param>
        /// <param name="directionVertex"> Vertex relative to origin vertex setting up base side of rectangle. </param>
        /// <param name="height"> Distance of other side from base side. Positive is on left side of direction vertex/vector. </param>
        public static void AppendRectangle(IList<Vector2> points, Vector2 originVertex, Vector2 directionVertex, float height)
        {
            var normalVector = new Vector2(-directionVertex.x, directionVertex.y).Normalized();
            var vertex1 = originVertex;
            var vertex2 = originVertex + directionVertex;
            var vertex3 = vertex2 + normalVector * height;
            var vertex4 = vertex1 + normalVector * height;

            AppendLine(points, vertex1, vertex2);
            AppendLine(points, vertex2, vertex3);
            AppendLine(points, vertex3, vertex4);
            AppendLine(points, vertex4, vertex1);
        }

        public static void AppendTriangle(IList<Vector2> points, Vector2 a, Vector2 b, Vector2 c)
        {
            AppendLine(points, a, b);
            AppendLine(points, b, c);
            AppendLine(points, c, a);
        }

        public static void AppendRegularConvexPolygon(IList<Vector2> points, Vector2 center, float radius, int verticesCount, float rotationAngle)
        {
            var vertices = RegularConvexPolygonVertices(center, radius, verticesCount, rotationAngle)
                .ToArray();

            for (int i = 1; i < vertices.Length; i++)
                AppendLine(points, vertices[i - 1], vertices[i]);

            // closing line
            AppendLine(points, vertices[verticesCount - 1], vertices[0]);
        }

        public static void AppendCandleBar(IList<Vector2> points, Vector2 bottom, float bottomOffset, Vector2 top, float topOffset, float bodyHalfWidth)
        {
            var dirVector = (top - bottom).Normalized();
            var rectBottom = bottom + dirVector * bottomOffset;
            var rectTop = top - dirVector * topOffset;
            var rectCenter = (rectBottom + rectTop) / 2;

            AppendLine(points, bottom, rectBottom);
            AppendLine(points, top, rectTop);
            AppendRectangle(points, rectCenter, (rectCenter - rectBottom).Length(), bodyHalfWidth, dirVector.Angle());
        }

        #endregion
    }
}

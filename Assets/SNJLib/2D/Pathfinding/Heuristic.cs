﻿using UnityEngine;

namespace Pathfinding
{
    /// <summary>
    /// A* 길찾기에서 사용되는 의사결정 모음
    /// </summary>
    public static class Heuristic
    {
        public static float ManhattanDist(Vector3Int a, Vector3Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }

        static readonly float D = 1;
        static readonly float D2 = Mathf.Sqrt(2) * D;

        public static float DiagonalDist(Vector3Int a, Vector3Int b)
        {
            float dx = Mathf.Abs(a.x - b.x);
            float dy = Mathf.Abs(a.y - b.y);
            return D * (dx + dy) + (D2 - 2 * D) * Mathf.Min(dx, dy);
        }

        public static float EuclideanDist(Vector3Int a, Vector3Int b)
        {
            return Vector3Int.Distance(a, b);
        }
    }
}
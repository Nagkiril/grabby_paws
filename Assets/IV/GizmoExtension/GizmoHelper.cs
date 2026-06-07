using UnityEngine;

namespace IV.GizmoExtension
{
    public static class GizmoHelper
    {
        public static void DrawCircleXZ(Vector3 center, float radius, Color color)
        {
            if (radius > 0f)
            {
                Gizmos.color = color;

                const int segmentCount = 48;
                var previousPoint = center + new Vector3(radius, 0f, 0f);

                for (var segment = 1; segment <= segmentCount; segment++)
                {
                    var nextPointRatio = (float)segment / segmentCount;
                    var nextPointAngle = nextPointRatio * Mathf.PI * 2f;
                    var nextPoint = center + new Vector3(Mathf.Cos(nextPointAngle) * radius, 0f, Mathf.Sin(nextPointAngle) * radius);

                    Gizmos.DrawLine(previousPoint, nextPoint);
                    previousPoint = nextPoint;
                }
            }
        }
    }
}

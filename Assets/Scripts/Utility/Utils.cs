using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class Utils : MonoBehaviour
    {
        public static void AddPolygonCollider2D(GameObject go, Sprite sprite)
        {

            PolygonCollider2D polygon = go.AddComponent<PolygonCollider2D>();

            int shapeCount = sprite.GetPhysicsShapeCount();
            polygon.pathCount = shapeCount;
            var points = new List<Vector2>(64);
            for (int i = 0; i < shapeCount; i++)
            {
                sprite.GetPhysicsShape(i, points);
                polygon.SetPath(i, points);
            }

            polygon.isTrigger = true;

        }
    }
}
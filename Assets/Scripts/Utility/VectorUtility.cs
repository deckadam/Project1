using UnityEngine;

namespace Utility.Vector
{
    public static class VectorUtility
    {
        public static Vector3 ToVector3(this Vector2Int data)
        {
            return new Vector3(data.x, data.y, 0);
        }
    }
}
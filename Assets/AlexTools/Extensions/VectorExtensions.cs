using System;
using UnityEngine;

namespace AlexTools.Extensions
{
    public enum Orientation : byte
    {
        X0Y,
        Y0X,
        X0Z,
        Z0X,
        Y0Z,
        Z0Y
    }
    
    public static class VectorExtensions
    {
        #region Convertation
        
        public static Vector3 ToVector3(this Vector2 vector2, 
            Orientation orientation = Orientation.X0Y, float value = 0) => orientation switch
        {
            Orientation.X0Y => new Vector3(vector2.x, vector2.y, value),
            Orientation.Y0X => new Vector3(vector2.y, vector2.x, value),
            Orientation.X0Z => new Vector3(vector2.x, value, vector2.y),
            Orientation.Z0X => new Vector3(vector2.y, value, vector2.x),
            Orientation.Y0Z => new Vector3(value, vector2.x, vector2.y),
            Orientation.Z0Y => new Vector3(value, vector2.y, vector2.x),
            _ => throw new ArgumentException()
        };

        public static Vector2 ToVector2(this Vector3 vector3, 
            Orientation orientation = Orientation.X0Y) => orientation switch
        {
            Orientation.X0Y => new Vector2(vector3.x, vector3.y),
            Orientation.Y0X => new Vector2(vector3.y, vector3.x),
            Orientation.X0Z => new Vector2(vector3.x, vector3.z),
            Orientation.Z0X => new Vector2(vector3.z, vector3.x),
            Orientation.Y0Z => new Vector2(vector3.y, vector3.z),
            Orientation.Z0Y => new Vector2(vector3.z, vector3.y),
            _ => throw new ArgumentException()
        };
        
        public static Vector3Int ToVector3Int(this Vector2Int vector2Int, 
            Orientation orientation = Orientation.X0Y, int value = 0) => orientation switch
        {
            Orientation.X0Y => new Vector3Int(vector2Int.x, vector2Int.y, value),
            Orientation.Y0X => new Vector3Int(vector2Int.y, vector2Int.x, value),
            Orientation.X0Z => new Vector3Int(vector2Int.x, value, vector2Int.y),
            Orientation.Z0X => new Vector3Int(vector2Int.y, value, vector2Int.x),
            Orientation.Y0Z => new Vector3Int(value, vector2Int.x, vector2Int.y),
            Orientation.Z0Y => new Vector3Int(value, vector2Int.y, vector2Int.x),
            _ => throw new ArgumentException()
        };
        
        public static Vector2Int ToVector2Int(this Vector3Int vector3Int, 
            Orientation orientation = Orientation.X0Y) => orientation switch
        {
            Orientation.X0Y => new Vector2Int(vector3Int.x, vector3Int.y),
            Orientation.Y0X => new Vector2Int(vector3Int.y, vector3Int.x),
            Orientation.X0Z => new Vector2Int(vector3Int.x, vector3Int.z),
            Orientation.Z0X => new Vector2Int(vector3Int.z, vector3Int.x),
            Orientation.Y0Z => new Vector2Int(vector3Int.y, vector3Int.z),
            Orientation.Z0Y => new Vector2Int(vector3Int.z, vector3Int.y),
            _ => throw new ArgumentException()
        };

        public static Vector2 ToVector2(this Vector2Int vector2Int) => 
            new(vector2Int.x, vector2Int.y);
        public static Vector2 ToVector2(
            this Vector3Int vector3Int, 
            Orientation orientation = Orientation.X0Y) => 
            vector3Int.ToVector2Int(orientation).ToVector2();
        
        public static Vector3 ToVector3(this Vector3Int vector3Int)
            => new(vector3Int.x, vector3Int.y, vector3Int.z);
        public static Vector3 ToVector3(
            this Vector2Int vector2Int, 
            Orientation orientation = Orientation.X0Y, 
            int value = 0) => 
            vector2Int.ToVector2().ToVector3(orientation, value);

        #endregion

        #region Floor/Ceil/Round

        public static Vector2Int FloorVector2(this Vector2 vector2)
        {
            int x = Mathf.FloorToInt(vector2.x);
            int y = Mathf.FloorToInt(vector2.y);
            return new Vector2Int(x, y);
        }
        
        public static Vector2Int CeilVector2(this Vector2 vector2)
        {
            int x = Mathf.CeilToInt(vector2.x);
            int y = Mathf.CeilToInt(vector2.y);
            return new Vector2Int(x, y);
        }
        
        public static Vector2Int RoundVector2(this Vector2 vector2)
        {
            int x = Mathf.RoundToInt(vector2.x);
            int y = Mathf.RoundToInt(vector2.y);
            return new Vector2Int(x, y);
        }
        
        public static Vector3Int FloorVector3(this Vector3 vector3)
        {
            int x = Mathf.FloorToInt(vector3.x);
            int y = Mathf.FloorToInt(vector3.y);
            int z = Mathf.FloorToInt(vector3.z);
            return new Vector3Int(x, y, z);
        }
        
        public static Vector3Int CeilVector3(this Vector3 vector3)
        {
            int x = Mathf.CeilToInt(vector3.x);
            int y = Mathf.CeilToInt(vector3.y);
            int z = Mathf.CeilToInt(vector3.z);
            return new Vector3Int(x, y, z);
        }
        
        public static Vector3Int RoundVector3(this Vector3 vector3)
        {
            int x = Mathf.RoundToInt(vector3.x);
            int y = Mathf.RoundToInt(vector3.y);
            int z = Mathf.RoundToInt(vector3.z);
            return new Vector3Int(x, y, z);
        }

        #endregion

        #region Addition

        public static Vector2 Add(this Vector2 vector3, float x, float y = 0) => 
            new(vector3.x + x, vector3.y + y);

        public static Vector2 With(this Vector2 vector3, float? x = null, float? y = null) => 
            new(x ?? vector3.x, y ?? vector3.y);
        
        public static Vector2Int Add(this Vector2Int vector3Int, int x, int y = 0) => 
            new(vector3Int.x + x, vector3Int.y + y);

        public static Vector2Int With(this Vector2Int vector3Int, int? x , int? y = null) => 
            new(x ?? vector3Int.x, y ?? vector3Int.y);
        
        public static Vector3 Add(this Vector3 vector3, float x, float y = 0, float z = 0) => 
            new(vector3.x + x, vector3.y + y, vector3.z + z);

        public static Vector3 With(this Vector3 vector3, float? x = null, float? y = null, float? z = null) => 
            new(x ?? vector3.x, y ?? vector3.y, z ?? vector3.z);
        
        public static Vector3Int Add(this Vector3Int vector3Int, int x, int y = 0, int z = 0) => 
            new(vector3Int.x + x, vector3Int.y + y, vector3Int.z + z);

        public static Vector3Int With(this Vector3Int vector3Int, int? x, int? y = null, int? z = null) => 
            new(x ?? vector3Int.x, y ?? vector3Int.y, z ?? vector3Int.z);

        #endregion

        #region InRange

        public static bool InRange(this Vector2 current, Vector2 target, float range) => 
            (current - target).sqrMagnitude <= range * range;
        
        public static bool InRange(this Vector2Int current, Vector2Int target, float range) => 
            (current - target).sqrMagnitude <= range * range;
        
        public static bool InRange(this Vector3 current, Vector3 target, float range) => 
            (current - target).sqrMagnitude <= range * range;
        
        public static bool InRange(this Vector3Int current, Vector3Int target, float range) => 
            (current - target).sqrMagnitude <= range * range;

        #endregion

        #region ComponentDivision

        public static Vector2 ComponentDivide(this Vector2 dividend, Vector2 divisor) => new( 
                divisor.x != 0 ? dividend.x / divisor.x : dividend.x, 
                divisor.y != 0 ? dividend.y / divisor.y : dividend.y);
        
        public static Vector3 ComponentDivide(this Vector3 dividend, Vector3 divisor) => new( 
                divisor.x != 0 ? dividend.x / divisor.x : dividend.x, 
                divisor.y != 0 ? dividend.y / divisor.y : dividend.y, 
                divisor.z != 0 ? dividend.z / divisor.z : dividend.z);

        #endregion

        public static Vector2 ComponentMultiply(this Vector2 dividend, Vector2 divisor) => new(
                dividend.x * divisor.x,
                dividend.y * divisor.y);

        public static Vector3 ComponentMultiply(this Vector3 dividend, Vector3 divisor) => new(
                dividend.x * divisor.x,
                dividend.y * divisor.y,
                dividend.z * divisor.z);

        #region Scale

        public static Vector2 Scale(this Vector2 vector2, float x, float y = 1) => 
            new(vector2.x * x, vector2.y * y);
        
        public static Vector2Int Scale(this Vector2Int vector2Int, int x, int y = 1) => 
            new(vector2Int.x * x, vector2Int.y * y);
        
        public static Vector3 Scale(this Vector3 vector3, float x, float y = 1, float z = 1) => 
            new(vector3.x * x, vector3.y * y, vector3.z * z);
        
        public static Vector3Int Scale(this Vector3Int vector3Int, int x, int y = 1, int z = 1) => 
            new(vector3Int.x * x, vector3Int.y * y, vector3Int.z * z);

        #endregion

    }
}
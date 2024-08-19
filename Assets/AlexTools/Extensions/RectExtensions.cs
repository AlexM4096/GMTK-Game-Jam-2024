using UnityEngine;

namespace AlexTools.Extensions
{
    public static class RectExtensions
    {
        #region Scale

        public static Rect Scale(this Rect rect, float scale) 
            => new(rect.position * scale, rect.size * scale);
        
        public static Rect Scale(this Rect rect, float scaleX, float scaleY) 
            => new(rect.position.Scale(scaleX, scaleY), rect.size.Scale(scaleX, scaleY));
        
        public static Rect Scale(this Rect rect, Vector2 scale) 
            => new(rect.position * scale, rect.size * scale);
        
        public static RectInt Scale(this RectInt rectInt, int scale) 
            => new(rectInt.position * scale, rectInt.size * scale);
        
        public static RectInt Scale(this RectInt rectInt, int scaleX, int scaleY) 
            => new(rectInt.position.Scale(scaleX, scaleY), rectInt.size.Scale(scaleX, scaleY));
        
        public static RectInt Scale(this RectInt rectInt, Vector2Int scale) 
            => new(rectInt.position * scale, rectInt.size * scale);

        #endregion

        #region Border

        public static Rect AddBorder(this Rect rect, float radius) => new(
            rect.position - Vector2.one * radius,
            rect.size + Vector2.one * (radius * 2));
        
        public static Rect AddBorder(this Rect rect, float radiusX, float radiusY) => new(
            rect.position - Vector2.right * radiusX - Vector2.up * radiusY,
            rect.size + (Vector2.right * radiusX + Vector2.up * radiusY) * 2);
        
        public static Rect AddBorder(
            this Rect rect, float topRadius, float rightRadius, float bottomRadius, float leftRadius) => new(
            rect.position - Vector2.right * (rightRadius - leftRadius) - Vector2.up * (topRadius - bottomRadius),
            rect.size + Vector2.right * (rightRadius + leftRadius) + Vector2.up * (topRadius + bottomRadius));
        
        public static RectInt AddBorder(this RectInt rectInt, int radius) => new(
            rectInt.position - Vector2Int.one * radius,
            rectInt.size + Vector2Int.one * (radius * 2));
        
        public static RectInt AddBorder(this RectInt rectInt, int radiusX, int radiusY) => new(
            rectInt.position - Vector2Int.right * radiusX - Vector2Int.up * radiusY,
            rectInt.size + (Vector2Int.right * radiusX + Vector2Int.up * radiusY) * 2);
        
        public static RectInt AddBorder(
            this RectInt rectInt, int topRadius, int rightRadius, int bottomRadius, int leftRadius) => new(
            rectInt.position - Vector2Int.right * (rightRadius - leftRadius) - Vector2Int.up * (topRadius - bottomRadius),
            rectInt.size + Vector2Int.right * (rightRadius + leftRadius) + Vector2Int.up * (topRadius + bottomRadius));

        #endregion

        public static Rect Centralize(this Rect rect) 
            => new(rect.position - rect.size / 2, rect.size);
        
        public static RectInt Centralize(this RectInt rectInt) 
            => new(rectInt.position - rectInt.size / 2, rectInt.size);
    }
}
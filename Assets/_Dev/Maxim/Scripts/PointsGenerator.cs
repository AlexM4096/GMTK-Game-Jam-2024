using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointsGenerator : MonoBehaviour
{
    public float radius = 2f;
    public float circlesGap = 0f;

    private readonly List<Transform> _points = new();

    private void OnDrawGizmosSelected()
    {
        if (_points == null || _points.Count == 0)
            return;

        Gizmos.color = Color.green;

        foreach (var point in _points)
        {
            Gizmos.DrawWireSphere(point.position, radius);
        }
    }

    public Transform[] GeneratePoints(int numberOfPoints)
    {
        ClearPoints();

        if (numberOfPoints <= 0)
        {
            return Array.Empty<Transform>();
        }

        var pointsToGenerate = numberOfPoints;

        int circleNumber = 1;
        float diameter = radius * 2;
        while (pointsToGenerate > 0)
        {
            var circleLength = 2 * Mathf.PI * diameter * circleNumber;
            var circlesFit = Mathf.Min(Mathf.FloorToInt(circleLength / diameter), pointsToGenerate);
            var angleStep = 360.0f / circlesFit * Mathf.Deg2Rad;

            var realRadius = (diameter + circlesGap) * circleNumber;
            for (int i = 0; i < circlesFit; i++)
            {
                var angle = angleStep * i;

                float offsetRadius = radius * 0.8f;
                float offsetRandomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

                float x =
                    realRadius * Mathf.Cos(angle) + offsetRadius * Mathf.Cos(offsetRandomAngle);
                float y =
                    realRadius * Mathf.Sin(angle) + offsetRadius * Mathf.Sin(offsetRandomAngle);

                CreatePoint(new Vector3(x, y, 0));
            }

            pointsToGenerate -= circlesFit;
            circleNumber += 1;
        }

        return _points.ToArray();
    }

    private void ClearPoints()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            Destroy(_points[i].gameObject);
        }

        _points.Clear();
    }

    private void CreatePoint(Vector3 localPosition)
    {
        GameObject point = new GameObject("Point " + (_points.Count + 1));
        point.transform.SetParent(transform);
        point.transform.localPosition = localPosition;
        _points.Add(point.transform);
    }
}

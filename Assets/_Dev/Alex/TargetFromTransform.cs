using UnityEngine;

namespace Alex
{
    public readonly struct TargetFromTransform : ITargetable
    {
        private readonly Transform _transform;

        public Vector3 Position => _transform.position;

        public TargetFromTransform(Transform transform) => _transform = transform;
    }
}
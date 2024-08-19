using UnityEngine;

namespace Alex
{
    public interface ITargetable
    {
        Vector3 Position { get; }
        Vector3 Velocity { get; }
    }
}
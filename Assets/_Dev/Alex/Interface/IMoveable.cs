using UnityEngine;

namespace Alex
{
    public interface IMoveable
    {      
        Vector3 Position { get; }
        Vector3 Velocity { get; }

        bool CanMove { get; set; }

        void MoveTo(Vector3 position);
        void MoveBy(Vector3 delta);

        void TurnTo(Vector3 positon);
    }
}
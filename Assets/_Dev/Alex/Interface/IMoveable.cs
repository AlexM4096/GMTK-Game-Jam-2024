using UnityEngine;

namespace Alex
{
    public interface IMoveable
    {      
        Vector3 Position { get; }
        Vector3 Velocity { get; }

        float Speed { get; set; }
        bool CanMove { get; set; }

        void MoveTo(Vector3 position);
        void MoveBy(Vector3 delta);

        void MoveAndLook(Vector3 position)
        {
            MoveTo(position);
            LookAt(position);
        }

        void Stop();

        void LookAt(Vector3 positon);
    }
}
using UnityEngine;
using Pathfinding;

namespace Alex
{
    public class AIMoveable : IMoveable
    {
        private readonly IAstarAI _astarAI;

        public AIMoveable(IAstarAI astarAI) => _astarAI = astarAI;

        public Vector3 Position => _astarAI.position;
        public Vector3 Velocity => _astarAI.velocity;

        public float Speed 
        { 
            get => _astarAI.maxSpeed;
            set => _astarAI.maxSpeed = value;
        }

        public bool CanMove 
        { 
            get => _astarAI.canMove; 
            set => _astarAI.canMove = value; 
        }

        public void MoveTo(Vector3 position) => _astarAI.destination = position;
        public void MoveBy(Vector3 delta) => _astarAI.Move(delta);

        public void Stop() => MoveBy(Vector3.zero);

        public void LookAt(Vector3 positon)
        {
            var delta = positon - Position;
            _astarAI.rotation = delta.x > 0 ? Right : Left;
        }

        private static readonly Quaternion Right = Quaternion.Euler(0, 0, 0);
        private static readonly Quaternion Left = Quaternion.Euler(0, 180, 0);
    }
}
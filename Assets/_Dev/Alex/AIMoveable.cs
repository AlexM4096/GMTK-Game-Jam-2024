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

        public bool CanMove 
        { 
            get => _astarAI.canMove; 
            set => _astarAI.canMove = value; 
        }

        public void MoveTo(Vector3 position) => _astarAI.destination = position;
        public void MoveBy(Vector3 delta) => _astarAI.Move(delta);

        public void TurnTo(Vector3 positon) { }

    }
}
using UnityEngine;

namespace Alex
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMove : MonoBehaviour, IMoveable
    {
        [SerializeField] private new Rigidbody2D rigidbody;

        public Vector3 Position => transform.position;
        public Vector3 Velocity => rigidbody.velocity;

        [field: SerializeField]
        public float Speed { get; set; }
        public bool CanMove { get; set; } = true;

        public void MoveBy(Vector3 delta) => rigidbody.velocity = delta.normalized * Speed;
        public void MoveTo(Vector3 position) => MoveBy(position - Position);

        public void Stop() => rigidbody.velocity = Vector3.zero;

        public void LookAt(Vector3 positon)
        {
            
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(Position, Position + Velocity);
        }
#endif
    }
}
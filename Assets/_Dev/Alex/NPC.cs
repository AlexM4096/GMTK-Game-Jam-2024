using NPBehave;
using Pathfinding;
using UnityEngine;

namespace Alex
{
    public class NPC : MonoBehaviour
    {
        private const string TargetPositionKey = "TargetPosition";
        private const string DistanceToTargetKey = "DistanceToTarget";
        private const string TargetKey = "Target";
        private const string HasTargetKey = "HaveTarget";
        private const string VectorToTargetKey = "VectorToTarget";
        private const string DirectionToTargetKey = "DirectionToTarget";

        private const float TargetCloseRadius = 5f;

        [SerializeField] private AIPath aiPath;

        private Blackboard _playerBlackboard;
        private Root _behaviorTree;

        private IMoveable _moveable;
        private IAttackable _attackable;

        private Blackboard Blackboard => _behaviorTree.Blackboard;

        private void Awake()
        {
            _attackable = GetComponent<IAttackable>();
        }

        private void Start()
        {
            _playerBlackboard = UnityContext.GetSharedBlackboard(Player.BlackboardKey);

            _moveable = new AIMove(aiPath);

            _behaviorTree = CreateBehaviorTree();
            _behaviorTree.Start();

            Blackboard[TargetKey] = _playerBlackboard.Get<ITargetable>(Player.Target);

#if UNITY_EDITOR
            Debugger debugger = gameObject.AddComponent<Debugger>();
            debugger.BehaviorTree = _behaviorTree;
#endif
        }

        private Root CreateBehaviorTree()
        {
            return new Root(
                new Service(0.125f, UpdateBlackboard,
                    new Selector(
                        new BlackboardCondition(HasTargetKey, Operator.IS_EQUAL, false, Stops.SELF,
                            new Action(SetTarget)
                        ),
                        new BlackboardCondition(DistanceToTargetKey, Operator.IS_SMALLER, TargetCloseRadius, Stops.IMMEDIATE_RESTART,
                            new Action(() =>
                            {
                                var position = Blackboard.Get<Vector3>(DirectionToTargetKey) * -10;
                                _moveable.MoveAndLook(position);
                            })
                        )
                    )
                )
            );
        }

        private void UpdateBlackboard()
        {
            ITargetable target = Blackboard.Get<ITargetable>(TargetKey);
            Blackboard[TargetKey] = target;

            bool isTargetNull = target == null;
            Blackboard[HasTargetKey] = !isTargetNull;

            if (isTargetNull) return;

            Vector3 targetPosition = target.Position;
            Blackboard[TargetPositionKey] = targetPosition;

            Vector3 vectorToTarget = targetPosition - transform.position;
            Blackboard[VectorToTargetKey] = vectorToTarget;

            Vector3 directionToTarget = vectorToTarget.normalized;
            Blackboard[DirectionToTargetKey] = directionToTarget;

            float distanceToTarget = vectorToTarget.magnitude;
            Blackboard[DistanceToTargetKey] = distanceToTarget;
        }
        private ITargetable SelectTarget()
        {
            return _playerBlackboard.Get<ITargetable>(Player.Target);
        }

        private void SetTarget()
        {
            var target = SelectTarget();
            Blackboard[TargetKey] = target;
            _attackable.Target = target;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, TargetCloseRadius);

            if (_moveable == null) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(_moveable.Position, _moveable.Position + _moveable.Velocity);
        }
#endif
    }
}
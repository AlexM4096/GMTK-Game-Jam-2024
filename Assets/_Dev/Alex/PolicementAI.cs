using Alex;
using NPBehave;
using Pathfinding;
using UnityEngine;

public class PolicementAI : MonoBehaviour
{
    private const string TargetPositionKey = "TargetPosition";
    private const string DistanceToTargetKey = "DistanceToTarget";
    private const string TargetKey = "Target";
    private const string HasTargetKey = "HaveTarget";
    private const string VectorToTargetKey = "VectorToTarget";
    private const string DirectionToTargetKey = "DirectionToTarget";
    private const string AmmoAmountKey = "AmmoAmount";
    private const string IsReloadingKey = "IsReloading";

    private const int AmmoInClip = 12;
    private const float AttackDistance = 4f;
    private const float ReloadTime = 2f;
    private const float TargetCloseRadius = 2.5f;

    [SerializeField] private AIPath aiPath;

    private Blackboard _playerBlackboard;
    private Root _behaviorTree;

    private IMoveable _moveable;
    private IAttackable _attackable;

    private Blackboard Blackboard => _behaviorTree.Blackboard;

    private void Start()
    {
        _playerBlackboard = UnityContext.GetSharedBlackboard(Player.BlackboardKey);

        _moveable = new AIMoveable(aiPath);

        _behaviorTree = CreateBehaviorTree();
        _behaviorTree.Start();

        Blackboard[AmmoAmountKey] = AmmoInClip;
        Blackboard[IsReloadingKey] = false;

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
                        new Action(() => { Blackboard[TargetKey] = SelectTarget(); })
                    ),
                    new Failer(new Action(() => { _moveable.CanMove = true; })),
                    new BlackboardCondition(DistanceToTargetKey, Operator.IS_SMALLER, TargetCloseRadius, Stops.IMMEDIATE_RESTART,
                        new Action(() => _moveable.MoveTo(Blackboard.Get<Vector3>(DirectionToTargetKey) * -10))
                    ),
                    new BlackboardCondition(DistanceToTargetKey, Operator.IS_GREATER, AttackDistance, Stops.IMMEDIATE_RESTART,
                        new Action(() => _moveable.MoveTo(Blackboard.Get<Vector3>(TargetPositionKey)))
                    ),
                    new Failer(new Action(() => { _moveable.CanMove = false; })),
                    new BlackboardCondition(DistanceToTargetKey, Operator.IS_SMALLER, AttackDistance, Stops.IMMEDIATE_RESTART,
                        new Selector(
                            new BlackboardCondition(AmmoAmountKey, Operator.IS_GREATER, 0, Stops.IMMEDIATE_RESTART,
                                new Cooldown(0.3f,
                                    new Sequence(
                                        new Action(() => { Blackboard[AmmoAmountKey] = Blackboard.Get<int>(AmmoAmountKey) - 1; }),
                                        new Action(() => _attackable?.Attack())
                                    )
                                )
                            ),
                            new Sequence(
                                new Wait(ReloadTime),
                                new Action(() => { Blackboard[AmmoAmountKey] = AmmoInClip; })
                            )
                        )
                    )
                )
            )
        );
    }

    private void UpdateBlackboard()
    {
        Transform target = Blackboard.Get<Transform>(TargetKey);
        Blackboard[TargetKey] = target;

        bool isTargetNull = target == null;
        Blackboard[HasTargetKey] = !isTargetNull;

        if (isTargetNull) return;

        Vector3 targetPosition = target.position;
        Blackboard[TargetPositionKey] = targetPosition;

        Vector3 vectorToTarget = targetPosition - transform.position;
        Blackboard[VectorToTargetKey] = vectorToTarget;

        Vector3 directionToTarget = vectorToTarget.normalized;
        Blackboard[DirectionToTargetKey] = directionToTarget;

        float distanceToTarget = vectorToTarget.magnitude;
        Blackboard[DistanceToTargetKey] = distanceToTarget;
    }

    private Transform SelectTarget()
    {
        return _playerBlackboard.Get<Transform>(Player.TransformKey);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AttackDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TargetCloseRadius);
    }
#endif

}

using NPBehave;
using Unity.VisualScripting;
using UnityEngine;

public class PolicementAI : MonoBehaviour
{
    private const string PlayerPositionKey = "PlayerPosition";
    private const string DistanceToPlayerKey = "DistanceToPlayer";

    private Blackboard _playerBlackboard;

    private Root _behaviorTree;

    private void Start()
    {
        _playerBlackboard = UnityContext.GetSharedBlackboard(Player.BlackboardKey);

        _behaviorTree = CreateBehaviorTree();
        _behaviorTree.Start();

#if UNITY_EDITOR
        Debugger debugger = gameObject.AddComponent<Debugger>();
        debugger.BehaviorTree = _behaviorTree;
#endif
    }

    private Root CreateBehaviorTree()
    {
        return new Root(
            new Service(0.125f, UpdateBlackboard,
                new BlackboardCondition(DistanceToPlayerKey, Operator.IS_SMALLER, 7.5f, Stops.IMMEDIATE_RESTART,
                    new NPBehave.Sequence(
                        new Action(() => print("DDD")),
                        new Wait(1f)
                    )
                )
            )
        );
    }

    private void UpdateBlackboard()
    {
        Vector3 playerPosition = _playerBlackboard.Get<Vector3>(Player.PositionKey);
        float distanceToPlayer = (playerPosition - transform.position).magnitude;

        _behaviorTree.Blackboard[PlayerPositionKey] = playerPosition;
        _behaviorTree.Blackboard[DistanceToPlayerKey] = distanceToPlayer;
    }
}

using NPBehave;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const string BlackboardKey = "player";
    public const string PositionKey = "position";

    private Blackboard _blackboard;

    private void Start()
    {
        _blackboard = UnityContext.GetSharedBlackboard(BlackboardKey);
    }

    private void Update()
    {
        _blackboard[PositionKey] = transform.position;
    }
}

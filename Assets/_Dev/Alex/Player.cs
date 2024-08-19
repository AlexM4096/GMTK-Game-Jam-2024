using Alex;
using NPBehave;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const string BlackboardKey = "player";
    public const string TransformKey = "transform";

    private Blackboard _blackboard;

    private void Awake()
    {
        _blackboard = UnityContext.GetSharedBlackboard(BlackboardKey);
        _blackboard[TransformKey] = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Flyweight.Flyweight>(out var flyweight))
            return;

        flyweight.ReleaseSelf();
    }
}

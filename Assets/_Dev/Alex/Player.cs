using Alex;
using NPBehave;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerController controller;

    public const string BlackboardKey = "Player";
    public const string Target = "Target";

    private Blackboard _blackboard;

    public void TakeDamage(float amount, IAttackable source)
    {
        // print($"Take {amount} damage from {source}");
    }

    private void Awake()
    {
        _blackboard = UnityContext.GetSharedBlackboard(BlackboardKey);
        _blackboard[Target] = controller;
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (!collision.gameObject.TryGetComponent<Flyweight.Flyweight>(out var flyweight))
    //         return;
    // 
    //     flyweight.ReleaseSelf();
    // }
}

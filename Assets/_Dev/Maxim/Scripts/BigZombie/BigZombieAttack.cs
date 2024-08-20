using System;
using System.Collections;
using Alex;
using JSAM;
using UnityEngine;

public class BigZombieAttack : MonoBehaviour, IAttackable
{
    [SerializeField]
    private Zombie mainZombie;

    [field: SerializeField]
    public float Damage { get; set; } = 1f;
    public bool CanAttack { get; set; }
    float IAttackable.Damage { get; set; }
    public ITargetable Target { get; set; }

    public event Action Attacked;

    [SerializeField]
    private float delayBetweenAttacks = 2f;

    [SerializeField]
    private LayerMask scanMask;

    [SerializeField]
    private Vector2 scanOrigin;

    [SerializeField]
    private Vector2 scanSize;

    private readonly Collider2D[] _scanResults = new Collider2D[1];
    private ContactFilter2D _contactFilter;
    private Coroutine _attackRoutine;

    private void Awake()
    {
        _contactFilter = new ContactFilter2D() { layerMask = scanMask, useLayerMask = true };
    }

    private void OnDisable()
    {
        StopScan();
    }

    private IEnumerator AttackRoutine()
    {
        var wait = new WaitForSeconds(0.125f);
        while (true)
        {
            yield return wait;

            Vector2 scaledOrigin = Vector2.Scale(scanOrigin, transform.localScale);
            Vector2 scaledSize = Vector2.Scale(scanSize, transform.localScale);

            var count = Physics2D.OverlapCapsule(
                (Vector2)transform.position + scaledOrigin,
                scaledSize,
                CapsuleDirection2D.Vertical,
                0,
                _contactFilter,
                _scanResults
            );
            if (count > 0)
            {
                var target = _scanResults[0].GetComponentInParent<IDamageable>();
                mainZombie.StartAttacking();
                yield return new WaitForSeconds(0.8f);

                AudioManager.PlaySound(AudioLibrarySounds.ZombieAttack);
                Attacked?.Invoke();

                if (target != null)
                {
                    target.TakeDamage(Damage, this);

                    yield return new WaitForSeconds(0.8f);
                    mainZombie.StopAttacking();

                    yield return new WaitForSeconds(delayBetweenAttacks);
                }
            }
        }
    }

    public void StartScan()
    {
        _attackRoutine = StartCoroutine(AttackRoutine());
    }

    public void StopScan()
    {
        if (mainZombie != null && mainZombie.IsAttacking)
        {
            mainZombie.StopAttacking();
        }
        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector2 scaledOrigin = Vector2.Scale(scanOrigin, transform.localScale);
        Vector2 scaledSize = Vector2.Scale(scanSize, transform.localScale);

        Vector2 capsuleCenter = (Vector2)transform.position + scaledOrigin;

        float capsuleHeight = scaledSize.y;
        float capsuleRadius = scaledSize.x / 2;

        Vector2 point1 = capsuleCenter + Vector2.up * (capsuleHeight / 2 - capsuleRadius);
        Vector2 point2 = capsuleCenter - Vector2.up * (capsuleHeight / 2 - capsuleRadius);

        Gizmos.DrawWireSphere(point1, capsuleRadius);
        Gizmos.DrawWireSphere(point2, capsuleRadius);
        Gizmos.DrawLine(
            point1 + Vector2.left * capsuleRadius,
            point2 + Vector2.left * capsuleRadius
        );
        Gizmos.DrawLine(
            point1 + Vector2.right * capsuleRadius,
            point2 + Vector2.right * capsuleRadius
        );
    }
}

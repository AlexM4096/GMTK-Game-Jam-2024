using System.Collections;
using Alex;
using JSAM;
using UnityEngine;

public class BigZombieAttack : MonoBehaviour, IAttackable
{
    [SerializeField]
    private Zombie mainZombie;

    [field: SerializeField]
    public float Damage { get; set; }
    public bool CanAttack { get; set; }
    public ITargetable Target { get; set; }

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

    private void OnEnable()
    {
        _attackRoutine = StartCoroutine(AttackRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_attackRoutine);
        _attackRoutine = null;
    }

    private IEnumerator AttackRoutine()
    {
        var wait = new WaitForSeconds(0.125f);
        while (true)
        {
            yield return wait;

            var count = Physics2D.OverlapCapsule(
                (Vector2)transform.position + scanOrigin,
                scanSize,
                CapsuleDirection2D.Vertical,
                0,
                _contactFilter,
                _scanResults
            );
            if (count > 0)
            {
                var target = _scanResults[0].GetComponentInParent<IDamageable>();
                mainZombie.StartAttacking();
                yield return new WaitForSeconds(0.4f);
                AudioManager.PlaySound(AudioLibrarySounds.ZombieAttack);
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

    private void OnDrawGizmosSelected()
    {
        // Устанавливаем цвет для Gizmos
        Gizmos.color = Color.yellow;

        // Вычисляем центр капсулы
        Vector2 capsuleCenter = transform.position + (Vector3)scanOrigin;

        // Рисуем капсулу в виде комбинации двух сфер и цилиндра
        float capsuleHeight = scanSize.y;
        float capsuleRadius = scanSize.x / 2;

        // Вычисляем позиции верхней и нижней частей капсулы
        Vector2 point1 = capsuleCenter + Vector2.up * (capsuleHeight / 2 - capsuleRadius);
        Vector2 point2 = capsuleCenter - Vector2.up * (capsuleHeight / 2 - capsuleRadius);

        // Рисуем капсулу с использованием Gizmos
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

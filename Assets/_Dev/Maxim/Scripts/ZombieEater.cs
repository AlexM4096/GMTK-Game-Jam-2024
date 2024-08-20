using System.Collections;
using UnityEngine;

public class ZombieEater : MonoBehaviour
{
    public ZombieCreator ZombieCreator;

    [SerializeField]
    private Zombie zombie;

    [SerializeField]
    private LayerMask eatableMask;

    [SerializeField]
    private Vector2 scanOrigin = Vector2.zero;

    [SerializeField]
    private float radius = 1f;

    private Collider2D[] _targets = new Collider2D[1];
    private ContactFilter2D _contactFilter;
    private Coroutine _scanRoutine;

    public bool IsScanActive = true;

    private void Start()
    {
        _contactFilter = new ContactFilter2D() { layerMask = eatableMask, useLayerMask = true };
        StartScan();
    }

    private IEnumerator ScannerRoutine()
    {
        var waitTime = new WaitForSeconds(0.125f);
        while (true)
        {
            yield return waitTime;
            var count = Physics2D.OverlapCircle(
                transform.position + (Vector3)scanOrigin,
                radius,
                _contactFilter,
                _targets
            );
            if (count > 0)
            {
                var eatableByZombie = _targets[0].GetComponentInParent<EatableByZombie>();
                if (eatableByZombie && !eatableByZombie.IsEated)
                {
                    zombie.StartAttacking();
                    yield return new WaitForSeconds(0.6f);

                    if (_targets[0] != null && !eatableByZombie.IsEated && eatableByZombie.Eated())
                    {
                        ZombieCreator.CreateZombie(_targets[0].transform.position);
                    }

                    yield return new WaitForSeconds(0.3f);
                    zombie.StopAttacking();
                }
            }
        }
    }

    public void StartScan()
    {
        _scanRoutine = StartCoroutine(ScannerRoutine());
    }

    public void StopScan()
    {
        StopCoroutine(_scanRoutine);
        _scanRoutine = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (Vector3)scanOrigin, radius);
    }
}

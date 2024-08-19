using System.Collections;
using UnityEngine;

public class ZombieEater : MonoBehaviour
{
    public ZombieCreator ZombieCreator;

    [SerializeField]
    private Animator zombieAnimator;

    [SerializeField]
    private LayerMask eatableMask;

    [SerializeField]
    private float damage = 10f;

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
            var count = Physics2D.OverlapCircle(
                transform.position + (Vector3)scanOrigin,
                radius,
                _contactFilter,
                _targets
            );
            if (count > 0)
            {
                var eatableByZombie = _targets[0].GetComponentInParent<EatableByZombie>();
                if (eatableByZombie)
                {
                    zombieAnimator.Play("attack");
                    yield return new WaitForSeconds(0.5f);

                    if (_targets[0] != null)
                    {
                        if (
                            Vector3.Distance(transform.position, _targets[0].transform.position)
                            < radius
                        )
                        {
                            ZombieCreator.CreateZombie(_targets[0].transform.position);
                            eatableByZombie.Eated();
                        }
                    }

                    yield return new WaitForSeconds(0.3f);

                    // TODO Replace on "idle" animation
                    zombieAnimator.Play("walk");
                }
            }
            yield return waitTime;
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

using UnityEngine;
using UnityEngine.Events;

public class EatableByZombie : MonoBehaviour
{
    [SerializeField]
    private int health = 1;

    [SerializeField]
    private UnityEvent onDamage;

    public bool IsEated => health <= 0;

    public bool Eated()
    {
        if (health > 0)
        {
            onDamage?.Invoke();
        }

        health -= 1;
        if (health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
        // TODO Possibly play death animation
    }
}

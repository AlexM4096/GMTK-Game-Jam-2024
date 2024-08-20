using UnityEngine;

public class EatableByZombie : MonoBehaviour
{
    [SerializeField]
    private int health = 1;

    public bool IsEated => health <= 0;

    public bool Eated()
    {
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

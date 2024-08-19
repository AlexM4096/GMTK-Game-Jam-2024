using UnityEngine;

public class EatableByZombie : MonoBehaviour
{
    [SerializeField]
    private float health;

    public void Eated()
    {
        Destroy(gameObject);
        // TODO Possibly play death animation
    }
}

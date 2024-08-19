using UnityEngine;

public class MainZombie : MonoBehaviour
{
    [field: SerializeField]
    public SpriteRenderer Sprite { get; private set; }

    [field: SerializeField]
    public ZombieEater ZombieEater { get; private set; }
}

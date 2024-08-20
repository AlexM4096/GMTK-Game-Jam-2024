using UnityEngine;
using UnityEngine.Events;

public class MainZombieDeathTracker : MonoBehaviour
{
    [SerializeField]
    private Zombie mainZombie;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private ZombieGroup zombieGroup;

    [SerializeField]
    private UnityEvent onDeath;

    void Start()
    {
        mainZombie.Health.DeathEvent += OnDeath;
    }

    private void OnDeath()
    {
        mainZombie.Health.DeathEvent -= OnDeath;
        playerController.IsActive = false;
        onDeath?.Invoke();
        zombieGroup.KillAllZombiesInGroup();
        zombieGroup.KillZombie(mainZombie);
    }
}

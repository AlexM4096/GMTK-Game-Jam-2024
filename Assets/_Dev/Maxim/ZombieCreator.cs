using UnityEngine;

public class ZombieCreator : MonoBehaviour
{
    [SerializeField]
    private int initialZombieCount = 0;

    [SerializeField]
    private Zombie zombie;

    [SerializeField]
    private ZombieGroup zombieGroup;

    private void Start()
    {
        for (int i = 0; i < initialZombieCount; i++)
        {
            CreateZombie();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateZombie();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            zombieGroup.MoveAllZombiesToTheirPoints();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            zombieGroup.MoveAllZombiesToCenter();
        }
    }

    [ContextMenu("Create Zombie")]
    private void CreateZombie()
    {
        var createdZombie = Instantiate(zombie);
        createdZombie.Sprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        zombieGroup.AddZombieToGroup(createdZombie);
    }
}

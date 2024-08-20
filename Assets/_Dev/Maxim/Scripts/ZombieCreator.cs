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

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CreateZombie();
        }
    }
#endif

    [ContextMenu("Create Zombie")]
    public void CreateZombie()
    {
        CreateZombie(Vector3.zero);
    }

    public void CreateZombie(Vector3 position)
    {
        var createdZombie = Instantiate(zombie, position, Quaternion.identity);
        createdZombie.transform.position = position;
        zombieGroup.AddZombieToGroup(createdZombie);

        var zombieEater = createdZombie.GetComponentInChildren<ZombieEater>();
        if (zombieEater != null)
        {
            zombieEater.ZombieCreator = this;
        }
    }
}

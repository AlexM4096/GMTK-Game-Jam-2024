using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombieGroup : MonoBehaviour
{
    [SerializeField]
    private PointsGenerator pointsGenerator;

    [SerializeField]
    private Transform zombiesContainer;

    [SerializeField]
    private Transform offCenter;

    [SerializeField]
    private GameObject zombieCorpse;

    public int ZombieCount => _zombies.Count;

    private readonly List<Zombie> _zombies = new();
    public IEnumerable<Zombie> Zombies => _zombies;

    private Transform[] _zombiePoints = new Transform[0];

    public void AddZombieToGroup(Zombie zombie)
    {
        _zombies.Add(zombie);
        _zombiePoints = pointsGenerator.GeneratePoints(_zombies.Count);
        SetZombieOnPoints();

        zombie.Health.DeathEvent += () =>
        {
            RemoveZombieFromGroup(zombie);
            Instantiate(zombieCorpse, zombie.transform.position, Quaternion.identity);
            Destroy(zombie.gameObject);
        };

        if (zombiesContainer != null)
        {
            zombie.transform.SetParent(zombiesContainer);
        }
    }

    public void RemoveZombieFromGroup(Zombie zombie)
    {
        _zombies.Remove(zombie);
        _zombiePoints = pointsGenerator.GeneratePoints(_zombies.Count);
        SetZombieOnPoints();
    }

    [ContextMenu("Move all zombies to center")]
    public void MoveAllZombiesToBigZombie()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            _zombies[i].Target = offCenter;
            _zombies[i].GoToBigZombie();
        }
    }

    [ContextMenu("Move all zombies to their points")]
    public void MoveAllZombiesToTheirPoints()
    {
        for (int i = 0; i < _zombiePoints.Length; i++)
        {
            _zombies[i].Target = _zombiePoints[i];
            _zombies[i].GoFromBigZombie();
        }
    }

    private void SetZombieOnPoints()
    {
        for (int i = 0; i < _zombiePoints.Length; i++)
        {
            _zombies[i].Target = _zombiePoints[i];
        }
    }

    public bool AllZombiesReachedPoints()
    {
        return _zombies.All(z => z.AiPath.reachedEndOfPath);
    }

    public int GetReachedEndOfPath()
    {
        return _zombies.Count(z => z.AiPath.reachedEndOfPath);
    }
}

using System;
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

    public int ZombieCount => _zombies.Count;

    private readonly List<Zombie> _zombies = new();

    private Transform[] _zombiePoints = new Transform[0];

    private void Start() { }

    public void AddZombieToGroup(Zombie zombie)
    {
        _zombies.Add(zombie);
        _zombiePoints = pointsGenerator.GeneratePoints(_zombies.Count);
        SetZombieOnPoints();

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
    public void MoveAllZombiesToCenter()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            _zombies[i].DestinationSetter.target = offCenter;
        }
    }

    [ContextMenu("Move all zombies to their points")]
    public void MoveAllZombiesToTheirPoints()
    {
        SetZombieOnPoints();
    }

    public void HideZombies()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            _zombies[i].Sprite.gameObject.SetActive(false);
        }
    }

    public void ShowZombies()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            _zombies[i].Sprite.gameObject.SetActive(true);
        }
    }

    private void SetZombieOnPoints()
    {
        for (int i = 0; i < _zombiePoints.Length; i++)
        {
            _zombies[i].DestinationSetter.target = _zombiePoints[i];
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

    public void EnableScan()
    {
        _zombies.ForEach(z => z.GetComponentInChildren<ZombieEater>().StartScan());
    }
}

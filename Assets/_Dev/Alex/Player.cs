using Alex;
using NPBehave;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ZombieGroup zombieGroup;

    public const string BlackboardKey = "Player";
    public const string TargetsKey = "TargetsKey";

    private Blackboard _blackboard;

    private void Awake()
    {
        _blackboard = UnityContext.GetSharedBlackboard(BlackboardKey);   
    }

    private void Update()
    {
        _blackboard[TargetsKey] = zombieGroup.Zombies.Select(x => new TargetFromTransform(x.transform) as ITargetable);
    }
}

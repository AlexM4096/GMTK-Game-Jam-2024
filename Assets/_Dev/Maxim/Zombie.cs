using Pathfinding;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [field: SerializeField]
    public AIDestinationSetter DestinationSetter { get; private set; }

    [field: SerializeField]
    public SpriteRenderer Sprite { get; private set; }

    [field: SerializeField]
    public AIPath AiPath { get; private set; }
}

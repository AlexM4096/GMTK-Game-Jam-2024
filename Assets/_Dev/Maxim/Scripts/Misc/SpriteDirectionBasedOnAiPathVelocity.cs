using Pathfinding;
using UnityEngine;

public class SpriteDirectionBasedOnAiPathVelocity : MonoBehaviour
{
    [SerializeField]
    private AIPath aIPath;

    [SerializeField]
    private SpriteRenderer sprite;

    private void Update()
    {
        var velX = aIPath.desiredVelocity.x;

        if (Mathf.Abs(velX) > 0.1f)
        {
            sprite.flipX = velX < 0;
        }
    }
}

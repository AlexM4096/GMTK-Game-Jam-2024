using UnityEngine;

public class RandomInitialAnimationTime : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        animator.Play(0, -1, Random.value);
    }
}

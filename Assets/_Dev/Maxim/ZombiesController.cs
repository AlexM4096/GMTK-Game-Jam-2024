using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ZombiesController : MonoBehaviour
{
    [SerializeField]
    private ZombieGroup zombieGroup;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private MainZombie mainZombie;

    [SerializeField]
    private int zombiesRequiredToBeginTransformation = 10;

    [SerializeField]
    private float transformationActiveTime = 5f;

    private bool _isBigZombieActive;
    private bool _isBigZombieTransformationActive = false;
    private Vector3 _targetBigZombieScale = Vector3.one;

    private Coroutine _bigZombieTransformationCoroutine;

    void Start() { }

    void Update()
    {
        if (!_isBigZombieTransformationActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (zombieGroup.ZombieCount < (zombiesRequiredToBeginTransformation - 1))
                return;
            mainZombie.transform.DOKill();
            _bigZombieTransformationCoroutine = StartCoroutine(CallBigZombieAbility());
        }

        if (_isBigZombieTransformationActive && Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(_bigZombieTransformationCoroutine);
            EndBigZombieAbility();
        }

        if (_isBigZombieTransformationActive || _isBigZombieActive)
        {
            mainZombie.transform.localScale = Vector3.Lerp(
                mainZombie.transform.localScale,
                _targetBigZombieScale,
                Time.deltaTime * 3f
            );
        }
    }

    private void EndBigZombieAbility()
    {
        _targetBigZombieScale = Vector3.one;
        _isBigZombieTransformationActive = false;

        mainZombie.Sprite.sortingOrder -= 1;
        mainZombie.transform.DOScale(_targetBigZombieScale, 1f);

        zombieGroup.ShowZombies();
        zombieGroup.MoveAllZombiesToTheirPoints();

        playerController.IsActive = true;
    }

    public IEnumerator CallBigZombieAbility()
    {
        if (_isBigZombieActive)
            yield break;

        _isBigZombieTransformationActive = true;

        mainZombie.Sprite.sortingOrder += 1;
        zombieGroup.MoveAllZombiesToCenter();

        yield return null;
        yield return null;
        yield return null;

        playerController.IsActive = false;

        int reachedEnd = zombieGroup.GetReachedEndOfPath();
        while (reachedEnd < zombieGroup.ZombieCount)
        {
            reachedEnd = zombieGroup.GetReachedEndOfPath();
            _targetBigZombieScale = Vector3.one * Mathf.Min(1 + reachedEnd / 10.0f, 10);
            yield return null;
        }

        zombieGroup.HideZombies();
        yield return null;

        _isBigZombieTransformationActive = false;
        _isBigZombieActive = true;
        playerController.IsActive = true;

        yield return new WaitForSeconds(transformationActiveTime);

        _isBigZombieActive = false;
        EndBigZombieAbility();
    }
}

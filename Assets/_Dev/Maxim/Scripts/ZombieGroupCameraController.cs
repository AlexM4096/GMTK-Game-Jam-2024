using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class ZombieGroupCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private ZombieGroup zombieGroup;

    [SerializeField]
    private BigZombieAbility bigZombieAbility;

    [SerializeField]
    private BigZombieAttack bigZombieAttack;

    [SerializeField]
    private float shakeAmplitude = 6f;

    [SerializeField]
    private float startOrtoSize = 10;

    [SerializeField]
    private float sizePerZombie = 0.2f;

    private float _targetOrtoSize;
    private bool _isZombieBig;

    private void Awake()
    {
        _targetOrtoSize = startOrtoSize;
        virtualCamera.m_Lens.OrthographicSize = startOrtoSize;
        bigZombieAbility.StateChanged += OnStateChanged;
        zombieGroup.ZombieCountChanged += OnZombieCountChanged;
        bigZombieAttack.Attacked += OnBigZombieAttacked;
    }

    private void OnStateChanged(string zombieState)
    {
        _isZombieBig =
            zombieState == BigZombieAbility.BIG_ZOMBIE
            || zombieState == BigZombieAbility.BIG_ZOMBIE_TRANSITION;
        _targetOrtoSize =
            startOrtoSize + zombieGroup.ZombieCount * sizePerZombie * (_isZombieBig ? 4 : 1);
    }

    private void OnBigZombieAttacked()
    {
        DOTween
            .Sequence()
            .AppendCallback(() =>
            {
                virtualCamera
                    .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
                    .m_AmplitudeGain = shakeAmplitude;
            })
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                virtualCamera
                    .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>()
                    .m_AmplitudeGain = 0;
            });
    }

    private void Update()
    {
        virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(
            virtualCamera.m_Lens.OrthographicSize,
            _targetOrtoSize,
            Time.deltaTime
        );
    }

    private void OnZombieCountChanged(int zombieCount)
    {
        _targetOrtoSize = startOrtoSize + zombieCount * sizePerZombie * (_isZombieBig ? 2 : 1);
    }
}

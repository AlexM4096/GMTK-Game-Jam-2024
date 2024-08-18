using UnityEngine;
using UnityHFSM;

public partial class BigZombieAbility
{
    public class BigZombieState : StateBase
    {
        private readonly MainZombie _mainZombie;
        private readonly ZombieGroup _zombieGroup;
        private readonly BigZombieAbilityConfig _abilityConfig;
        private float _timeElapsed;
        private float _abilityDuration;
        private Vector3 _targetBigZombieScale;

        public BigZombieState(
            MainZombie mainZombie,
            ZombieGroup zombieGroup,
            BigZombieAbilityConfig abilityConfig
        )
            : base(needsExitTime: true, isGhostState: false)
        {
            _mainZombie = mainZombie;
            _zombieGroup = zombieGroup;
            _abilityConfig = abilityConfig;
        }

        public override void OnEnter()
        {
            _timeElapsed = 0f;
            _zombieGroup.HideZombies();
            _abilityDuration = Mathf.Min(
                _abilityConfig.StartAbilityDuration
                    + (
                        _zombieGroup.ZombieCount
                        - (_abilityConfig.ZombiesRequiredToBeginTransformation - 1)
                    ) * _abilityConfig.AdditionalAbilityDurationPerZombie,
                _abilityConfig.MaxAbilityDuration
            );
            _targetBigZombieScale =
                Vector3.one
                * Mathf.Min(
                    1 + _zombieGroup.ZombieCount * _abilityConfig.AdditionalScalePerZombie,
                    _abilityConfig.MaxScale
                );
        }

        public override void OnLogic()
        {
            _timeElapsed += Time.deltaTime;

            _mainZombie.transform.localScale = Vector3.Lerp(
                _mainZombie.transform.localScale,
                _targetBigZombieScale,
                Time.deltaTime * 3f
            );

            if (_timeElapsed >= _abilityDuration)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            _zombieGroup.ShowZombies();
        }
    }
}

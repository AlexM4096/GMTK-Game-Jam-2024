using UnityEngine;
using UnityHFSM;

public partial class BigZombieAbility
{
    public class BigZombieState : StateBase
    {
        private readonly Zombie _mainZombie;
        private readonly ZombieGroup _zombieGroup;
        private readonly AbilityStatus _abilityStatus;
        private readonly BigZombieAbilityConfig _abilityConfig;
        private Vector3 _targetBigZombieScale;

        public BigZombieState(
            Zombie mainZombie,
            ZombieGroup zombieGroup,
            AbilityStatus abilityStatus,
            BigZombieAbilityConfig abilityConfig
        )
            : base(needsExitTime: true, isGhostState: false)
        {
            _mainZombie = mainZombie;
            _zombieGroup = zombieGroup;
            _abilityStatus = abilityStatus;
            _abilityConfig = abilityConfig;
        }

        public override void OnEnter()
        {
            _mainZombie.ZombieEater.StopScan();

            _abilityStatus.ActiveRemaining = Mathf.Min(
                _abilityConfig.StartAbilityDuration
                    + (
                        _zombieGroup.ZombieCount
                        - (_abilityConfig.ZombiesRequiredToBeginTransformation - 1)
                    ) * _abilityConfig.AdditionalAbilityDurationPerZombie,
                _abilityConfig.MaxAbilityDuration
            );
            _abilityStatus.IsActive = true;

            _targetBigZombieScale =
                Vector3.one
                * Mathf.Min(
                    1 + _zombieGroup.ZombieCount * _abilityConfig.AdditionalScalePerZombie,
                    _abilityConfig.MaxScale
                );
        }

        public override void OnLogic()
        {
            _mainZombie.transform.localScale = Vector3.Lerp(
                _mainZombie.transform.localScale,
                _targetBigZombieScale,
                Time.deltaTime * 3f
            );

            _abilityStatus.ActiveRemaining -= Time.deltaTime;

            if (_abilityStatus.ActiveRemaining <= 0)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            _mainZombie.ZombieEater.StartScan();
            _abilityStatus.IsActive = false;
        }
    }
}

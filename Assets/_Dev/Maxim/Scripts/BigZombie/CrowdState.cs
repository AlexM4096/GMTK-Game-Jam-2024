using UnityEngine;
using UnityHFSM;

public partial class BigZombieAbility
{
    public class CrowdState : StateBase
    {
        private readonly StateMachine _fsm;
        private readonly MainZombie _mainZombie;
        private readonly BigZombieAbilityConfig _abilityConfig;
        private readonly ZombieGroup _zombieGroup;
        private readonly AbilityStatus _abilityStatus;

        public CrowdState(
            StateMachine fsm,
            MainZombie mainZombie,
            AbilityStatus abilityStatus,
            BigZombieAbilityConfig abilityConfig,
            ZombieGroup zombieGroup
        )
            : base(needsExitTime: true, isGhostState: false)
        {
            _fsm = fsm;
            _mainZombie = mainZombie;
            _zombieGroup = zombieGroup;
            _abilityStatus = abilityStatus;
            _abilityConfig = abilityConfig;
        }

        public override void OnEnter()
        {
            _abilityStatus.CooldownRemaining = _abilityConfig.AbilityCooldown;
            _zombieGroup.MoveAllZombiesToTheirPoints();
        }

        public override void OnLogic()
        {
            if (!_abilityStatus.InCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                if (
                    _zombieGroup.ZombieCount
                    < (_abilityConfig.ZombiesRequiredToBeginTransformation - 1)
                )
                    return;
                _fsm.RequestStateChange(BIG_ZOMBIE_TRANSITION);
                fsm.StateCanExit();
            }

            if (_abilityStatus.InCooldown)
            {
                _abilityStatus.CooldownRemaining -= Time.deltaTime;
                if (_abilityStatus.CooldownRemaining <= 0)
                {
                    _abilityStatus.InCooldown = false;
                }
            }

            _mainZombie.transform.localScale = Vector3.Lerp(
                _mainZombie.transform.localScale,
                Vector3.one,
                Time.deltaTime * 3f
            );
        }

        public override void OnExit() { }
    }
}

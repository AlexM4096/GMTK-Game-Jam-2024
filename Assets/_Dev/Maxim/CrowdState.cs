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

        public bool IsAbilityInCooldown;
        private float _timeElapsed;

        public CrowdState(
            StateMachine fsm,
            MainZombie mainZombie,
            BigZombieAbilityConfig abilityConfig,
            ZombieGroup zombieGroup
        )
            : base(needsExitTime: true, isGhostState: false)
        {
            _fsm = fsm;
            _mainZombie = mainZombie;
            _zombieGroup = zombieGroup;
            _abilityConfig = abilityConfig;
        }

        public override void OnEnter()
        {
            _timeElapsed = 0f;
            _zombieGroup.MoveAllZombiesToTheirPoints();
        }

        public override void OnLogic()
        {
            if (!IsAbilityInCooldown && Input.GetKeyDown(KeyCode.Space))
            {
                if (
                    _zombieGroup.ZombieCount
                    < (_abilityConfig.ZombiesRequiredToBeginTransformation - 1)
                )
                    return;
                _fsm.RequestStateChange(BIG_ZOMBIE_TRANSITION);
                fsm.StateCanExit();
            }

            if (IsAbilityInCooldown)
            {
                _timeElapsed += Time.deltaTime;
                if (_timeElapsed >= _abilityConfig.AbilityCooldown)
                {
                    IsAbilityInCooldown = false;
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

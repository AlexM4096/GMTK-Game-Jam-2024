using UnityEngine;
using UnityHFSM;

public partial class BigZombieAbility
{
    public class BigZombieTransitionState : StateBase
    {
        private readonly ZombieGroup _zombieGroup;
        private readonly PlayerController _playerController;
        private readonly BigZombieAbilityConfig _abilityConfig;
        private readonly Zombie _mainZombie;
        private readonly StateMachine _fsm;

        public BigZombieTransitionState(
            StateMachine fsm,
            Zombie mainZombie,
            ZombieGroup zombieGroup,
            PlayerController playerController,
            BigZombieAbilityConfig abilityConfig
        )
            : base(needsExitTime: true, isGhostState: false)
        {
            _fsm = fsm;
            _mainZombie = mainZombie;
            _zombieGroup = zombieGroup;
            _playerController = playerController;
            _abilityConfig = abilityConfig;
        }

        public override void OnEnter()
        {
            _mainZombie.Sprite.sortingOrder += 1;
            _zombieGroup.MoveAllZombiesToBigZombie();
            _playerController.IsActive = false;
        }

        public override void OnLogic()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _fsm.RequestStateChange(CROWD);
                fsm.StateCanExit();
            }

            var reachedZombies = _zombieGroup.GetReachedEndOfPath();
            var isAllZombiesReached = reachedZombies == _zombieGroup.ZombieCount;

            var targetBigZombieScale =
                Vector3.one
                * Mathf.Min(
                    1 + reachedZombies * _abilityConfig.AdditionalScalePerZombie,
                    _abilityConfig.MaxScale
                );

            _mainZombie.transform.localScale = Vector3.Lerp(
                _mainZombie.transform.localScale,
                targetBigZombieScale,
                Time.deltaTime * 3f
            );

            if (
                isAllZombiesReached
                && Vector3.Distance(_mainZombie.transform.localScale, targetBigZombieScale) < 0.5f
            )
            {
                _fsm.RequestStateChange(BIG_ZOMBIE);
                fsm.StateCanExit();
            }
        }

        public override void OnExit()
        {
            _mainZombie.Sprite.sortingOrder -= 1;
            _playerController.IsActive = true;
        }
    }
}

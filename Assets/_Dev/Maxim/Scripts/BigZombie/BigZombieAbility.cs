using UnityEngine;
using UnityHFSM;

public partial class BigZombieAbility : MonoBehaviour
{
    [SerializeField]
    private ZombieGroup zombieGroup;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Zombie mainZombie;

    [SerializeField]
    private SpriteRenderer bigZombieSprite;

    [SerializeField]
    private BigZombieAttack bigZombieAttack;

    [SerializeField]
    private BigZombieAbilityConfig abilityConfig;

    public event System.Action<string> StateChanged;

    private StateMachine _fsm;
    public AbilityStatus AbilityStatus { get; private set; } = new();
    public BigZombieAbilityConfig AbilityConfig => abilityConfig;

    public const string CROWD = "Crowd";
    public const string BIG_ZOMBIE = "BigZombie";
    public const string BIG_ZOMBIE_TRANSITION = "BigZombieTransition";

    void Start()
    {
        _fsm = new StateMachine();

        var crowdState = new CrowdState(
            _fsm,
            mainZombie,
            AbilityStatus,
            abilityConfig,
            zombieGroup
        );

        _fsm.AddState(CROWD, crowdState);
        _fsm.AddState(
            BIG_ZOMBIE_TRANSITION,
            new BigZombieTransitionState(
                _fsm,
                mainZombie,
                zombieGroup,
                bigZombieSprite,
                playerController,
                abilityConfig
            )
        );
        _fsm.AddState(
            BIG_ZOMBIE,
            new BigZombieState(
                mainZombie,
                zombieGroup,
                bigZombieAttack,
                bigZombieSprite,
                AbilityStatus,
                abilityConfig
            )
        );

        _fsm.AddTransition(CROWD, BIG_ZOMBIE_TRANSITION);
        _fsm.AddTransition(BIG_ZOMBIE_TRANSITION, CROWD);
        _fsm.AddTransition(BIG_ZOMBIE_TRANSITION, BIG_ZOMBIE);
        _fsm.AddTransition(
            BIG_ZOMBIE,
            CROWD,
            afterTransition: (s) => AbilityStatus.InCooldown = true
        );

        _fsm.SetStartState(CROWD);

        _fsm.Init();

        _fsm.StateChanged += (s) =>
        {
            StateChanged?.Invoke(_fsm.ActiveStateName);
            Debug.Log(_fsm.GetActiveHierarchyPath());
        };
    }

    void Update()
    {
        _fsm.OnLogic();
    }

    [System.Serializable]
    public class BigZombieAbilityConfig
    {
        [Header("Big Zombie Parameters")]
        [Header("Scale Parameters")]
        public int ZombiesRequiredToBeginTransformation = 10;

        public float AdditionalScalePerZombie = 0.1f;

        public float MaxScale = 10f;

        [Header("Ability Duration")]
        public float StartAbilityDuration = 3f;

        public float AdditionalAbilityDurationPerZombie = 0.1f;

        public float MaxAbilityDuration = 10f;

        public float AbilityCooldown = 5f;
    }
}

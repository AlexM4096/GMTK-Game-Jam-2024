using System;
using Alex;
using Pathfinding;
using UnityEngine;
using UnityHFSM;

public class Zombie : MonoBehaviour
{
    [field: SerializeField]
    public Transform Target { get; set; }

    [field: SerializeField, Header("Components")]
    public SpriteRenderer Sprite { get; private set; }

    [field: SerializeField]
    public Animator Animator { get; private set; }

    [field: SerializeField]
    public AIPath AiPath { get; private set; }

    [field: SerializeField]
    public ZombieEater ZombieEater { get; private set; }

    [field: SerializeField]
    public Health Health { get; private set; }

    private StateMachine _fsm;

    private const string IDLE = "idle";
    private const string WALK = "walk";
    private const string ATTACK = "attack";
    private const string BIG_ZOMBIE_TRANSITION = "big_zombie_transition";

    private void Awake()
    {
        _fsm = new StateMachine();

        _fsm.AddState(
            IDLE,
            onEnter: (s) =>
            {
                Animator.Play(IDLE);
            }
        );

        _fsm.AddState(
            WALK,
            onEnter: (s) =>
            {
                Animator.Play(WALK);
            }
        );

        _fsm.AddState(
            ATTACK,
            onEnter: (s) =>
            {
                AiPath.isStopped = true;
                Animator.Play(ATTACK);
            },
            onExit: (s) =>
            {
                AiPath.isStopped = false;
            },
            needsExitTime: true
        );

        _fsm.AddState(BIG_ZOMBIE_TRANSITION, new BigZombieTransition(this));

        _fsm.SetStartState(IDLE);

        _fsm.AddTwoWayTransition(IDLE, WALK, (s) => AiPath.velocity.magnitude > 0.01f);

        _fsm.AddTransition(ATTACK, IDLE);

        _fsm.AddTriggerTransitionFromAny("go_to_big_zombie", BIG_ZOMBIE_TRANSITION);
        _fsm.AddTriggerTransition("go_from_big_zombie", BIG_ZOMBIE_TRANSITION, IDLE);

        _fsm.Init();
    }

    private void OnEnable()
    {
        if (AiPath != null)
        {
            AiPath.onSearchPath += UpdateDestination;
        }
    }

    private void OnDisable()
    {
        if (AiPath != null)
        {
            AiPath.onSearchPath -= UpdateDestination;
        }
    }

    private void Update()
    {
        UpdateDestination();
        _fsm.OnLogic();
    }

    private void UpdateDestination()
    {
        if (AiPath != null && Target != null)
        {
            AiPath.destination = Target.position;
        }
    }

    public void ChangeAnimator(Animator newAnimator)
    {
        Animator = newAnimator;
    }

    public void GoToBigZombie()
    {
        _fsm.Trigger("go_to_big_zombie");
    }

    public void GoFromBigZombie()
    {
        _fsm.Trigger("go_from_big_zombie");
    }

    public void StartAttacking()
    {
        _fsm.RequestStateChange(ATTACK);
    }

    public bool IsAttacking => _fsm.ActiveStateName == ATTACK;

    public void StopAttacking()
    {
        _fsm.RequestStateChange(IDLE, forceInstantly: true);
    }

    public class BigZombieTransition : StateBase
    {
        private readonly Zombie _zombie;
        private float _timeElapsed;
        private bool _isHidden;

        public BigZombieTransition(Zombie zombie)
            : base(needsExitTime: true, isGhostState: false)
        {
            _zombie = zombie;
        }

        public override void OnEnter()
        {
            _isHidden = false;
            _timeElapsed = 0f;
            _zombie.ZombieEater.StopScan();
        }

        public override void OnLogic()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed > 0.1f && !_isHidden && _zombie.AiPath.remainingDistance <= 0.5f)
            {
                _isHidden = true;
                _zombie.Sprite.gameObject.SetActive(false);
            }
        }

        public override void OnExit()
        {
            _zombie.Sprite.gameObject.SetActive(true);
            _zombie.ZombieEater.StartScan();
        }

        public override void OnExitRequest()
        {
            fsm.StateCanExit();
        }
    }
}

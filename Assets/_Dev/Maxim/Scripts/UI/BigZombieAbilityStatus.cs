using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigZombieAbilityStatus : MonoBehaviour
{
    [SerializeField]
    private BigZombieAbility bigZombieAbility;

    [SerializeField]
    private Image cooldownFillBar;

    [SerializeField]
    private Image activeFillBar;

    [SerializeField]
    private TextMeshProUGUI statusLabel;

    [SerializeField]
    private string inCooldownStatus = "Cooldown...";

    [SerializeField]
    private string isActiveStatus = "Active...";

    [SerializeField]
    private string notInCooldownStatus = "BIG ZOMBIEE!";

    private AbilityStatus AbilityStatus => bigZombieAbility.AbilityStatus;

    private void Start()
    {
        if (bigZombieAbility == null)
        {
            bigZombieAbility = FindFirstObjectByType<BigZombieAbility>();
            if (bigZombieAbility == null)
            {
                throw new System.Exception("Can't find BigZombieAbility");
            }
        }

        AbilityStatus.OnCooldownChanged += OnCooldownChanged;
        AbilityStatus.OnActiveChanged += OnActiveChanged;
        OnCooldownChanged();
    }

    private void OnDestroy()
    {
        if (bigZombieAbility != null)
        {
            AbilityStatus.OnCooldownChanged -= OnCooldownChanged;
            AbilityStatus.OnActiveChanged -= OnActiveChanged;
        }
    }

    private void OnCooldownChanged()
    {
        cooldownFillBar.DOKill();
        if (AbilityStatus.InCooldown)
        {
            activeFillBar.fillAmount = 0;
            cooldownFillBar
                .DOFillAmount(0, AbilityStatus.CooldownRemaining)
                .SetEase(Ease.Linear)
                .From(1);
            statusLabel.text = inCooldownStatus;
        }
        else
        {
            cooldownFillBar.fillAmount = 0;
            statusLabel.text = notInCooldownStatus;
        }
    }

    private void OnActiveChanged()
    {
        if (AbilityStatus.IsActive)
        {
            Debug.Log(AbilityStatus.ActiveRemaining);
            cooldownFillBar.fillAmount = 0;
            activeFillBar.DOKill();
            activeFillBar
                .DOFillAmount(0, AbilityStatus.ActiveRemaining)
                .SetEase(Ease.Linear)
                .From(1);
        }
    }
}

using UnityEngine;

public class AbilityStatus
{
    private bool _inCooldown;
    public bool InCooldown
    {
        get => _inCooldown;
        set
        {
            _inCooldown = value;
            OnCooldownChanged?.Invoke();
        }
    }

    private bool _isActive;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnActiveChanged?.Invoke();
        }
    }

    private float _cooldownRemaining;
    public float CooldownRemaining
    {
        get => _cooldownRemaining;
        set { _cooldownRemaining = Mathf.Max(value, 0); }
    }

    private float _activeRemaining;
    public float ActiveRemaining
    {
        get => _activeRemaining;
        set { _activeRemaining = Mathf.Max(value, 0); }
    }

    public event System.Action OnCooldownChanged;
    public event System.Action OnActiveChanged;
}

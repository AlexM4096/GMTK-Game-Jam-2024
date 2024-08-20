using Alex;
using JSAM;
using UnityEngine;
using UnityEngine.Events;

public class HouseBreak : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth = 10;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private UnityEvent onHouseDestroyed;

    private Material _material;
    private float _health;
    private bool _isDestroyed;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
        _health = maxHealth;
    }

    public void TakeDamage(float amount, IAttackable source)
    {
        if (_isDestroyed)
            return;
        if (_health > 0 && amount > 0)
        {
            AudioManager.PlaySound(AudioLibrarySounds.RockDestoy);
        }
        _health = Mathf.Max(_health - amount, 0);
        if (_health == 0)
        {
            _isDestroyed = true;
            AudioManager.PlaySound(AudioLibrarySounds.HouseDestroy);
            onHouseDestroyed?.Invoke();
        }
        _material.SetFloat("_Damage", 1f - _health / maxHealth);
    }
}

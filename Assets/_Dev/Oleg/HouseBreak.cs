using Alex;
using JSAM;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HouseBreak : MonoBehaviour, IDamageable
{
    [SerializeReference]
    private List<DamageCriteria> criterias;

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
        if (criterias != null && criterias.Count > 0)
        {
            if (source is not MonoBehaviour monoBehaviour)
                return;

            if (!criterias.Any(x => x.CanTakeDamageFrom(monoBehaviour)))
                return;
        }

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
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        _material.SetFloat("_Damage", 1f - _health / maxHealth);
    }
}

using System.Collections;
using System.Collections.Generic;
using Alex;
using UnityEngine;

public class DamageParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Health _health;

    private void Start()
    {
        _health = GetComponentInParent<Health>();
        _health.DeathEvent += OnDamage;
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnDamage()
    {
        
        gameObject.transform.parent = null;
        _particleSystem.Play();
    }
}

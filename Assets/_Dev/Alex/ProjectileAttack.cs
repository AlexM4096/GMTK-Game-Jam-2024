using Alex;
using System.Collections;
using UnityEngine;

namespace Assets._Dev.Alex
{
    public class ProjectileAttack : MonoBehaviour, IAttackable
    {
        [SerializeField] private ProjectileSettings settings;

        public bool CanAttack { get; set; } = true;
        public float Damage { get; set; }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseBreak : MonoBehaviour
{
    [SerializeField] private float _damgage;
    private Material _material;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        _material.SetFloat("_Damage", _damgage);
    }
}

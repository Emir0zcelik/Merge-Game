using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    [Header("Health")]
    [SerializeField] int maxHealth = 3;
    private int _currentHealth = 0;
    public int CurrentHealth { get; set; }

    private void Start()
    {
        _currentHealth = maxHealth;
    }


    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        EnemyTakeDamage();

        if (_currentHealth <= 0)
        {
            EnemyDeath();
        }
    }
    public void EnemyTakeDamage()
    {
        _currentHealth--;
    }
    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}

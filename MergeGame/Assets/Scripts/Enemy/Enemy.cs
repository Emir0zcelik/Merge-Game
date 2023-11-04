using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    public float Speed { get { return speed; } set { speed = value; } }

    [Header("Health")]
    [SerializeField] int _health = 3;




    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        EnemyHurt();

        if (_health <= 0)
        {
            EnemyDeath();
        }
    }
    void EnemyHurt()
    {

    }
    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}

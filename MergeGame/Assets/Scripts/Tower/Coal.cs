using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;



    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

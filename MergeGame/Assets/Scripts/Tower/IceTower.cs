using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Speed = 1f;
        }
    }
}

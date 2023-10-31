using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;


    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }



    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

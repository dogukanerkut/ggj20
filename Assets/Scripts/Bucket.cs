using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public GameObject waterSplash;
    public bool willExplode;

    private void OnCollisionEnter(Collision collision)
    {
        if(willExplode)
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public GameObject waterSplash;
    public bool willExplode;

    private void OnCollisionEnter(Collision col)
    {
        if(willExplode && !col.gameObject.CompareTag("Player"))
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);

            Collider[] cols = Physics.OverlapSphere(transform.position, 1.9f);
            foreach(Collider collider in cols)
            {
                if(collider.CompareTag("Ballista"))
                {
                    collider.GetComponent<Ballista>().ExtnguishFire();
                }
            }

            Destroy(gameObject);
        }
    }
}

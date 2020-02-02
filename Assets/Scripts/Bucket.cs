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
            Destroy(gameObject);

            RaycastHit hit;
            if(Physics.SphereCast(transform.position, 1.25f ,Vector3.forward, out hit, 0.02f))
            {
                if(hit.transform.CompareTag("Ballista"))
                {
                    hit.transform.GetComponent<Ballista>().ExtnguishFire();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private Transform target;
    private bool inHand;
    private bool destroy;
    private Rigidbody rb;
    private Collider col;
    private Container container;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Pickup(Transform target)
    {
        this.target = target;
    }

    public void Throw(Container container)
    {
        target = container.transform;
        inHand = false;
        this.container = container;
        destroy = true;
    }

    public void Throw(Vector3 force)
    {
        target = null;
        inHand = false;
        rb.freezeRotation = false;
        transform.SetParent(null);
        rb.velocity = Vector3.zero;
        //rb.AddForce(force * 10f);
    }

    private void Update()
    {
        if (inHand)
        {
            transform.position = target.position;
            return;
        }
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.position, target.position) < 1f)
            {
                if (destroy)
                {
                    Destroy(gameObject);
                    container.AddItem();
                }
                else
                {
                    inHand = true;
                    rb.freezeRotation = true;
                    col.enabled = false;
                }
            }
        }
    }

}

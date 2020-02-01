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
        col.enabled = false;
        rb.isKinematic = true;
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
        col.enabled = true;
        rb.isKinematic = false;
        rb.AddForce(force * 100);

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
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * 50);
            if (Vector3.Distance(transform.position, target.position) < 0.2f)
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
                }
            }
        }
    }

}

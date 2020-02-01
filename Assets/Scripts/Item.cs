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
    [SerializeField] private ItemType _itemType;
    public ItemType ItemType { get => _itemType; }
    private Quaternion _targetQuaternion;
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
        transform.SetParent(container.transform);
        target = container.GetTarget();
        _targetQuaternion = target.transform.localRotation;
        inHand = false;
        this.container = container;
        destroy = true;

    }

    public void Throw(Vector3 force)
    {
        _targetQuaternion = Quaternion.identity;
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
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 10f);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetQuaternion, Time.deltaTime * 50);
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
public enum ItemType
{
    Wood,
    Bucket,
    Stone,
    Arrow
}

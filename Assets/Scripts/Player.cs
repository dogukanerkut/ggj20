﻿using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    public PlayerMovement movement;
    public Transform pickupPoint;
    public float pickupLerpSpeed;
    public Transform characterParent;
    public float pickupRange;
    public float freeThrowForward;
    public float freeThrowUpward;

    private Item item;
    private bool holdingItem;

    private void Update()
    {
        bool interact = movement.joystick ? Input.GetKeyDown(KeyCode.JoystickButton1) :
            Input.GetKeyDown(KeyCode.E);
        if (interact)
        {
            if (holdingItem)
            {
                Throw();
            }
            else
            {
                Pickup();
            }
        }
        bool attack = movement.joystick ? Input.GetKeyDown(KeyCode.JoystickButton0) :
            Input.GetKeyDown(KeyCode.Q);
        if (attack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position - characterParent.forward, 1f, characterParent.forward, out hit,
            2.5f, LayerMask.GetMask("Interactable"));
        if (hit.transform != null)
        {
            if (hit.transform.tag == "Spawner")
            {
                hit.transform.GetComponent<ObjectSpawner>().Hit();
            }
        }
        animator.SetTrigger("Hit");
    }

    public void Throw()
    {
        RaycastHit hit;
        Physics.SphereCast(transform.position, 0.5f, characterParent.forward, out hit,
            8f, LayerMask.GetMask("Container"));
        bool canThrowToContainer = false;
        Container container = null;
        if (hit.transform != null)
        {
            container = hit.transform.GetComponent<Container>();
            if (container.IsContainerOf(item.ItemType))
            {
                canThrowToContainer = true;
            }
        }
        if (canThrowToContainer && container.IsCapacityAllowed())
        {
            item.Throw(container);
        }
        else
        {
            item.Throw(characterParent.transform.forward * freeThrowForward +
                           characterParent.transform.up * freeThrowUpward);

        }

        holdingItem = false;
        animator.SetBool("Pickup", false);
        animator.SetTrigger("Throw");
    }

    public void Pickup()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + characterParent.forward, pickupRange,
            LayerMask.GetMask("Item"));
        IOrderedEnumerable<Collider> orderedHits = hits.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
        if (hits.Count() > 0)
        {
            Item item = orderedHits.ElementAtOrDefault(0).GetComponent<Item>();
            this.item = item;
            item.Pickup(pickupPoint);
            holdingItem = true;
            animator.SetBool("Pickup", true);
            item.transform.SetParent(pickupPoint);
        }
    }

}

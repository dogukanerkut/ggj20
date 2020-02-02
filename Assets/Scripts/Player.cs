using System.Linq;
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
    public float attackCooldown = .5f;
    public MeshRenderer hatMeshRenderer;

    private Item item;
    private bool holdingItem;

    private void Update()
    {
        if (movement.inputHandle.GetInteractInput(movement.playerNo))
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
        if (movement.inputHandle.GetAttackInput(movement.playerNo))
        {
            StartAttackAnimation();
        }

    }
    private bool _canAttack = true;
    public void SetCanAttack(bool state)
    {
        Attack();
        _canAttack = true;
    }
    public void StartAttackAnimation()
    {
        if (!_canAttack)
        {
            return;
        }
        if (holdingItem)
        {
            return;
        }
        animator.SetTrigger("Hit");
        _canAttack = false;
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
            if (hit.transform.tag == "Wall")
            {
                if (hit.transform.GetComponent<WallHandler>().AttemptToRepair())
                {
                    //hit is successfull, you can spawn particle fx here
                }
            }
        }
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

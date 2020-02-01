using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    public PlayerMovement movement;
    public Collider weaponCollider;
    public AnimationClip hitClip;

    private void Update()
    {
        bool attack = movement.joystick ? Input.GetKeyDown(KeyCode.JoystickButton0) :
            Input.GetMouseButtonDown(0);
        if (attack)
        {
            weaponCollider.enabled = true;
            animator.SetTrigger("Hit");
            StartCoroutine(HitAnimEnd());
        }
    }

    private IEnumerator HitAnimEnd()
    {
        yield return new WaitForSeconds(hitClip.length / animator.speed);
        weaponCollider.enabled = false;
    } 

}

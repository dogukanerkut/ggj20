using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool joystick;
    public float speed;
    public Transform character;
    public float rotationLerpSpeed;

    private Rigidbody playerRB;
    private Vector3 lookAtTarget;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovementInput();
        LookAt();
    }

    public void MovementInput()
    {
        float horizontal = joystick ? Input.GetAxis("Horizontal1") : Input.GetAxis("Horizontal");
        float vertical = joystick ? Input.GetAxis("Vertical1") : Input.GetAxis("Vertical");
        Vector3 moveVector = Vector3.zero;
        if (horizontal < 0f) moveVector -= transform.right;
        if (horizontal > 0f) moveVector += transform.right;
        if (vertical < 0f) moveVector -= transform.forward;
        if (vertical > 0f) moveVector += transform.forward;
        lookAtTarget = transform.position + moveVector;
        moveVector = moveVector * speed * Time.deltaTime;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
    }

    private void LookAt()
    {
        Vector3 target = Vector3.Lerp(character.position + character.forward, lookAtTarget,
            Time.deltaTime * rotationLerpSpeed);
        character.LookAt(target);
    }

}

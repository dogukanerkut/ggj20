using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool joystick;
    public float speed;
    public Transform _characterParent;
    public Transform _character;
    public float rotationLerpSpeed;

    private Rigidbody playerRB;
    private Quaternion lookRotation;
    private Vector3 _inputDir;
    [SerializeField] private Vector3 axis;
    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }
    // private void Update()
    // {
    //     RaycastHit hitInfo = new RaycastHit();
    //     Physics.Raycast(transform.position, Vector3.down, out hitInfo, 10);
    //     if (hitInfo.collider != null)
    //     {
    //     //    Quaternion lookAt = Quaternion.LookRotation((hitInfo.normal).normalized, axis);
           
    //     //    _character.localRotation = Quaternion.Euler(lookAt.eulerAngles.x - 90, lookAt.eulerAngles.y, lookAt.eulerAngles.z);
    //     Debug.Log(hitInfo.collider.gameObject.name);
    //     Debug.Log(hitInfo.normal);
    //         _character.up = Vector3.Lerp(_character.up, hitInfo.normal, Time.deltaTime * 8f);

    //     }
    // }
    private void FixedUpdate()
    {
        float horizontal = joystick ? Input.GetAxis("Horizontal1") : Input.GetAxisRaw("Horizontal");
        float vertical = joystick ? Input.GetAxis("Vertical1") : Input.GetAxisRaw("Vertical");
        _inputDir = new Vector3(horizontal, 0, vertical).normalized;
        if (!IsReceivingInput())
        {
            playerRB.isKinematic = true;
            return;
        }
        playerRB.isKinematic = false;
        MovementInput();
        LookAt();
    }

    public void MovementInput()
    {
        Vector3 moveVector = _inputDir * speed * Time.fixedDeltaTime;
        playerRB.velocity = new Vector3(moveVector.x, playerRB.velocity.y, moveVector.z);
    }
    private bool IsReceivingInput()
    {
        return _inputDir.magnitude > 0;
    }

    private void LookAt()
    {
        lookRotation = Quaternion.LookRotation(_inputDir, Vector3.up);
        _characterParent.transform.rotation = Quaternion.Slerp(_characterParent.transform.rotation, lookRotation, rotationLerpSpeed * Time.fixedDeltaTime);
    }

}

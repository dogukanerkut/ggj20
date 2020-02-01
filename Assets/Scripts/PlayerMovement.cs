using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool joystick;
    public bool joystick2;
    public float speed;
    public Transform _characterParent;
    public float rotationLerpSpeed;

    private Rigidbody playerRB;
    private Quaternion lookRotation;
    private Vector3 _inputDir;
    [SerializeField] private PhysicMaterial _standingStillMaterial;
    [SerializeField] private PhysicMaterial _movingMaterial;
    [SerializeField] private Animator _animator;
    [SerializeField] private int param_Walking = Animator.StringToHash("Walking");
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        playerRB = GetComponent<Rigidbody>();
    }
    private RaycastHit _hitInfo;
    private void LateUpdate()
    {
        Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 10);

    }
    private void FixedUpdate()
    {
        float horizontal = joystick ? Input.GetAxis("Horizontal1") :
            joystick2 ? Input.GetAxis("Horizontal2") : Input.GetAxisRaw("Horizontal");
        float vertical = joystick ? Input.GetAxis("Vertical1") : 
            joystick2 ? Input.GetAxis("Vertical2") : Input.GetAxisRaw("Vertical");
        _inputDir = new Vector3(horizontal, 0, vertical).normalized;
        if (!IsReceivingInput())
        {
            _collider.material = _standingStillMaterial;
            _animator.SetBool(param_Walking, false);
            // playerRB.isKinematic = true;
            // playerRB.MovePosition(transform.position + (Vector3.down * Time.fixedDeltaTime));
            // playerRB.velocity = new Vector3(0, Physics.gravity.y, 0);
            return;
        }
        if (!_animator.GetBool(param_Walking))
        {
            _animator.SetBool(param_Walking, true);

        }
        _collider.material = _movingMaterial;
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
        Quaternion lastRotation = Quaternion.LookRotation(_inputDir, Vector3.up);
        if (_hitInfo.collider != null)
        {
            lastRotation *= Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
            lastRotation = Quaternion.Euler(lastRotation.eulerAngles.x * -1, lastRotation.eulerAngles.y, lastRotation.eulerAngles.z);
        }
        _characterParent.transform.rotation = Quaternion.Slerp(_characterParent.transform.rotation, lastRotation, rotationLerpSpeed * Time.fixedDeltaTime);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCameraHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private Vector3 _offset;
    private Vector3 velocity;
    [SerializeField] private float _smoothTime = 0.5f;
    [SerializeField] private float _minFOV;
    [SerializeField] private float _maxFOV;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _fovSpeed;


    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();
        Zoom();
    }
    private void Move()
    {
        Vector3 targetPosition = GetCenterPosition() + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
    }
    private void Zoom()
    {
        float targetFOV = Mathf.Lerp(_maxFOV, _minFOV, GetFarthestDistance() / _maxDistance);
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFOV, Time.deltaTime * _fovSpeed);
    }

    private float GetFarthestDistance()
    {
        Bounds bounds = new Bounds(_targets[0].position, Vector3.zero);
        for (int i = 0; i < _targets.Count; i++)
        {
            bounds.Encapsulate(_targets[i].position);
        }
        return bounds.size.x + (bounds.size.z * 1.66f);
    }
    
    private Vector3 GetCenterPosition()
    {
        Bounds bounds = new Bounds(_targets[0].position, Vector3.zero);
        for (int i = 0; i < _targets.Count; i++)
        {
            bounds.Encapsulate(_targets[i].position);
        }
        return bounds.center;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _targetBallista;
    [SerializeField] private GameObject _ballistaPrefab;
    [SerializeField] private float _distanceFromWall;
    [SerializeField] private AutoProjectile _projectilePrefab;
    [SerializeField] private AutoProjectile _fireArrowPrefab;
    [SerializeField] private float _initialAngle;
    private GameObject _instantiatedBallista;

    void Start()
    {
        _instantiatedBallista = Instantiate(_ballistaPrefab);
        _instantiatedBallista.transform.position = -_target.transform.right * _distanceFromWall;
        _instantiatedBallista.transform.LookAt(_target.position - _instantiatedBallista.transform.position, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Attack(false);
        //     Attack(true);
        // }
    }

    public void Attack(bool fireArrow)
    {
        if(!_targetBallista)
            fireArrow = false;
        if(!fireArrow || !_targetBallista.gameObject.activeInHierarchy)
        {
            AutoProjectile projectile = Instantiate(_projectilePrefab);
            projectile.transform.position = _instantiatedBallista.transform.position;
            projectile.Fire(_target, _initialAngle);
        }
        else
        {
            AutoProjectile projectile = Instantiate(_fireArrowPrefab);
            projectile.transform.position = _instantiatedBallista.transform.position;
            projectile.Fire(_targetBallista, _initialAngle * 2);
        }
    }
}

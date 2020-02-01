using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Attacker> _attackers;
    [SerializeField] private AttackData _data;
    private void Awake()
    {
        StartCoroutine(DelayedAttack(_data.DelayBeforeFirstAttack));
    }
    private IEnumerator DelayedAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        _attackers[Random.Range(0, _attackers.Count)].Attack();
        StartCoroutine(DelayedAttack(_data.AttackFrequency));
    }
}

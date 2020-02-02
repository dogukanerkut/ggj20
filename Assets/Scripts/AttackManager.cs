using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Attacker> _attackers;
    [SerializeField] private AttackData _data;

    private int currentAttackCount;

    private void Awake()
    {
        StartCoroutine(DelayedAttack(_data.DelayBeforeFirstAttack));
    }
    private IEnumerator DelayedAttack(float delay)
    {
        float rand = Random.value;
        bool firingArrow = rand < _data.ChanceToBurnBallista;

        if(firingArrow)
            delay /= 2;
        else
            currentAttackCount++;

        if(currentAttackCount > _data.hitCountBeforeDiffIncrease)
        {
            delay -= _data.delayDecrease;
            currentAttackCount = 0;
        }

        yield return new WaitForSeconds(delay);
        _attackers[Random.Range(0, _attackers.Count)].Attack(firingArrow);
        StartCoroutine(DelayedAttack(_data.AttackFrequency));
    }
}

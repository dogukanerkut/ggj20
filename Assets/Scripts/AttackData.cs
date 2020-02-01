﻿using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "AttackData")]
public class AttackData : ScriptableObject
{
    public float DelayBeforeFirstAttack;
    public float AttackFrequency;
    [Range(0, 1)]
    public float ChanceToBurnBallista;
}

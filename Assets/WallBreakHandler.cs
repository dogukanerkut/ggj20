﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class WallBreakHandler : MonoBehaviour
{
    [SerializeField] private List<Breakable> _breakStages = new List<Breakable>();
    private int _currentBreakIndex;
    public float CurrentBreakPercentage { get; private set; }
    private CinemachineImpulseSource _impulse;
    private void Awake()
    {
        _currentBreakIndex = 0;
        EnableSubObject(_breakStages[_currentBreakIndex]);
        _impulse = GetComponent<CinemachineImpulseSource>();
    }
    public void ReceiveHit()
    {
        if (++_currentBreakIndex > _breakStages.Count -1)
        {
            return;
        }
        CurrentBreakPercentage = _currentBreakIndex / ((float)_breakStages.Count-1);
        Debug.Log(CurrentBreakPercentage);
        if (_currentBreakIndex == _breakStages.Count -1)
        {
            Debug.Log("Game Over");
            EventManager.EventGameOver.Invoke();
        }
        else
        {
            EventManager.EventWallBreak.Invoke(this);
        }
        EnableSubObject(_breakStages[_currentBreakIndex]);
    }

    private void EnableSubObject(Breakable breakable)
    {
        _breakStages.ForEach(b =>
        {
            
            if (breakable == b)
            {
                breakable.Object.SetActive(true);
                if (breakable.InitialEffect != null)
                {
                    breakable.InitialEffect.Play(true);
                }
                if (breakable.ContinousEffect != null)
                {
                    breakable.ContinousEffect.Play(true);
                }

            }
            else
            {
                b.Object.SetActive(false);
                if (b.InitialEffect != null)
                {
                    b.InitialEffect.Stop(true);
                }
                if (b.ContinousEffect != null)
                {
                    b.ContinousEffect.Stop(true);
                }
            }
        });
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_impulse != null)
            {
            _impulse.GenerateImpulseAt(transform.position, new Vector3(5,5,5));
                
            }
        }
    }

}
[System.Serializable]
public class Breakable
{
    public GameObject Object;
    public ParticleSystem InitialEffect;
    public ParticleSystem ContinousEffect;
}

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
    public bool IsWallSolid { get => _currentBreakIndex == 0; }
    public void ReceiveHit()
    {
        if (++_currentBreakIndex > _breakStages.Count - 1)
        {
            return;
        }
        CurrentBreakPercentage = _currentBreakIndex / ((float)_breakStages.Count - 1);
        //Debug.Log(CurrentBreakPercentage);
        if (_currentBreakIndex == _breakStages.Count - 1)
        {
            TimeControl.instance.SetTimeScale(.01f);
            GameControl.instance.GameOver();
            Debug.Log("Game Over");
            EventManager.EventGameOver.Invoke();
        }
        else
        {
            EventManager.EventWallBreak.Invoke(this);
        }
        EnableSubObject(_breakStages[_currentBreakIndex], false);
    }

    private void EnableSubObject(Breakable breakable, bool isRepair = true, bool isRestored = false)
    {
        if (breakable.Object == null)
        {
            _breakStages.ForEach(b =>
            {
                if (b.Object != null)
                {
                    b.Object.SetActive(false);
                }
            });
            return;
        }
        _breakStages.ForEach(b =>
        {
            if (breakable == b)
            {
                if (breakable.Object != null)
                {
                    breakable.Object.SetActive(true);
                    var handler = breakable.Object.GetComponent<WallHandler>();
                    if (!isRepair)
                    {
                        if (handler != null)
                        {
                            handler.ShineBabyShine();
                        }
                    }
                    if (isRestored)
                    {
                        if (handler != null)
                        {
                            handler.RestoreFX();
                        }
                    }
                }
                if (breakable.InitialEffect != null && !isRestored)
                {
                    breakable.InitialEffect.Play(true);
                }
                if (breakable.ContinousEffect != null && !isRestored)
                {
                    breakable.ContinousEffect.Play(true);
                }

            }
            else
            {
                if (b.Object != null)
                {
                    b.Object.SetActive(false);
                }
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
    public void Repair()
    {
        if (_currentBreakIndex - 1 < 0)
        {
            return;
        }
        _currentBreakIndex--;
        EnableSubObject(_breakStages[_currentBreakIndex], true, true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_impulse != null)
            {
                _impulse.GenerateImpulseAt(transform.position, new Vector3(5, 5, 5));

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

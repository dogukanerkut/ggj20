using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEffect : MonoBehaviour
{
    public GameObject toSwitch;

    public void ActivateFX(float timer)
    {
        toSwitch.SetActive(false);
        StartCoroutine(TimerForSpawn(timer));
    }

    private IEnumerator TimerForSpawn(float time)
    {
        yield return new WaitForSeconds(time);
        toSwitch.SetActive(true);
    }
}

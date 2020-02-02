using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public static TimeControl instance;

    private float targetTimeScale = 1;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        targetTimeScale = 1;
    }

    private void Update()
    {
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, Time.unscaledDeltaTime * 10);
    }

    public void SetTimeScale(float scale)
    {
        targetTimeScale = scale;
    }
}

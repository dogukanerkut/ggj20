using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{

    public int targetScore;

    private int score;
    private List<GameObject> arrows = new List<GameObject>();

    private void Start()
    {
        EventManager.EventArrowFired.AddListener(UpdateScore);
        foreach (Transform child in transform)
        {
            arrows.Add(child.gameObject);
        }
    }

    private void OnDestroy()
    {
        EventManager.EventArrowFired.RemoveListener(UpdateScore);
    }

    public void UpdateScore()
    {
        score++;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (i < score)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }
        }
        if (score == targetScore)
        {
            TimeControl.instance.SetTimeScale(.01f);
            GameControl.instance.GameWin();
            Debug.Log("Game Win");
        }
    }

}

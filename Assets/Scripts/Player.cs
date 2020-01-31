using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject spawnerGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ObjectSpawner objSpawner = spawnerGO.GetComponent<ObjectSpawner>();
            objSpawner.Hit();
        }
    }

}

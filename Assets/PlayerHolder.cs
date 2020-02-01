using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void SetCanAttack()
    {
        _player.SetCanAttack(true);
        Debug.Log("SetCanAttack");
    }
}

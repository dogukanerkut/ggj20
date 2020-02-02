using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public List<Material> materials;

    private InputHandle inputHandle;
    private int playerCount;
    private List<Transform> children = new List<Transform>();

    private void Start()
    {
        inputHandle = new InputHandle();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        int joystickCount = Input.GetJoystickNames().Where(x => x.Length > 0).Count();
        int limit = joystickCount + 1 < 4 ? joystickCount + 1 : 4;
        for (int i = 0; i < limit; i++)
        {
            GameObject playerGO = Instantiate(playerPrefab);
            playerGO.transform.position = children[playerCount++].position;
            PlayerMovement pm = playerGO.GetComponent<PlayerMovement>();
            pm.playerNo = playerCount - 1;
            pm.inputHandle = inputHandle;
            Player p = playerGO.GetComponent<Player>();
            p.hatMeshRenderer.material = materials[pm.playerNo];
        }
    }

}

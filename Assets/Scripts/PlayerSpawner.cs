using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public List<Material> materials;
    public CinemachineTargetGroup targetGroup;
    public Transform cam;

    private InputHandle inputHandle;
    private int playerCount;
    private List<Transform> children = new List<Transform>();
    private Vector3 camPos;
    private Quaternion camRot;

    private void Start()
    {
        camPos = cam.transform.position;
        camRot = cam.transform.rotation;

        inputHandle = new InputHandle();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        int joystickCount = Input.GetJoystickNames().Where(x => x.Length > 0).Count();
        int limit = joystickCount + 1 < 4 ? joystickCount + 1 : 4;
        for (int i = 0; i < limit; i++)
        {
            SpawnPlayer();
        }
        StartCoroutine(Wait());
    }

    public void SpawnPlayer()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerGO.transform.position = children[playerCount++].position;
        PlayerMovement pm = playerGO.GetComponent<PlayerMovement>();
        pm.playerNo = playerCount - 1;
        pm.inputHandle = inputHandle;
        Player p = playerGO.GetComponent<Player>();
        p.hatMeshRenderer.material = materials[pm.playerNo];
        targetGroup.AddMember(playerGO.transform, 1f, 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnPlayer();
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        cam.transform.position = camPos;
        cam.transform.rotation = camRot;
    }

}

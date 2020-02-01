using UnityEngine;
using DG.Tweening;
public class ObjectSpawner : MonoBehaviour
{

    public GameObject itemPrefab;
    public Transform spawnPoint;
    public Vector2 forwardForceInterval;
    public Vector2 upwardForceInterval;
    public Vector2 sidewaysForceInterval;
    public int hitPoints;
    public float cooldown;
    public bool randomRot = false;
    public int spawnCount = 1;
    public ParticleSystem spawnVFX;
    public SpawnerEffect spawnerFX;
    public ParticleSystem _hitFX;
    private Rigidbody itemRB;
    private float interactTime;
    private int currentHitPoints;
    public ItemType ItemType;
    public Transform DoTarget;
    private void Start()
    {
        interactTime = -cooldown;
        currentHitPoints = hitPoints;
    }

    public void SpawnItem()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            interactTime = Time.time;
            GameObject itemGO = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);
            if (randomRot)
                itemGO.transform.rotation = Random.rotation;
            itemRB = itemGO.GetComponent<Rigidbody>();
            float forwardForce = Random.Range(forwardForceInterval.x, forwardForceInterval.y);
            float upwardsForce = Random.Range(upwardForceInterval.x, upwardForceInterval.y);
            float sidewaysForce = Random.Range(sidewaysForceInterval.x, sidewaysForceInterval.y);
            itemGO.transform.SetParent(transform);
            itemRB.AddForce(transform.forward * forwardForce + transform.up * upwardsForce +
                transform.right * sidewaysForce);
        }
        spawnVFX.Play();

        if (spawnerFX)
        {
            spawnerFX.ActivateFX(cooldown);
        }
    }

    public void Hit()
    {
        if (Time.time - interactTime < cooldown) return;

        currentHitPoints--;
        if (currentHitPoints <= 0)
        {
            SpawnItem();
            currentHitPoints = hitPoints;
        }

        DoHitEffect();
    }
    private void DoHitEffect()
    {
        if (_hitFX != null)
        {
            _hitFX.Play(true);
        }

        if (DoTarget == null)
        {
            return;
        }
        if (ItemType == ItemType.Wood)
        {
            DoTarget.DOPunchRotation(new Vector3(1, 1, 1), 0.5f);
        }

    }
    public void DoSpawnTween()
    {
        DoTarget.localRotation = Quaternion.identity;
        DoTarget.DOShakeRotation(cooldown, 20, 10, 90, false);
    }
}

using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

	public GameObject itemPrefab;
	public Transform spawnPoint;
	public Vector2 forwardForceInterval;
	public Vector2 upwardForceInterval;
	public Vector2 sidewaysForceInterval;
	public int hitPoints;
	public float cooldown;

	private Rigidbody itemRB;
	private float interactTime;
	private int currentHitPoints;

	private void Start()
	{
		interactTime = -cooldown;
		currentHitPoints = hitPoints;
	}

	private void SpawnItem()
	{
		interactTime = Time.time;
		GameObject itemGO = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity, transform);
		itemRB = itemGO.GetComponent<Rigidbody>();
		float forwardForce = Random.Range(forwardForceInterval.x, forwardForceInterval.y);
		float upwardsForce = Random.Range(upwardForceInterval.x, upwardForceInterval.y);
		float sidewaysForce = Random.Range(sidewaysForceInterval.x, sidewaysForceInterval.y);
		itemRB.AddForce(transform.forward * forwardForce + transform.up * upwardsForce +
			transform.right * sidewaysForce);
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
	}

}

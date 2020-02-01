
using UnityEngine;
using System.Collections;

public class AutoProjectile : MonoBehaviour
{

    public void Fire(Transform target, float initialAngle)
    {
        var rigid = GetComponent<Rigidbody>();
        Vector3 p = target.position;

        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(p.x, 0, p.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

        float distance = Vector3.Distance(planarTarget, planarPostion);
        float yOffset = transform.position.y - p.y;

        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
        float signedAngle = Vector3.SignedAngle(planarTarget - planarPostion, Vector3.forward, Vector3.up);
        Vector3 finalVelocity = Quaternion.AngleAxis(-signedAngle, Vector3.up) * velocity;

        rigid.velocity = finalVelocity;
        // rigid.AddForce(finalVelocity * rigid.mass, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.transform.parent.GetComponent<WallBreakHandler>().ReceiveHit();
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
    }
}

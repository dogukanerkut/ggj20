using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    public GameObject arrow;
    public float arrowLoadDelay;
    public float arrowSpeed;
    public float burnDuration;
    public ParticleSystem firePS;
    public ParticleSystem steamEffect;
    private bool onFire;
    private float burnTimer;

    public GameObject destructionEffect;

    private bool moveArrow;
    private GameObject newArrow;

    public void DelayedFire()
    {
        StartCoroutine(Fire());
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(arrowLoadDelay);
        newArrow = Instantiate(arrow, transform);
        arrow.SetActive(false);
        //Rigidbody newArrowRB = newArrow.GetComponent<Rigidbody>();
        //newArrowRB.isKinematic = false;
        //newArrowRB.velocity = newArrow.transform.up * 20f;
        StartCoroutine(Destroy(newArrow));
        moveArrow = true;
    }

    private void Update()
    {
        if(onFire)
        {
            burnTimer -= Time.deltaTime;
            if(burnTimer < 0)
                DestroyBallista();
        }
        if (moveArrow)
        {
            newArrow.transform.position += newArrow.transform.up * arrowSpeed * Time.deltaTime;
        }
    }

    public IEnumerator Destroy(GameObject obj)
    {
        yield return new WaitForSeconds(10f);
        Destroy(obj);
        moveArrow = false;
        GetComponent<Container>().RemoveItem();
    }

    private void DestroyBallista()
    {
        if(gameObject.activeInHierarchy)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    public void ExtnguishFire()
    {
        burnTimer = burnDuration;
        steamEffect.Play();
        firePS.Stop();
        onFire = false;
    }

    public void Burn()
    {
        firePS.Play();
        if(!onFire)
            burnTimer = burnDuration;
        onFire = true;
    }
}

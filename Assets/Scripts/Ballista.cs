using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    public GameObject arrow;
    public float arrowSpeed;
    public float burnDuration;
    public ParticleSystem firePS;
    public ParticleSystem steamEffect;
    public ParticleSystem muzzle;
    private bool onFire;
    private float burnTimer;

    public GameObject destructionEffect;

    private bool moveArrow;
    private GameObject newArrow;

    public void AttempFire()
    {
        if(arrow.activeInHierarchy)
            Fire();
    }

    public void Fire()
    {
        newArrow = Instantiate(arrow, transform);
        arrow.SetActive(false);
        StartCoroutine(Destroy(newArrow));
        muzzle.Play();
        moveArrow = true;
        EventManager.EventArrowFired.Invoke();
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
        if(onFire)
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

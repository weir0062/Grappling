using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum BulletState
    {
        ReadyToShoot,
        Shooting,
        Retracting
    }
    BulletState bulletState;
    public Rigidbody body;
    public float BulletSpeed = 30.0f;
    Gun gun;
    GameObject bulletSpawnPoint;
    float distFromOrigin;
    float lerppercent = 0.0f;
    SphereCollider collider;
    Vector3 FromRetract;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distFromOrigin = Vector3.Distance(bulletSpawnPoint.transform.position, transform.position);

        switch (bulletState)
        {
            case BulletState.ReadyToShoot:
                break;
            case BulletState.Shooting:
                if(distFromOrigin>69)
                {
                    SetState(BulletState.Retracting);
                }
                break;
            case BulletState.Retracting:
                Vector3 GunPosition = gun.transform.position;
                float dur = distFromOrigin / BulletSpeed;
                lerppercent += Time.deltaTime / dur;
                lerppercent = Mathf.Clamp01(lerppercent);
                transform.position= Vector3.Lerp(FromRetract, GunPosition, lerppercent);
                
                if (lerppercent == 1.0f)
                {
                    SetState(BulletState.ReadyToShoot);
                    Debug.Log("ReadyToShoot");
                }
                break;
            default:
                break;
        }
    }

    public void Setup(Gun Newgun)
    {
        gun = Newgun;
        bulletSpawnPoint = gun.BulletSpawn;
        SetState(BulletState.Retracting);
        collider = GetComponent<SphereCollider>();
        
    }

    public BulletState GetState()
    {
        return bulletState;
    }
    public void SetState(BulletState newState)
    {
        if (bulletState == newState)
        {
            return;
        }
        bulletState = newState;

        switch (bulletState)
        {
            case BulletState.Shooting:
                transform.parent = null;
                body.isKinematic = false;
                collider.enabled = true;
                body.velocity = gun.BulletSpawn.transform.forward * BulletSpeed;
                break;
            case BulletState.Retracting:
                FromRetract = transform.position;
                break;
            case BulletState.ReadyToShoot:
                lerppercent = 0.0f;
                transform.parent = bulletSpawnPoint.transform;
               // transform.position = Vector3.zero;
                body.isKinematic = true;
                collider.enabled = false;
                break;
            default:
                break;
        }
    }
    public void Shoot()
    {
        SetState(BulletState.Shooting);
    }

    public void Retract()
    {
        SetState(BulletState.Retracting);
    }

    private void OnCollisionEnter(Collision collision)
    {
        body.velocity = Vector3.zero;
        body.useGravity = false;
        if (collision.gameObject.layer == LayerMask.NameToLayer("PullTo"))
        {
            if (gun)
            {
                gun.PullTo(transform.position);
            }
        }
        else
        {
            Retract();
        }
    }

}
/*
public enum BulletState
    {
        ReadyToShoot, 
        Shooting,
        Retracting
    }
    BulletState bulletState;
    public Rigidbody body;
    public static float TimeToDestroy = 1.69f;
    public float BulletSpeed = 30.0f;
    public MeshRenderer renderer;
    Gun gun;
    float TimePassed = 0.0f;
    Vector3 StartPoint;
    float distFromOrigin;
    float lerppercent = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
    }

    void Update()
    {

        distFromOrigin = Vector3.Distance(StartPoint, transform.position);
        TimePassed += Time.deltaTime;

        switch (bulletState)
        {
            case BulletState.ReadyToShoot:
                break;
            case BulletState.Shooting:
                if (TimePassed >= TimeToDestroy || distFromOrigin > 69.0f)
                {
                    if (this)
                    {
                        Retract();
                    }
                }
                break;
            case BulletState.Retracting:
                Vector3 GunPosition = gun.transform.position;
                Vector3.Lerp(transform.position, GunPosition, lerppercent);
                lerppercent += Time.deltaTime / 6.9f;

                if (lerppercent > 1.0f)
                {
                    lerppercent = 1.0f;
                    Destruction();
                }
                break;
            default:
                break;
        }

       

    }
    public void SetState(BulletState newState)
    {
        bulletState = newState;

        switch (bulletState)
        {
            case BulletState.Shooting:

                body.velocity = gun.BulletSpawn.transform.forward * BulletSpeed;
                break;
            case BulletState.Retracting:
                break;
            default:
                break;
        }
    }
    public void Shoot( Gun UsedGun)
    {
        gun = UsedGun;
        SetState(BulletState.Shooting);
    }

    public void Retract()
    {        
        SetState(BulletState.Retracting);
    }
    void Destruction()
    {
        renderer.enabled = false;
        Destroy(this);
    }
    private void OnCollisionEnter(Collision collision)
    {        
        body.velocity = Vector3.zero;
        body.useGravity = false;
        if(collision.gameObject.tag == "PullTo")
        {
             if(gun)
                 {
                 gun.PullTo(transform.position);
                 }
        }
        else
        {
            Destruction();
        }
    }
}
*/
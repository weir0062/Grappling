using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject BulletSpawn;
    PlayerController player;
    Projectile bullet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SetupBullet();
    }
    void Shoot()
    {
        bullet.Shoot();
        Debug.Log("Gun Shooting");
    }
    void SetupBullet()
    {

        bullet = GameObject.Instantiate(BulletPrefab).GetComponent<Projectile>();
        bullet.gameObject.transform.position = BulletSpawn.transform.position;
        bullet.Setup(this);
    }
    public void OnFinishedPulling()
    {
        bullet.SetState(Projectile.BulletState.Retracting);
    }
    public void PullTo(Vector3 endpos)
    {      
        player.PullTo(endpos);
    }
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            if(bullet.GetState() == Projectile.BulletState.ReadyToShoot)
            {
                Shoot();
            }
        }
    }
}

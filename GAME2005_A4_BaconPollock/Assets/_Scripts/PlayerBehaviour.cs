using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Weapon
    public Transform bulletSpawn;
    public ObjectPool bulletPool;
    public float bulletSpeed;
    public float bulletLifespan;
    public float refireRate;
    private float refireCD;

    void start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        refireCD += Time.deltaTime;
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (refireCD >= refireRate)
            {
                if(bulletPool.Empty())
                {
                    Debug.Log("Bullet pool is empty!");
                }
                else
                {
                    refireCD = 0.0f;
                    
                    var bullet = bulletPool.Get();
                    bullet.transform.position = bulletSpawn.position;
                    bullet.GetComponent<PhysicsBehaviour>().velocity = bulletSpawn.forward * bulletSpeed;
                    
                    var returner = bullet.GetComponent<PoolReturner>();
                    returner.pool = bulletPool;

                    returner.timer = bullet.GetComponent<Timer>();
                    returner.timer.currentTime = 0.0f;
                    returner.timer.endTime = bulletLifespan;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public int fireRate;
    public ObjectPool bulletPool;

    void start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
    }

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {
                bulletPool.Get(bulletSpawn.position, bulletSpawn.forward);
            }
        }
    }
}

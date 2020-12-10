using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolReturner : MonoBehaviour
{
    public ObjectPool pool;
    public Timer timer;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (timer.IsExpired())
        {
            pool.Return(this.gameObject);
        }
    }
}

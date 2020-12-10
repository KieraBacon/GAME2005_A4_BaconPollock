using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{
    private static WorldSettings _instance;
    public static WorldSettings Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Vector3 gravity = new Vector3(0.0f, -9.8f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        PhysicsBehaviour[] actors = FindObjectsOfType<PhysicsBehaviour>();
        for(uint i = 0; i < actors.Length; i++)
        {
            if(actors[i].GravityEnabled)
            {
                actors[i].acceleration += gravity;
            }
        }
    }

// Update is called once per frame
void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer : MonoBehaviour
{
    public float currentTime;
    public float endTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
    }
    public bool IsExpired()
    {
        return currentTime >= endTime;
    }
}

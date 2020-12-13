using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timeDisplay;
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
        float roundedTime = Mathf.Round(currentTime);
        timeDisplay.text = roundedTime.ToString();
    }
    public bool IsExpired()
    {
        return currentTime >= endTime;
    }
}

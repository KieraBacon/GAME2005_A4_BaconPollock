using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsValue : MonoBehaviour
{

    public GameObject targetObject;
    public Text textUI;
    public string valueName;

    // Start is called before the first frame update
    void Start()
    {
        SetValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValue()
    {
        PhysicsBehaviour pb = targetObject.GetComponent<PhysicsBehaviour>();
        switch (valueName)
        {
            case "Friction":
                textUI.text = pb.friction.ToString();
                break;
            case "Mass":
                textUI.text = pb.mass.ToString();
                break;
            case "Velocity":
                textUI.text = pb.velocity.ToString();
                break;
        }
;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public GameObject targetObject;
    public string valueName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementValue()
    {
        PhysicsBehaviour pb = targetObject.GetComponent<PhysicsBehaviour>();
        switch (valueName)
        {
            case "Friction":
                pb.SetFriction(pb.GetFriction() + 0.1f);
                break;
            case "Mass":
                pb.SetMass(pb.GetMass() + 1.0f);
                break;
            //case "Velocity":
            //    textUI.text = pb.velocity.ToString();
            //    break;
        }
;
    }

    public void DecrementValue()
    {
        PhysicsBehaviour pb = targetObject.GetComponent<PhysicsBehaviour>();
        switch (valueName)
        {
            case "Friction":
                pb.SetFriction(pb.GetFriction() - 0.1f);
                break;
            case "Mass":
                pb.SetMass(pb.GetMass() - 1.0f);
                break;
                //case "Velocity":
                //    textUI.text = pb.velocity.ToString();
                //    break;
        }
;
    }
}

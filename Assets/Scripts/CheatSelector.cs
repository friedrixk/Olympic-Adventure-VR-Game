using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSelector : Selector
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool isTriggered()
    {
        return Input.GetKeyDown(KeyCode.C);
    }

    public override bool isSecondaryTriggered()
    {
        return Input.GetKeyDown(KeyCode.V);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

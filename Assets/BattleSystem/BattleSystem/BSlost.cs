using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSlost : BSstate
{
    public BSlost(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Lost");
    }

    public override void Run()
    {

    }
}

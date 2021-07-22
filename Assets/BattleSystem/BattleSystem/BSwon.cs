using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSwon : BSstate
{
    public BSwon(BSstatemanager theManager) : base(theManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Won");
    }

    public override void Run()
    {

    }


}

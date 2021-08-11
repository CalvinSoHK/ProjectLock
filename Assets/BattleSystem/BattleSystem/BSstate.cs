using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BSstate
{ 
    protected BSstatemanager stateManager;

    public abstract void Run();
    public virtual void Enter() { }
    public virtual void Leave() { }
    
    public BSstate(BSstatemanager theManager)
    {
        stateManager = theManager;
    }
}

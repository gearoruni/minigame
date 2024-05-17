using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Component : PoolBaseClass
{
    public string name;
    public Entity entity;
    //public int dataDefind;

    /*-------- Component Data --------*/



    /*--------    End Data    --------*/

    public Component() { }

    public virtual void Init() { }
    public virtual void DataInit() { }

    public virtual void Update() { }

    public virtual void OnCache()
    {

    }
}

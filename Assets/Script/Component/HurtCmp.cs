using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCmp : MonoBehaviour
{
    public string ComponentId;
    public int dmg;
    public string tag;
    public Action callback;
    public void Init(string ComponentId, int dmg)
    {
        this.ComponentId = ComponentId;
        this.dmg = dmg;
        this.tag = gameObject.tag;
    }
}

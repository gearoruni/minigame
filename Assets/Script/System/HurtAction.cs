using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAction 
{
    
    public static int TakenDmg(int dmg,int hp)
    {
        return hp - dmg;
    }

}

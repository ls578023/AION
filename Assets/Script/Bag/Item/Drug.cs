using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Drug : Item
{
    public int hp;
    public int mp;

    public Drug()
    {
        hp = 0;
        mp = 0;
    }

    public Drug(Drug drug):base(drug)
    {
        hp = drug.hp;
        mp = drug.mp;
    }
}

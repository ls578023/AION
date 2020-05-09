using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Equipment : Item
{
    public int atk;
    public int def;
    public int mAtk;
    public int mDef;
    public int crit;
    public int maxHP;
    public int maxMP;
    public ModelType modelType;
    public int star;//装备星级

    public Equipment()
    {
        atk = 0;
        def = 0;
        mAtk = 0;
        mDef = 0;
        crit = 0;
        maxHP = 0;
        maxMP = 0;
        modelType = ModelType.Boy;
        star = 0;
    }

    public Equipment(Equipment equ)
        : base(equ)
    {
        atk = equ.atk;
        def = equ.def;
        mAtk = equ.mAtk;
        mDef = equ.mDef;
        crit = equ.crit;
        maxHP = equ.maxHP;
        maxMP = equ.maxMP;
        modelType = equ.modelType;
        star = equ.star;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardInfo : MonoBehaviour {//掉落面板中的每一行信息 包括图标和名字

    public ItemID id;
    public Image icon;
    public Text _name; 

    public void InitReward()//给了ID之后，生成奖励的图片和名字
    {
        Item obj = ItemFactory.CreateItem(id);//给了物品ID之后，生成这个商品的属性
        var itemSprite = ResourcesManager.Load<Sprite>(obj.path);
        icon.sprite = itemSprite;
        _name.text = obj._name;
    }
}

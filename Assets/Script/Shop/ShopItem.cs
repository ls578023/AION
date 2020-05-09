using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour 
{
    public ItemID id;
    public Image icon;
    public Text price;
    public Button buy;
    public AudioSource sound;

    public void InitShopUnit()
    {
        Item obj = ItemFactory.CreateItem(id);//给了物品ID之后，生成这个商品的属性
        var itemSprite = ResourcesManager.Load<Sprite>(obj.path);
        icon.sprite = itemSprite;
        price.text = obj.buyPrice.ToString();
        buy.onClick.AddListener(BuyItem);
    }

    void BuyItem()
    {
        sound.Play();
        if (PlayerInfo.Instance.money - int.Parse(price.text) >= 0)
        {
            //可以购买
            BagData.Instance.AddItem(id);//购买之后背包增加这个物品
            PlayerInfo.Instance.money -= int.Parse(price.text);
            MyEventSystem.SendMsg(EventName.UpdateMoney, null);
        }
        else
        {
            TipPanel.Instance.info.text = "金币不足";
            TipPanel.Instance.Open();
        }


    }

}

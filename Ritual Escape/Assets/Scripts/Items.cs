using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : InterractableOnLook
{
    public Item currentItem = Item.none;
    protected override void Start()
    {
        base.Start();
        //Manager.instance.inactiveItems.Add(this);
        //gameObject.SetActive(false);
    }
    protected override void OnMouseOver()
    {
        if (Vector3.Distance(playerTrans.position, transform.position) > useDinst)
        {
            return;
        }
        if (currentItem != Item.none)
        {
            manager.ShowUseInterface(messageToUse + currentItem.ToString());
            canUse = true;
        }
    }

    public override void Use()
    {
        if (canUse)
        {
            player.GetItem(currentItem);
            player.OnUse.RemoveListener(Use);
            //Manager.instance.CreateItem(currentItem);
            Manager.instance.RemoveItem(this);
            manager.HideUseInterface();
        }
    }
}

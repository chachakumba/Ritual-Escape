using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interractble : MonoBehaviour
{
    protected Player player;
    protected Transform playerTrans;
    protected Manager manager;
    public string messageToUse = "";
    protected virtual void Start()
    {
        manager = Manager.instance;
        player = manager.player;
        playerTrans = player.transform;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {

    }
    protected virtual void OnTriggerExit(Collider other)
    {

    }
    public virtual void Use()
    {

    }
}
public abstract class InterractableOnLook: Interractble
{
    protected float useDinst = 5;
    public bool canUse = false;
    protected override void Start()
    {
        base.Start();
        useDinst = player.itemPickUpDist;
    }
    protected virtual void OnMouseEnter()
    {
        player.OnUse.AddListener(Use);
    }
    protected virtual void OnMouseOver()
    {
        if (Vector3.Distance(playerTrans.position, transform.position) > useDinst)
        {
            return;
        }
        manager.ShowUseInterface(messageToUse);
        canUse = true;
    }
    protected virtual void OnMouseExit()
    {
        manager.HideUseInterface();
        canUse = false;
        Debug.Log("CanNotUse");
        player.OnUse.RemoveListener(Use);
    }
}
public abstract class InterractableOnEnter : Interractble
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.OnUse.AddListener(Use);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.OnUse.RemoveListener(Use);
        }
    }
}

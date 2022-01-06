using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InterractableOnLook
{
    public bool isUsed = false;
    Animator anim;
    Controls controls;
    protected override void Start()
    {
        base.Start();
        manager.hidingSpots.Add(this);
        anim = GetComponent<Animator>();
        controls = new Controls();
    }
    public override void Use()
    {
        anim.Play("_Open");
        player.Hide(this);
        controls.Basic.Use.performed += (ctx) => UnHideAnim();
        controls.Enable();
    }
    async void UseAs()
    {
        await System.Threading.Tasks.Task.Yield();
    }
    void UnHideAnim()
    {
        Debug.Log("Ex");
        anim.Play("_Close");
    }
    public void UnHide()
    {
        player.UnHide();
    }
}

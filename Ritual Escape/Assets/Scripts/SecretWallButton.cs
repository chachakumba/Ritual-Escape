using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWallButton : InterractableOnLook
{
    public SecretWall attachedWall;
    protected override void Start()
    {
        base.Start();
    }

    public override void Use()
    {
        if (canUse)
        {
            attachedWall.Use();
        }
    }
}

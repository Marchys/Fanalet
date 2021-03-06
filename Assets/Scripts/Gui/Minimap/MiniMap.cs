﻿using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviourEx, IHandle<PlayerDeathMessage>
{

    public Transform Target;
    public float ZoomLevel = 10f;

    public Vector2 TransformPosition(Vector3 position)
    {
        var offset = position- Target.position;
        var newPosition = new Vector2(offset.x, offset.y);
        newPosition*=ZoomLevel;
        return newPosition;
    }

    public void Handle(PlayerDeathMessage message)
    {
        enabled = false;
    }


}

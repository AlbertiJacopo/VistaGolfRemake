using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RotationState
{
    TouchMoved
}

public enum ZoomState
{
    TouchBegan,
    TouchMoved,
    TouchEnded
}

public enum CameraState
{
    Zoom,
    Rotation
}
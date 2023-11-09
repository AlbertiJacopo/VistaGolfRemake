using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomStatesManager : StatesMachine<ZoomState>
{
    public Vector3 TouchScreenStartPos;
    public Vector3 TouchScreenStartPos2;
    public InputManager InputManager;

    public ZoomStatesManager(Vector3 touchScreenStartPos, Vector3 touchScreenStartPos2, InputManager inputManager)
    {
        TouchScreenStartPos = touchScreenStartPos;
        TouchScreenStartPos2 = touchScreenStartPos2;
        InputManager = inputManager;
    }

    protected override void InitStates()
    {
        StatesList.Add(ZoomState.TouchBegan, new ZoomTouchBegan(ZoomState.TouchBegan, this));
        StatesList.Add(ZoomState.TouchMoved, new ZoomTouchMoved(ZoomState.TouchMoved, this));
        StatesList.Add(ZoomState.TouchEnded, new ZoomTouchEnded(ZoomState.TouchEnded, this));
    }
}




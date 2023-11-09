using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomStatesManager : StatesMachine<ZoomState>
{
    public Transform PlayerTransform;

    public ZoomStatesManager(Transform playerTransform, Dictionary<CameraState, State<CameraState>> stateList = null, State<CameraState> currentState = null, State<CameraState> previousState = null) : base()
        {
        PlayerTransform = playerTransform;
        }

    protected override void InitStates()
        {
            StatesList.Add(ZoomState.TouchBegan, new ZoomTouchBegan(ZoomState.TouchBegan, this));
            StatesList.Add(ZoomState.TouchMoved, new ZoomTouchMoved(ZoomState.TouchMoved, this));
            StatesList.Add(ZoomState.TouchEnded, new ZoomTouchEnded(ZoomState.TouchEnded, this));
        }
}


public enum ZoomState
{
    TouchBegan,
    TouchMoved,
    TouchEnded
}

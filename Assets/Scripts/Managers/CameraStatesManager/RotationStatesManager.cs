using Framework.Generics.Pattern.StatePattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RotationStatesManager : StatesMachine<RotationState>
{
    public Transform PlayerTransform;

    public RotationStatesManager(Transform playerTransform, Dictionary<CameraState, State<CameraState>> stateList = null, State<CameraState> currentState = null, State<CameraState> previousState = null) : base()
    {
        PlayerTransform = playerTransform;
    }

    protected override void InitStates()
    {
        StatesList.Add(RotationState.TouchBegan, new RotationTouchBegan(RotationState.TouchBegan, this));
        StatesList.Add(RotationState.TouchMoved, new RotationTouchMoved(RotationState.TouchMoved, this));
    }
}


public enum RotationState
{
    TouchBegan,
    TouchMoved
}

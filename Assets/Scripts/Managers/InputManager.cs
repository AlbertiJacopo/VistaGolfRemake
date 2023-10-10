using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float m_DeadZoneSwingRadius;
    [SerializeField] private float m_DeadZoneBallRadius;
    private GameObject m_Ball;
    private Vector2 m_TouchStartPosition;
    private Vector2 m_TouchEndPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetStartEndTouches()
    {

    }

    public bool CheckDeadzoneBall()
    {
        return true;
    }
}

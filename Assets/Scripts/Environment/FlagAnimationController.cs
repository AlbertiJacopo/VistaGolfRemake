using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimationController : MonoBehaviour
{
    [SerializeField] private float m_DistanceFromBall;
    [SerializeField] private Transform m_Ball;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ChechIfNearBall();
    }

    private void ChechIfNearBall()
    {
        if (Vector3.Distance(transform.position, m_Ball.position) < m_DistanceFromBall)
            m_Animator.SetBool("IsRising", true);

        else
            m_Animator.SetBool("IsRising", false);
    }
}

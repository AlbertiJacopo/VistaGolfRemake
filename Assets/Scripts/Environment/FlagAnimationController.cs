using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAnimationController : MonoBehaviour
{
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            m_Animator.SetBool("IsRising", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            m_Animator.SetBool("IsRising", true);
    }
}

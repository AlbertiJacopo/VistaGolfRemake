using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Tooltip("Duration in seconds of the pause between restarting the animation")]
    [SerializeField] float m_AnimationPauseTime;
    [SerializeField] string m_AnimationName;
    private Animator m_Anim;
    // Start is called before the first frame update
    void Start()
    {
        m_Anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AnimationCycle());
    }

    private IEnumerator AnimationCycle()
    {
        m_Anim.Play(m_AnimationName);
        yield return new WaitForSeconds(m_AnimationPauseTime);
    }

}

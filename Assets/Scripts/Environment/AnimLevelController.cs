using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimLevelController : MonoBehaviour
{
	[SerializeField] private Animator m_Level;

	// Start is called before the first frame update
	void Start()
	{
        GameManager.instance.EventManager.Register(Constants.ANIM_START_SINK, PlaySinkAnimaton);
        StartCoroutine(WaitTimeBallAppear());
    }

	public void PlaySinkAnimaton(object[] param)
	{
		m_Level.SetBool("EndLevel", true);

		StartCoroutine(WaitForEndAnim());
	}
        
    /// <summary>
    /// Wait for the end of the animation then loads next level
    /// </summary>
	private IEnumerator WaitForEndAnim()
	{
		yield return new WaitForSeconds(m_Level.GetCurrentAnimatorStateInfo(0).length);
		GameManager.instance.EventManager.TriggerEvent(Constants.LOAD_NEXT_LEVEL);
	}

    private IEnumerator WaitTimeBallAppear()
    {
        yield return new WaitForSeconds(m_Level.GetCurrentAnimatorStateInfo(0).length);
        //GameManager.instance.EventManager.TriggerEvent(Constants.TOGGLE_BALL);
        GameManager.instance.EventManager.TriggerEvent(Constants.SPAWN_BALL);
    }
}

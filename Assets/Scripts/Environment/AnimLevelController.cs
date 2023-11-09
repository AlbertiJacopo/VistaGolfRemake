using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimLevelController : MonoBehaviour
{

	[SerializeField] private Animator m_Level;
	[SerializeField] private GameObject m_Ball;

	// Start is called before the first frame update
	void Start()
	{
		GameManager.instance.EventManager.Register(Constants.ANIM_START_SINK, PlaySinkAnimaton);
		GameManager.instance.EventManager.Register(Constants.TOGGLE_BALL, BallToggle);
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
		BallToggle(null);
	}

	/// <summary>
    	/// Set ball gameobject active or not depending on which state the gameobject is
    	/// <summary>
	public void BallToggle(object[] param)
	{
		if (m_Ball.activeSelf)
			m_Ball.SetActive(false);
		else if (!m_Ball.activeSelf)
			m_Ball.SetActive(true);
	}
}

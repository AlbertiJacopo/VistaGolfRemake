using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float m_ReducingAlphaPercent;
    [SerializeField] private Material m_TransparentMaterial;
    private Transform m_Ball;
    private Material[] m_PreviousMaterials;
    private GameObject[] m_PreviousGameObjects;

    private void Start()
    {
        GameManager.instance.EventManager.Register(Constants.UPDATE_CAMERA_ROTATION, FadingMaterial);
    }

    public void FadingMaterial(object[] param)
    {
        Vector3 dir = m_Ball.position - transform.position;
        float distance = Vector3.Distance(m_Ball.position, transform.position);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, distance);

        m_PreviousMaterials = new Material[hits.Length - 1];
        m_PreviousGameObjects = new GameObject[hits.Length - 1];

        if (hits.Length > 1)
        {
            for (int i = 0; i < hits.Length - 1; i++)
            {
                if (hits[i].collider.gameObject.transform.position != m_Ball.position)
                {
                    m_PreviousGameObjects[i] = hits[i].collider.gameObject;
                    m_PreviousMaterials[i] = hits[i].collider.gameObject.GetComponent<MeshRenderer>().material;
                    Color tmp = m_PreviousMaterials[i].color;
                    tmp.a = 1f - m_ReducingAlphaPercent;
                    m_TransparentMaterial.color = tmp;
                    hits[i].collider.gameObject.GetComponent<MeshRenderer>().material = m_TransparentMaterial;
                }
            }
        }
    }

    private void OnValidate()
    {
        if(m_Ball == null)
        {
            m_Ball = FindObjectOfType<MovementComponent>().transform;
        }
    }
}

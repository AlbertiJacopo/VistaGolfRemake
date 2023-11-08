using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.Universal.UniversalRenderPipelineEditorResources;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float m_ReducingAlphaPercent;
    private Material m_TransparentMaterial;
    [SerializeField] string m_FadedMaterialName;
    private Transform m_Ball;
    private Transform m_Camera;
    private Material m_PreviousMaterial;
    //private GameObject[] m_PreviousGameObjects;

    private void Start()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Player");
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        m_Ball = ball.transform;
        m_Camera = mainCamera.transform;
        GameManager.instance.EventManager.Register(Constants.UPDATE_CAMERA_ROTATION, ChangeObstacleMaterial);
        m_PreviousMaterial = GetComponent<MeshRenderer>().material;
        m_TransparentMaterial = Resources.Load<Material>(m_FadedMaterialName);
        if (m_PreviousMaterial == null)
            Debug.Log("MATERIAL NULL");
    }

    //public void FadingMaterial(object[] param)
    //{
    //    Vector3 dir = m_Ball.position - transform.position;
    //    float distance = Vector3.Distance(m_Ball.position, transform.position);

    //    RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, distance);

    //    m_PreviousMaterials = new Material[hits.Length - 1];
    //    m_PreviousGameObjects = new GameObject[hits.Length - 1];

    //    if (hits.Length > 1)
    //    {
    //        for (int i = 0; i < hits.Length - 1; i++)
    //        {
    //            if (hits[i].collider.gameObject.transform.position != m_Ball.position)
    //            {
                    //m_PreviousGameObjects[i] = hits[i].collider.gameObject;
                    //m_PreviousMaterials[i] = hits[i].collider.gameObject.GetComponent<MeshRenderer>().material;
                    //Color tmp = m_PreviousMaterials[i].color;
                    //tmp.a = 1f - m_ReducingAlphaPercent;
                    //m_TransparentMaterial.color = tmp;
                    //hits[i].collider.gameObject.GetComponent<MeshRenderer>().material = m_TransparentMaterial;
    //            }
    //        }
    //    }
    //}

    //private void OnValidate()
    //{
    //    if(m_Ball == null)
    //    {
    //        m_Ball = FindObjectOfType<MovementComponent>().transform;
    //    }
    //}

    public void ChangeObstacleMaterial(object[] param)
    {
        if (Vector3.Distance(m_Ball.position, transform.position) < Vector3.Distance(m_Ball.position, m_Camera.position)/2 && 
            Vector3.Distance(m_Camera.position, transform.position) < Vector3.Distance(m_Ball.position, m_Camera.position) / 2)
        {
            Debug.Log("ENTERED TRANSPARENT");
            Color tmp = m_PreviousMaterial.color;
            tmp.a = m_ReducingAlphaPercent;
            m_TransparentMaterial.color = tmp;
            GetComponent<MeshRenderer>().material = m_TransparentMaterial;
        }
        else
        {
            Debug.Log("EXITED TRANSPARENT");
            GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material = m_PreviousMaterial;
        }
    }
}

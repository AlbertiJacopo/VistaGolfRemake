using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.Universal.UniversalRenderPipelineEditorResources;

public class FadeController : MonoBehaviour
{
    [SerializeField] private float m_ReducingAlphaPercent;
    [SerializeField] string m_FadedMaterialName;
    [SerializeField, Range(0f, 1f)] private float m_MinValue;
    private Material m_TransparentMaterial;
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
        //Search from the file the transparent material and does a copy of that
        m_TransparentMaterial = new Material(Resources.Load<Material>(m_FadedMaterialName));
        if (m_PreviousMaterial == null)
            Debug.Log("MATERIAL NULL");
    }

    /// <summary>
    /// Change the matieral of the object from default to transparent and vice versa
    /// <summary>
    public void ChangeObstacleMaterial(object[] param)
    {

        if (IsInRange() && IsParallel())
        {
            Color tmp = m_PreviousMaterial.color;
            tmp.a = m_ReducingAlphaPercent;
            m_TransparentMaterial.color = tmp;
            GetComponent<MeshRenderer>().material = m_TransparentMaterial;
        }
        else
        {
            if (GetComponent<MeshRenderer>().material != m_PreviousMaterial)
                GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material = m_PreviousMaterial;
        }
    }

    /// <summary>
    /// Check if this object is in range to be faded
    /// <summary>
    private bool IsInRange()
    {
        return Vector3.Distance(m_Ball.position, transform.position) < Vector3.Distance(m_Ball.position, m_Camera.position) &&
                    Vector3.Distance(m_Camera.position, transform.position) < Vector3.Distance(m_Ball.position, m_Camera.position);
    }
    /// <summary>
    /// Check if this object is parallel (from the camera and the ball) enough to be faded 
    /// <summary>
    private bool IsParallel()
    {
        Vector3 dir = m_Ball.position - m_Camera.position;
        Vector3 objectDir = transform.position - m_Ball.position;
        float h = Mathf.Abs(Vector2.Dot(new Vector2(dir.x, dir.z).normalized, new Vector2(objectDir.x, objectDir.z).normalized));

        return h > m_MinValue && h < 1f;
    }
}

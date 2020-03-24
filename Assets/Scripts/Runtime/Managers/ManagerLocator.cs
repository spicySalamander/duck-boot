using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManagerLocator : MonoBehaviour
{
    public List<GameObject> m_Managers;

    private void Awake()
    {
        if(m_Managers.Count > 0)
            for (int i = 0; i < m_Managers.Count; i++)
            {
                Instantiate(m_Managers[i], transform);
            }
    }
}

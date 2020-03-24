using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static T m_Instance;

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (null == m_Instance)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if (null == m_Instance)
                    {
                        // create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        Debug.LogWarning($"[Singleton] Instance doesn't exist. created a new one and attached to: {singletonObject.name}");
                        // Make the instantiated obejct persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                    else
                    {
                        // Make the instantiated obejct persistent.
                        DontDestroyOnLoad(m_Instance.gameObject);
                    }
                }

                return m_Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }
}

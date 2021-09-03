using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
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
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if(m_Instance == null)
                    {
                        //gameObject가 비활성화 중인 것들에서도 찾자.
                        m_Instance = (T)GetAllObjectsOnlyInScene<T>();
                    }

                    if (m_Instance == null)
                    {
                        Debug.LogError($"{typeof(T)} 싱글턴 클래스 없음.");
                    }
                }
                return m_Instance;
            }

            Component GetAllObjectsOnlyInScene<T1>() where T1 : Component
            {
                var components = Resources.FindObjectsOfTypeAll(typeof(T1));
                foreach (Object co in components)
                {
                    Component component = co as Component;
                    GameObject go = component.gameObject;
                    if (go.scene.name == null) // 씬에 있는 오브젝트가 아니므로 제외한다.
                        continue;

                    if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave || go.hideFlags == HideFlags.HideInHierarchy)
                        continue;

                    return component;
                }

                return null;
            }
        }
    }


    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_Instance = null;
    }
}
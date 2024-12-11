using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_instance;

    public static T instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogWarning($"Singleton of type {typeof(T)} not found in the scene! Please ensure it is assigned or instantiated.");
            }
            return m_instance;
        }
    }

    public static void Initialize(T instance)
    {
        if (m_instance == null)
        {
            m_instance = instance;
        }
        else
        {
            Debug.LogWarning($"Instance of {typeof(T)} is already initialized.");
        }
    }
}

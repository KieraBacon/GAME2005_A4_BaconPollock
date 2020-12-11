using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool : MonoBehaviour
{
    public uint MaxSize;
    public GameObject objectType;
    private Queue<GameObject> m_pool;

    // Start is called before the first frame update
    void Start()
    {
        m_pool = new Queue<GameObject>();

        for (uint count = 0; count < MaxSize; count++)
        {
            GameObject obj = Instantiate(objectType, Vector3.zero, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(false);
            m_pool.Enqueue(obj);
        }
    }

    public Queue<GameObject> GetAll()
    {
        return m_pool;
    }

    public GameObject Get()
    {
        GameObject obj = m_pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        m_pool.Enqueue(obj);
        obj.SetActive(false);
    }

    public bool Empty()
    {
        return m_pool.Count <= 0;
    }
}
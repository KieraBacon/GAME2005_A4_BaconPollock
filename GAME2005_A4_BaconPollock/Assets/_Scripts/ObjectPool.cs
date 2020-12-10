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

        for(uint count = 0; count < MaxSize; count++)
        {
            GameObject obj = Instantiate(objectType, Vector3.zero, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(false);
            m_pool.Enqueue(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Get(Vector3 position, Vector3 direction)
    {
        GameObject obj = m_pool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.GetComponent<BulletBehaviour>().direction = direction;
        return obj;
    }

    public bool Empty()
    {
        return m_pool.Count <= 0;
    }
}

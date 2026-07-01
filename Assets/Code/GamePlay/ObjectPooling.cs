using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;
    [SerializeField] private Shuriken_E shurikenPrefab;
    [SerializeField] private int poolSize;
    private Queue<Shuriken_E> queue = new Queue<Shuriken_E>();
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            Create();
        }
    }
    private void Create()
    {
        Shuriken_E shuriken = Instantiate(shurikenPrefab, transform);
        shuriken.gameObject.SetActive(false);
        queue.Enqueue(shuriken);
    }
    // Update is called once per frame
    public Shuriken_E Get()
    {
        if (queue.Count == 0)
        {
            Create();
        }
        Shuriken_E shuriken = queue.Dequeue();
        shuriken.gameObject.SetActive(true);
        return shuriken;
    }
    public void ReturnPool(Shuriken_E shuriken)
    {
        shuriken.gameObject.SetActive(false);
        queue.Enqueue(shuriken);
    }
}

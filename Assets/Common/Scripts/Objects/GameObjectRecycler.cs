using System.Collections.Generic;
using UnityEngine;

class GameObjectPool
{
    private GameObject _proto;
    private List<GameObject> _pool;
    private int _capacity;

    public GameObjectPool(GameObject proto, int count)
    {
        _proto = proto;
        _pool = new List<GameObject>();
        _capacity = count;

        for(int i = 0; i < count; i++)
        {
            GameObject inst = proto.Clone();
            inst.SetActive(false);
            _pool.Add(inst);
        }
    }

    public GameObject Claim()
    {
        if(_pool.Count < 0)
        {
            _pool.Add(_proto.Clone());
            _capacity++;
        }

        GameObject last = _pool.Last();
        _pool.RemoveLast();

        last.SetActive(true);
        return(last);
    }

    public void Recycle(GameObject go)
    {
        go.SetActive(false);
        _pool.Add(go);
    }

    public int Capacity
    {
        set
        {
            for(int i = _capacity; i < value; i++)
            {
                _pool.Add(_proto.Clone());
            }

            _capacity = value;
        }
        get
        {
            return(_capacity);
        }
    }
}
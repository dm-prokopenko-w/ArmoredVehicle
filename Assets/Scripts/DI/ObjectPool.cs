using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ObjectPool<T> where T: Behaviour
    {
        private Dictionary<string, Pool<T>> _pools = new Dictionary<string, Pool<T>>();
        
        public void InitPool(T prefab, Transform container)
        {
            if (prefab != null && !_pools.ContainsKey(prefab.name))
            {
                _pools[prefab.name] = new Pool<T>(prefab, container);
            }
        }

        public T Spawn(T prefab, Vector3 pos, Quaternion rot, Transform container)
        {
            InitPool(prefab, container);
            return _pools[prefab.name].Spawn(pos, rot, container);
        }

        public void Despawn(T obj)
        {
            if (_pools.ContainsKey(obj.name))
            {
                _pools[obj.name].Despawn(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }

        class Pool<T> where T: Behaviour
        {
            private List<T> _inactive = new ();
            private T _prefab;
            private Transform _container;

            public Pool(T prefab, Transform container)
            {
                _prefab = prefab;
                _container = container;
            }

            public T Spawn(Vector3 pos, Quaternion rot, Transform container)
            {
                T obj;
                if (_inactive.Count == 0)
                {
                    obj = Object.Instantiate(_prefab, pos, rot, container);
                    obj.name = _prefab.name;
                }
                else
                {
                    obj = _inactive[_inactive.Count - 1];
                    _inactive.RemoveAt(_inactive.Count - 1);
                    obj.transform.SetParent(container);
                }

                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.gameObject.SetActive(true);
                return obj;
            }

            public void Despawn(T obj)
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(_container);
                _inactive.Add(obj);
            }
        }
    }
}
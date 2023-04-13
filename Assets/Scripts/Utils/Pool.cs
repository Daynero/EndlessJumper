using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class Pool<T> where T : MonoBehaviour, IDisposable
    {
        private readonly List<T> _pool = new();
        private readonly T _prototype;
        private readonly Transform _root;

        public Pool(int initialSize, T prototype, Transform root)
        {
            _prototype = prototype;
            _root = root;
            while (initialSize-- != 0)
            {
                _pool.Add(Object.Instantiate(prototype, root));
                _pool.Last().gameObject.SetActive(false);
            }
        }

        public T Create()
        {
            T item = _pool.First(o => !o.gameObject.activeSelf);
            if (item == null)
            {
                item = Object.Instantiate(_prototype, _root);
                _pool.Add(item);
            }

            item.gameObject.SetActive(true);
            return item;
        }

        public void DestroyAll()
        {
            foreach (T gameObject in _pool)
            {
                gameObject.Dispose();
            }
        }
    }
}
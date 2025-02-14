using HH.Factory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace HH.Pool
{
    public abstract class PoolSO<T> : ScriptableObject, IPool<T>
    {
        protected readonly Stack<T> Available = new Stack<T>();
        public abstract IFactory<T> Factory { get; set; }
        protected bool HasBeenPrewarmed { get; set; }

        protected virtual T Create() 
        {
            return Factory.Create();
        }

        public virtual void Prewarm(int num)
        {
            if (HasBeenPrewarmed) { return; }
            
            for (int i = 0;i < num; i++) 
            {
                Available.Push(Create());
            }
            HasBeenPrewarmed = true;
        }

        public virtual T Request()
        {
            return Available.Count > 0 ? Available.Pop() : Create();
        }


        public virtual void Return(T member)
        {
            Available.Push(member);
        }

        public virtual void OnDisable()
        {
            Available.Clear();
            HasBeenPrewarmed = false;
        }
    }
}

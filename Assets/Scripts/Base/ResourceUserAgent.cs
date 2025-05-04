using UnityEngine;
using Interfaces;
using Holders;

namespace Base
{
    /// <summary>
    /// Clase base genérica para agentes que consumen un recurso (vida o maná) al usar habilidades.
    /// </summary>
    [RequireComponent(typeof(PlayableStatHolder))]
    public abstract class ResourceUserAgent : MonoBehaviour, IResourceUser
    {
        protected PlayableStatHolder holder;

        protected virtual void Awake()
        {
            holder = GetComponent<PlayableStatHolder>();
        }

        protected abstract int CurrentResource { get; }
        protected abstract int MaxResource { get; }
        protected abstract void ConsumeResource(int amount);
        protected abstract void Recover(int amount);

        public int GetCurrentResource() => CurrentResource;
        public int GetMaxResource() => MaxResource;

        public void UseResource(int amount) => ConsumeResource(amount);
        public void RecoverResource(int amount) => Recover(amount);
    }
}
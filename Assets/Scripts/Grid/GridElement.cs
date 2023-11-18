using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridElement : MonoBehaviour, IPoolable<IMemoryPool>
    {
        private IMemoryPool _pool;

        public void Despawn()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            gameObject.SetActive(true);
        }

        public class Factory : PlaceholderFactory<GridElement> { }
    }
}
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridElement : MonoBehaviour, IPoolable<IMemoryPool>
    {
        private IMemoryPool _pool;


        public void OnDespawned()
        {
            _pool.Despawn(this);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public class Factory : PlaceholderFactory<GridElement> { }
    }
}
using UnityEngine;
using Utility.Vector;
using Zenject;

namespace Grid
{
    public class GridElement : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private GameObject _xObject;

        public bool IsMarked { get; private set; }

        private IMemoryPool _pool;
        private Grid _grid;

        public void Initialize(Vector2Int pos, Grid grid)
        {
            transform.position = pos.ToVector3();
            _grid = grid;
        }

        public void SetMarked()
        {
            if (IsMarked)
            {
                return;
            }

            _xObject.SetActive(true);
            _grid.InformChange(this);
            IsMarked = true;

            _grid.InformChange(this);
        }

        public void SetUnmarked()
        {
            _xObject.SetActive(false);
            IsMarked = false;
        }


        #region Pool

        public void Despawn()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            gameObject.SetActive(false);
            _xObject.SetActive(false);
            IsMarked = false;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
            gameObject.SetActive(true);
            IsMarked = false;
        }

        #endregion

        #region Factory

        public class Factory : PlaceholderFactory<GridElement> { }

        #endregion
    }
}
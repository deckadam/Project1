using System.Collections.Generic;
using Input.Camera;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class Grid : MonoBehaviour
    {
        private GridElement.Factory _gridElementFactory;
        private Dictionary<Vector2Int, GridElement> _activeElements = new Dictionary<Vector2Int, GridElement>();
        private CameraAdjuster _adjuster;

        [Inject]
        private void Inject(GridElement.Factory gridElementFactory, CameraAdjuster adjuster)
        {
            _gridElementFactory = gridElementFactory;
            _adjuster = adjuster;
        }

        private void Awake()
        {
            Initialize(3);
        }

        [Button]
        public void Initialize(int gridSize)
        {
            foreach (var activeElement in _activeElements)
            {
                activeElement.Value.Despawn();
            }

            _activeElements.Clear();

            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    var newGridElement = _gridElementFactory.Create();
                    var pos = new Vector2Int(i, j);
                    newGridElement.Initialize(pos, this);
                    _activeElements[pos] = newGridElement;
                }
            }

            _adjuster.AdjustTargetGroup(new GridElement[]
            {
                _activeElements[Vector2Int.zero],
                _activeElements[new Vector2Int(gridSize-1, gridSize-1)]
            });
        }

        public void InformChange(GridElement changedElement) { }
    }
}
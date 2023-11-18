using System.Collections.Generic;
using DefaultNamespace.Camera;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class Grid : MonoBehaviour
    {
        private GridElement.Factory _gridElementFactory;
        private List<GridElement> _activeElements = new List<GridElement>();
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
                activeElement.Despawn();
            }

            _activeElements.Clear();

            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    var newGridElement = _gridElementFactory.Create();
                    newGridElement.transform.position = new Vector3(i, j, 0);
                    _activeElements.Add(newGridElement);
                }
            }

            _adjuster.AdjustTargetGroup(_activeElements);
        }
    }
}
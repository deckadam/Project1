using System;
using System.Collections.Generic;
using Input.Camera;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Grid
{
    public class GridSystem : MonoBehaviour
    {
        public event Action OnMatchEvent;
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
                _activeElements[new Vector2Int(gridSize - 1, gridSize - 1)]
            });
        }

        public void InformChange(GridElement changedElement)
        {
            var connectedElements = new List<GridElement> { changedElement };
            var items = GetConnectedElements(changedElement, connectedElements);

            if (items.Count < 3)
            {
                return;
            }

            foreach (var gridElement in items)
            {
                gridElement.SetUnmarked();
            }

            OnMatchEvent?.Invoke();
        }

        private List<GridElement> GetConnectedElements(GridElement baseItem, List<GridElement> connectedElements)
        {
            //Right
            if (IsValidElement(baseItem.Index + Vector2Int.right, connectedElements, out var rightElement))
            {
                connectedElements.Add(rightElement);
                GetConnectedElements(rightElement, connectedElements);
            }

            //Left
            if (IsValidElement(baseItem.Index + Vector2Int.left, connectedElements, out var leftElement))
            {
                connectedElements.Add(leftElement);
                GetConnectedElements(leftElement, connectedElements);
            }

            //Up
            if (IsValidElement(baseItem.Index + Vector2Int.up, connectedElements, out var upperElement))
            {
                connectedElements.Add(upperElement);
                GetConnectedElements(upperElement, connectedElements);
            }

            //Down
            if (IsValidElement(baseItem.Index + Vector2Int.down, connectedElements, out var downElement))
            {
                connectedElements.Add(downElement);
                GetConnectedElements(downElement, connectedElements);
            }

            return connectedElements;
        }

        private bool IsValidElement(Vector2Int index, List<GridElement> list, out GridElement result)
        {
            return _activeElements.TryGetValue(index, out result) &&
                   result.IsMarked &&
                   !list.Contains(result);
        }
    }
}
using Grid;
using UnityEngine;
using Zenject;

namespace InputSystem.InputManager
{
    public class InputManager : MonoBehaviour
    {
        private Camera _camera;

        [Inject]
        private void Inject(Camera camera)
        {
            _camera = camera;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Physics.Simulate(Time.fixedDeltaTime);
                var mousePosition = UnityEngine.Input.mousePosition;
                var convertedPosition = new Vector3(mousePosition.x, mousePosition.y, 10);
                var point = _camera.ScreenToWorldPoint(convertedPosition);
                var hit = Physics2D.Raycast(point, Vector2.zero);
                if (hit.collider != null && hit.transform.TryGetComponent<GridElement>(out var result))
                {
                    result.SetMarked();
                }
            }
        }
    }
}
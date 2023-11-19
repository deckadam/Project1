using Cinemachine;
using Grid;
using Input.Camera;
using UnityEngine;
using Zenject;

namespace Input
{
    public class ContextInstaller : MonoInstaller
    {
        [SerializeField]private GridElement _gridElementPrefab;
        [SerializeField]private CinemachineTargetGroup _cinemachineTargetGroup;
        [SerializeField] private UnityEngine.Camera _camera;
        
        public override void InstallBindings()
        {
            Container.BindFactory<GridElement, GridElement.Factory>().
                FromPoolableMemoryPool(x => x.WithInitialSize(9).
                    FromComponentInNewPrefab(_gridElementPrefab).
                    UnderTransformGroup("GridElementPool"));

            Container.BindInstance(_cinemachineTargetGroup).AsSingle();
            Container.BindInstance(_camera).AsSingle();
            Container.Bind<CameraAdjuster>().AsSingle();
        }
    }
}
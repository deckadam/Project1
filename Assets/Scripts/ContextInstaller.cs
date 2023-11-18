using Grid;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class ContextInstaller : MonoInstaller
    {
        [SerializeField]private GridElement _gridElementPrefab;
        public override void InstallBindings()
        {
            Container.BindFactory<GridElement, GridElement.Factory>().
                FromPoolableMemoryPool(x => x.WithInitialSize(9).
                    FromComponentInNewPrefab(_gridElementPrefab).
                    UnderTransformGroup("GridElementPool"));
        }
    }
}
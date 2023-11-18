using System.Collections.Generic;
using Cinemachine;
using Grid;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Camera
{
    public class CameraAdjuster
    {
        private CinemachineTargetGroup _targetGroup;
        private GridElement[] _activeTargets = new GridElement[2];

        [Inject]
        private void Inject(CinemachineTargetGroup targetGroup)
        {
            _targetGroup = targetGroup;
        }

        public void AdjustTargetGroup(List<GridElement> targets)
        {
            foreach (var t in _targetGroup.m_Targets)
            {
                _targetGroup.RemoveMember(t.target);
            }

            var outliners = GetOutliners(targets);
            _activeTargets = outliners;
            
            foreach (var activeTarget in _activeTargets)
            {
                _targetGroup.AddMember(activeTarget.transform, 1f, 1f);
            }
        }

        private GridElement[] GetOutliners(List<GridElement> elements)
        {
            var outliners = new GridElement[2]
            {
                elements[0],
                elements[^1]
            };

            return outliners;
        }
    }
}
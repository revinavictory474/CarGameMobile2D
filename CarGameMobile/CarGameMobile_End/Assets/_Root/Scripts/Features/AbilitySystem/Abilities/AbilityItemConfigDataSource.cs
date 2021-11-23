using System.Collections.Generic;
using UnityEngine;

namespace Features.AbilitySystem.Abilities
{
    [CreateAssetMenu(
        fileName = nameof(AbilityItemConfigDataSource),
        menuName = "Configs/" + nameof(AbilityItemConfigDataSource))]
    internal class AbilityItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private AbilityItemConfig[] _abilityConfigs;

        public IReadOnlyList<AbilityItemConfig> AbilityConfigs => _abilityConfigs;
    }
}

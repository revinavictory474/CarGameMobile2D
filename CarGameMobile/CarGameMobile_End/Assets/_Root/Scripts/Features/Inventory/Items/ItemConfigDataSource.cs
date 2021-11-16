using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfigDataSource), menuName = "Configs/" + nameof(ItemConfigDataSource))]
    internal class ItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _items;

        public IReadOnlyList<ItemConfig> Items => _items;
    }
}
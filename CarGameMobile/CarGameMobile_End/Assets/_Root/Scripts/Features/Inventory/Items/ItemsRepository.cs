using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Inventory.Items
{
    internal interface IItemsRepository : IDisposable
    {
        IReadOnlyDictionary<string, IItem> Items { get; }
    }

    internal class ItemsRepository : IItemsRepository
    {
        private readonly Dictionary<string, IItem> _items;

        public IReadOnlyDictionary<string, IItem> Items => _items;

        private bool _isDisposed;

        public ItemsRepository(IEnumerable<ItemConfig> configs) =>
            _items = CreateCollection(configs);
        
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _items.Clear();
        }


        private Dictionary<string, IItem> CreateCollection(IEnumerable<ItemConfig> configs)
        {
            var collection = new Dictionary<string, IItem>();

            foreach (ItemConfig config in configs)
            {
                collection[config.Id] = CreateItem(config);
            }
            return collection;
        }

        private static Item CreateItem(ItemConfig config) =>
            new Item
            (
                config.Id,
                new ItemInfo
                (
                    config.Title,
                    config.Icon
                )
            );

    }
}
using Features.Inventory.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tool
{
    internal class ContentDataSourceLoader 
    {
        public static ItemConfig[] LoadItemConfigs(ResourcePath resourcePath)
        {
            var dataSource = ResourcesLoader.LoadObject<ItemConfigDataSource>(resourcePath);
            return dataSource == null ? Array.Empty<ItemConfig>() : dataSource.Items.ToArray();
        }

        //public static UpgradeItemConfig[] LoadUpgradeItemConfigs(ResourcePath resourcePath)
        //{
        //    var dataSource = ResourcesLoader.LoadObject<UpgradeItemConfigDataSource>(resourcePath);
        //    return dataSource == null ? Array.Empty<UpgradeItemConfig>() : dataSource.ItemConfigs.ToArray();
        //}

        //public static AbilityItemConfig[] LoadAbilityItemConfigs(ResourcePath resourcePath)
        //{
        //    var dataSource = ResourcesLoader.LoadObject<AbilityItemConfigDataSource>(resourcePath);
        //    return dataSource == null ? Array.Empty<AbilityItemConfig>() : dataSource.ItemConfigs.ToArray();
        //}
    }
}
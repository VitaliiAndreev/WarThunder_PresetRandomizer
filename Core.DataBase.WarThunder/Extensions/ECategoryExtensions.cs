﻿using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class ECategoryExtensions
    {
        public static bool IsValid(this ECategory category) =>
            category.EnumerationItemValueIsPositive();

        public static IEnumerable GetItems<T>(this ECategory category)
        {
            return category switch
            {
                ECategory.RepairCost => typeof(ERepairCost).GetEnumerationItems<T>(true),
                _ => new List<T>(),
            };
        }
    }
}
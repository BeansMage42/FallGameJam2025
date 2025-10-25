using UnityEngine;
using System;
[System.Serializable]

public struct IngredientList
{  // Structure declaration
    [Flags]
    public enum IngredientType
    {
        Meat = 0,
        Greens = 1,
        Dairy = 2,
        Bread = 4,
        Sweet = 8,
        Liquid = 16,

    }
    public IngredientType ingredientType;
}
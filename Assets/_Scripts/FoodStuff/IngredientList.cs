using UnityEngine;
using System;
[System.Serializable]


 // Structure declaration
    [Flags]
    public enum IngredientType
    {
        Meat = 1,
        Greens = 2,
        Dairy = 4,
        Bread = 8,
        Sweet = 16,
        Liquid = 32,

    }

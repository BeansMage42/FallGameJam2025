using UnityEngine;
using System;


public class ingredients : MonoBehaviour
{
    [SerializeField] private IngredientList _ingredient = new IngredientList();
    public IngredientList getIngriedients()
    {
        return _ingredient;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print(getIngriedients().ingredientType);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



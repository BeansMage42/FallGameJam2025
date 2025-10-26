using UnityEngine;

[CreateAssetMenu(fileName = "OrderSO", menuName = "Scriptable Objects/OrderSO")]
public class OrderSO : ScriptableObject
{

    [SerializeField] private string orderName;
    [SerializeField] private IngredientType orderIngredients;
    [SerializeField] public GameObject vomitObject;

    public string GetOrdername()
    {
        return orderName;
    }
    public IngredientType GetOrderIngredients()
    {
        return orderIngredients;
    }

}

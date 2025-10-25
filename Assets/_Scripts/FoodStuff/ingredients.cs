using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class ingredients : MonoBehaviour
{
    [SerializeField] private IngredientType _ingredient = new IngredientType();
    Interactable _interactable;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        _interactable.InteractedWith += OnInteract;
    }

    void Start()
    {
       // print(GetIngriedients());
    }
    public IngredientType GetIngriedients()
    {
        return _ingredient;
    }

    private void OnInteract(Interaction playerInteraction)
    {
        Debug.Log("player interacted");
        gameObject.SetActive(false);
    }
    

}



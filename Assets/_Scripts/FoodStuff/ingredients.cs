using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class ingredients : MonoBehaviour
{
    [SerializeField] private IngredientType _ingredient = new IngredientType();
    Interactable _interactable;
    public Action Eat;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
    }

    private void OnEnable()
    {
        _interactable.InteractedWith += OnInteract;
    }
    private void OnDisable()
    {
        _interactable.InteractedWith -= OnInteract;
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
        Eat?.Invoke();
        playerInteraction.AteSomething(this);
        gameObject.SetActive(false);
        _interactable.PlayInteractSound();
    }
    

}



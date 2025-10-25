using System;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    private Interactable _foundInteractable;
    [Header("Interaction")]
    [SerializeField] private float _reach;
    [SerializeField] private float _radius;
    [SerializeField] private Image _image;
    [SerializeField] private OrderSO _currentOrder;
    private IngredientType _currentOrderProgress = new IngredientType();
    private List<GameObject> _stomach = new List<GameObject>();
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started && _foundInteractable != null)
        {
            _stomach.Add(_foundInteractable.gameObject);
            CompareIngredients(_foundInteractable.GetComponent<ingredients>().GetIngriedients());
            _foundInteractable.InteractedWith?.Invoke(this);
            
        }
    }
    
    private void Update()
    {
        if (Physics.SphereCast(transform.position, _radius, transform.forward, out RaycastHit hit,_reach ))
        {
                if (hit.collider.TryGetComponent(out Interactable interactable)) 
                {
                _image.color = Color.green;
                    _foundInteractable = interactable;
                   // Debug.Log(hit.collider.name);
                }
            else
            {
                _foundInteractable = null;
                _image.color = Color.red;

               // Debug.Log("hit something but its not what you want");
            }
        }
        else
        {
            _foundInteractable = null;
            _image.color = Color.red;
           // Debug.Log("no");
        }
        
    }

    public void CompareIngredients(IngredientType other)
    {
        /*if(currentOrder.GetOrderIngredients() == other)
        {
            Debug.Log("same");
        }*/

        IngredientType[] test = InteractionHelpers.GetFlags(other);
        for (int i = 0; i < test.Length; i++) 
        {
            
                if ((_currentOrderProgress & test[i]) == 0)
                {
                    _currentOrderProgress |= test[i];
                    break;
                }
                else
                {
                    print("not already added");
                }
            
           
        }
        print(_currentOrderProgress);
        if((_currentOrderProgress & _currentOrder.GetOrderIngredients())== _currentOrder.GetOrderIngredients())
        {
            print("orderCompleted");
        }
    }
}

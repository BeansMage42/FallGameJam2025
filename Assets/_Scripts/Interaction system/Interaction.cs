using System;
using System.Collections.Generic;
using System.Collections;
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
    [SerializeField] private Sprite _mouth;
    [SerializeField] private Sprite _dot;
    [SerializeField] private float _maxTime;
    [SerializeField] private float _vomitTime;
    private bool _isBeingWatched;
    private bool _isVomiting;
    private float _currentTimer;
    private bool _hasEaten;
    public OrderSO _currentOrder;
    private IngredientType _currentOrderProgress = new IngredientType();
    private List<GameObject> _stomach = new List<GameObject>();
    public  Action completedOrder;
    
    AudioSource interactionSource;
    private Chef vomitScript;

    private void Start()
    {

        UIManager.Instance.ClearOrder();
        interactionSource = GetComponent<AudioSource>();
        vomitScript = GetComponent<Chef>();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started && _foundInteractable != null)
        {
            Debug.Log("interact");
            if (_foundInteractable.TryGetComponent(out ingredients ingredient) && _currentOrder == null && ingredient.isActiveAndEnabled) return;
            _foundInteractable.InteractedWith?.Invoke(this);
        }
    }
    public void SetOrder(OrderSO newOrder)
    {
        _currentOrder = newOrder;
        UIManager.Instance.UpdateOrderGUI(newOrder, _currentOrderProgress);
    }
    
    public void AteSomething(ingredients ingredients)
    { 
        if (_stomach.Count == 0)
        {
            _currentTimer = _maxTime;
        }
        _stomach.Add(ingredients.gameObject);
        CompareIngredients(ingredients.GetIngriedients());
        UIManager.Instance.UpdateOrderGUI(_currentOrder, _currentOrderProgress);
    }
    
    private void Update()
    {
        if(_stomach.Count > 0)
        {
            if(_currentTimer > 0)
            {
                _currentTimer-=Time.deltaTime;
                UIManager.Instance.UpdateBarfMeter(_currentTimer / _maxTime);
            }
            else
            {
                Vomit();
            }
        }

        if (Physics.SphereCast(transform.position, _radius, transform.forward, out RaycastHit hit,_reach ))
        {
                if (hit.collider.TryGetComponent(out Interactable interactable)) 
                {
                _image.sprite = _mouth;
                _image.gameObject.transform.localScale = new Vector3(1, 1);
                    _foundInteractable = interactable;
                   // Debug.Log(hit.collider.name);
                }
            else
            {
                _foundInteractable = null;
                _image.sprite = _dot;
                _image.gameObject.transform.localScale = new Vector3(0.5f, 0.5f);

                // Debug.Log("hit something but its not what you want");
            }
        }
        else
        {
            _foundInteractable = null;

            _image.sprite = _dot;
            _image.gameObject.transform.localScale = new Vector3(0.5f, 0.5f);
            // Debug.Log("no");
        }
        if(_isBeingWatched && _isVomiting)
        {
            Debug.Log("seen");
        }
        
    }

    private void Vomit()
    {
        vomitScript.EjectFluid();
        _stomach.Clear();
        print(_currentOrderProgress);
        UIManager.Instance.ClearOrder();
        if ((_currentOrderProgress & _currentOrder.GetOrderIngredients()) == _currentOrder.GetOrderIngredients())
        {
            completedOrder?.Invoke();
            Instantiate(_currentOrder.vomitObject,transform.position + transform.forward*1.3f,Quaternion.identity);
            _currentOrder = null;
            print("orderCompleted");
        }
        else
        {

            UIManager.Instance.UpdateOrderGUI(_currentOrder, _currentOrderProgress);
        }

        _currentOrderProgress = new IngredientType();
        StartCoroutine(VomitTime());
    }
    private IEnumerator VomitTime()
    {
        _isVomiting = true;
        yield return new WaitForSeconds(_vomitTime);
        _isVomiting = false;
    }
    public bool GetIsVomiting()
    {
        return _isVomiting;
    }
    public void CompareIngredients(IngredientType other)
    {
        IngredientType[] test = InteractionHelpers.GetFlags(other);
        for (int i = 0; i < test.Length; i++) 
        {
                if ((_currentOrderProgress & test[i]) == 0)
                {
                    _currentOrderProgress |= test[i];
                    break;
                }
        }
        
    }
}

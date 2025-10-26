using UnityEngine;


[RequireComponent(typeof(Interactable))]
public class CustomerBehaviour : AIBehaviour
{
    [SerializeField] private Counter counter;
    private Interactable interactable;
    private ingredients ingredients;
    [SerializeField] private OrderSO customerOrder;
    private Interaction interactor;
    private bool isInLine = true;
    Transform seat;
    
    public override void Awake()
    {
        base.Awake();
        interactable = GetComponent<Interactable>();
        ingredients = GetComponent<ingredients>();

    }
    private void Start()
    {
        counter.AddAIToLine(this);
        if (counter.IsFirstCustomer(this))
        { 
            ingredients.enabled = false;
            interactable.InteractedWith += OnInteract;
        }
        TargetLocation(counter.LineEndPos());
        ingredients.Eat += Eat;
    }
    public void OnInteract(Interaction interaction)
    {
        print("customer Interacted with");
        interactor = interaction;
        interaction.completedOrder += OrderCompleted;
        interaction.SetOrder( customerOrder);
    }
    public void OrderCompleted()
    {
        interactor.completedOrder -= OrderCompleted;
        interactable.InteractedWith -= OnInteract;
        isInLine = false;
        seat = counter.GetEmptySeat();
        TargetLocation(seat.position);
        counter.RemoveCustomerFromLine(this);
        ingredients.enabled = true;
    }

    public override void MoveUpInLine()
    {
        Debug.Log("move up in line");
            if (counter.IsFirstCustomer(this))
            {
                ingredients.enabled = false;
                interactable.InteractedWith += OnInteract;
            }
            ingredients.Eat += Eat;
        
    }
    public void Eat()
    {
        if (isInLine)
        {
            counter.RemoveCustomerFromLine(this);
        }
        else
        {
            counter.ReturnEmptySeat(seat);
        }
    }

    
 }

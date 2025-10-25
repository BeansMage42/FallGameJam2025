using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //temporarily serialized. Optimally there is some sort of manager script or scriptable object that knows the correct layers that these pull from

    
    public Action<Interaction> InteractedWith;
    public Action IsFocused;
    public Action IsUnfocused;

    

}


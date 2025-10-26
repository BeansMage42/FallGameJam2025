using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Interactable : MonoBehaviour
{
    //temporarily serialized. Optimally there is some sort of manager script or scriptable object that knows the correct layers that these pull from

    
    public Action<Interaction> InteractedWith;
    public Action IsFocused;
    public Action IsUnfocused;

    [SerializeField] AudioClip [] interactSound;
    
    public AudioClip GetInteractSound()
    {
        if (interactSound.Length <= 0) return null;
        return interactSound[Random.Range(0, interactSound.Length - 1)];
    }
}


using UnityEngine;
using UnityEngine.VFX;

public class Chef : MonoBehaviour
{
    [SerializeField] private bool triggerVomit;
    [SerializeField] private GameObject vomitObject;

    [SerializeField] private VisualEffect blood;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (triggerVomit || timer > 2)
        {
            timer = 0;
            
            triggerVomit = false;
            //VomitObject vomit = Instantiate(vomitObject, transform.position, Quaternion.identity).GetComponent<VomitObject>();
            //vomit.StartVomit(transform.forward);
            ExpelFluid();
        }
    }

    void ExpelFluid()
    {
        blood.SetVector3("PlayerRotation", transform.rotation.eulerAngles);
        blood.Play();
    }
} 

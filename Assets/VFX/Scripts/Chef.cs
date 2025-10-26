using UnityEngine;
using UnityEngine.VFX;

public class Chef : MonoBehaviour
{
    [SerializeField] private bool triggerVomit;
    [SerializeField] private GameObject vomitObject;

    [SerializeField] private VisualEffect fluid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerVomit)
        {
            triggerVomit = false;
            //VomitObject vomit = Instantiate(vomitObject, transform.position, Quaternion.identity).GetComponent<VomitObject>();
            //vomit.StartVomit(transform.forward);
            
            EjectFluid();
        }
    }

    void EjectFluid()
    {
        fluid.SetVector3("SpitDirection", transform.forward + new Vector3(0, 0.3f, 0));
        fluid.SetVector3("Position", transform.position);
        fluid.Play();
    }
}

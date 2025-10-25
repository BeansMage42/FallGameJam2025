using UnityEngine;

public class Chef : MonoBehaviour
{
    [SerializeField] private bool triggerVomit;
    [SerializeField] private GameObject vomitObject;
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
            VomitObject vomit = Instantiate(vomitObject, transform.position, Quaternion.identity).GetComponent<VomitObject>();
            vomit.StartVomit(transform.forward);
        }
    }
}

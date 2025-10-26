using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Chef : MonoBehaviour
{
    [SerializeField] private bool triggerVomit;
    [SerializeField] private GameObject vomitObject;
    
    [SerializeField] private VisualEffect chunk;
    [SerializeField] private VisualEffect fluid;
    
    [SerializeField] private AudioSource barfSource;
    [SerializeField] private AudioClip [] barfSounds;

    private WaitForSeconds barfDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barfDelay = new WaitForSeconds(0.35f);
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
        barfSource.clip = barfSounds[Random.Range(0, 3)];
        barfSource.Play();
        
        StartCoroutine(BarfDelay());
    }

    IEnumerator BarfDelay()
    {
        yield return barfDelay;
   
        fluid.SetVector3("SpitDirection", transform.forward + new Vector3(0, 0.3f, 0));
        fluid.SetVector3("Position", transform.position);
        fluid.Play();
        
        chunk.SetVector3("SpitDirection", transform.forward + new Vector3(0, 0.3f, 0));
        chunk.SetVector3("Position", transform.position);
        chunk.Play();
    }
}

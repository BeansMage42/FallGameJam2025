using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //Singleton (carried between scenes)
    private void Awake() //Acts before Start()
    {
        if (Instance != null)
        {
            if(this != Instance)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() //Acts before Update()
    {
        
    }

    void Update()
    {
        
    }
}

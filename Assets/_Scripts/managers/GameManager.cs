using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private float storeRating = 50;
    private void Awake() //Acts before Start()
    {
        if (Instance != null)
        {
            if (this != Instance)
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
    private void Start()
    {
        UIManager.Instance.UpdateScore(storeRating);
    }

    public void AddScore(float addScore)
    {
        storeRating += addScore;
        storeRating = Mathf.Clamp(storeRating, -50, 100);
        UIManager.Instance.UpdateScore(storeRating);
    }
    public float GetScore()
    {
        return storeRating;
    }
    public void GameEnd()
    {
        Time.timeScale = 0;

    }
    public void CloseGame()
    {
        Application.Quit();
    }
}

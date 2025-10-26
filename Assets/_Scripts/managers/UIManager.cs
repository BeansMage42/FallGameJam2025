using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] TextMeshProUGUI _orderUI;
    [SerializeField] Image _barfMeter;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] GameObject _menuObject;
    [SerializeField] TextMeshProUGUI _finalScoreText;
    
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
        _menuObject.SetActive(false);
    }

    private void Timer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            GameOverMenu();
            GameManager.Instance.GameEnd();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
      
    }

    public void UpdateBarfMeter(float amount)
    {
        _barfMeter.fillAmount = amount;
    }
    public void UpdateScore(float amount)
    {
        _scoreText.text = $"Store rating: {amount} % ";
    }
    public void UpdateOrderGUI(OrderSO order, IngredientType currentOrder)
    {
        IngredientType[] ingredientsInOrder = InteractionHelpers.GetFlags(order.GetOrderIngredients());
        _orderUI.text = order.GetOrdername();
        foreach (IngredientType i in ingredientsInOrder)
        {
            if ((currentOrder & i) != 0)
            {
                _orderUI.text += $"\n -<s>{i.ToString()}</s>";
                continue;
            }
            _orderUI.text += "\n -" + i.ToString();
        }
    }

    public void ClearOrder()
    {
        _orderUI.text = "Press 'E' Talk to customer to get next order";
    }

    public void GameOverMenu()
    {
        _menuObject.SetActive(true);
        _finalScoreText.text = $"Final Score: {GameManager.Instance.GetScore()}";
    }

    void Update()
    {
        Timer();
    }
}

using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private CarController carController; // Ссылка на скрипт управления игроком

    private float countdownDuration = 3f; // Длительность обратного отсчета
    public float gameDuration; // Длительность игры в секундах
    private float timeLeft; // Оставшееся время игры

    private bool gameStarted = false; // Флаг, указывающий, началась ли игра
    private bool isCountingDown = true; // Флаг, указывающий, идет ли обратный отсчет

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (gameStarted && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();

            if (timeLeft <= 0)
            {
                gameStarted = false;
                EndGame(); 
            }
        }
    }

    private System.Collections.IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        int count = 3;

        isCountingDown = true;
        carController.enabled = false;

        Time.timeScale = 0;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f); 
            count--;
        }
        countdownText.gameObject.SetActive(false);

        isCountingDown = false;
        carController.enabled = true;

        Time.timeScale = 1;

        StartGame();
    }

    private void StartGame()
    {
        gameStarted = true;
        timeLeft = gameDuration;
        UpdateTimerText();
    }

    private void EndGame()
    {
        moneyManager.winCash.gameObject.SetActive(true);
        moneyManager.GetMoneyByDrifting(); 
        moneyManager.winCash.text = "You win: " + MoneyManager.winCashAmount.ToString(); 
        timerText.gameObject.SetActive(false);
        Time.timeScale = 0; 
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

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
        // Начать обратный отсчет перед началом игры
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        // Проверяем, началась ли игра и не закончился ли обратный отсчет
        if (gameStarted && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerText();

            if (timeLeft <= 0)
            {
                gameStarted = false;
                EndGame(); // Завершаем игру
            }
        }
    }

    // Обратный отсчет перед началом игры
    private System.Collections.IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        int count = 3;

        // Отключаем управление игроком
        isCountingDown = true;
        carController.enabled = false;

        // Замораживаем время
        Time.timeScale = 0;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f); // Используем WaitForSecondsRealtime для обхода замороженного времени
            count--;
        }
        countdownText.gameObject.SetActive(false);

        // Включаем управление игроком после обратного отсчета
        isCountingDown = false;
        carController.enabled = true;

        // Восстанавливаем нормальное время
        Time.timeScale = 1;

        // Начать игру после обратного отсчета
        StartGame();
    }

    // Начать игру и запустить таймер
    private void StartGame()
    {
        gameStarted = true;
        timeLeft = gameDuration;
        UpdateTimerText();
    }

    // Завершить игру
    private void EndGame()
    {
        moneyManager.winCash.gameObject.SetActive(true);
        moneyManager.GetMoneyByDrifting(); // Получаем кэш после завершения игры
        moneyManager.winCash.text = MoneyManager.winCashAmount.ToString(); // Устанавливаем текст кэша после получения кэша
        timerText.gameObject.SetActive(false);
        // Остановить игру или выполнить другие действия по вашему выбору
        Time.timeScale = 0; // Остановить время
    }

    // Обновить текст таймера
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

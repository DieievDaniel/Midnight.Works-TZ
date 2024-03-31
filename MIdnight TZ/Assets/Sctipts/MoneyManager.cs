using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cashText;
    public TextMeshProUGUI winCash;
    private DriftManager driftManager;

    // Статическое поле для хранения значения кэша
    public static int cashAmount = 0;
    public static int winCashAmount = 0;

    void Start()
    {
        driftManager = FindObjectOfType<DriftManager>(); // Получаем ссылку на DriftManager
        UpdateCashText();
    }

    public void GetMoneyByDrifting()
    {
        winCashAmount = (int)driftManager.totalScore / 10; // Рассчитываем кэш за дрифт
        cashAmount += winCashAmount; // Добавляем кэш к общему кэшу
        UpdateCashText();
    }

    public void UpdateCashText()
    {
        cashText.text = "Cash: " + cashAmount.ToString(); // Обновляем текстовое поле с кэшем
    }
}
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cashText;
    public TextMeshProUGUI winCash;
    private DriftManager driftManager;

    public static int cashAmount = 0;
    public static int winCashAmount = 0;

    void Start()
    {
        driftManager = FindObjectOfType<DriftManager>(); 
        UpdateCashText();
    }

    public void GetMoneyByDrifting()
    {
        winCashAmount = (int)driftManager.totalScore / 10; 
        cashAmount += winCashAmount; 
        UpdateCashText();
    }

    public void UpdateCashText()
    {
        cashText.text = "Cash: " + cashAmount.ToString(); 
    }
}
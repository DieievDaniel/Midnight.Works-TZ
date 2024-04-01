using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
class SaveData
{
    public int totalCashAmount;
    public bool addedCashAfterLastGame;

    public SaveData(int totalCash, bool addedCashAfterLast)
    {
        totalCashAmount = totalCash;
        addedCashAfterLastGame = addedCashAfterLast;
    }
}

public class SaveSystem : MonoBehaviour
{
    private string filePath;
    private MoneyManager moneyManager;

    void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void SaveGame()
    {
        int totalCash = MoneyManager.cashAmount;
        bool addedCashAfterLastGame = MoneyManager.winCashAmount > 0;

        if (totalCash > 0)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            SaveData saveData = new SaveData(totalCash, addedCashAfterLastGame);
            binaryFormatter.Serialize(fileStream, saveData);
            fileStream.Close();
        }
    }

    public void LoadGame()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            if (saveData.totalCashAmount > 0)
            {
                MoneyManager.cashAmount = saveData.totalCashAmount;
                moneyManager.UpdateCashText();

                if (saveData.addedCashAfterLastGame)
                {
                    MoneyManager.winCashAmount = 0;
                }
            }
        }
        else
        {
            Debug.LogError("Save file not found.");
        }
    }
}

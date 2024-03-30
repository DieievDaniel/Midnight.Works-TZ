using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string carObjectName;
    public string nextSceneName;
    public Canvas canvasToDisable; // Ссылка на Canvas, который нужно отключить

    public void LoadNextSceneWithCar()
    {
        // Отключаем Canvas перед загрузкой следующей сцены
        if (canvasToDisable != null)
        {
            canvasToDisable.gameObject.SetActive(false);
        }

        // Ищем машину по имени в текущей сцене
        GameObject car = GameObject.Find(carObjectName);

        // Проверяем, найдена ли машина
        if (car != null)
        {
            // Переносим машину в следующую сцену
            DontDestroyOnLoad(car);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Car object with name " + carObjectName + " not found in the scene!");
        }
    }
}

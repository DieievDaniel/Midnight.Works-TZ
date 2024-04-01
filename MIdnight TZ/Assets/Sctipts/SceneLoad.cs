using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoad : MonoBehaviour
{
    public string nextSceneName;
    public Vector3 carPosition;
    public GameObject carObject;
    public Canvas gear;
    public Transform spawnPoint; // Новая переменная для ссылки на точку спавна

    public void LoadNextSceneWithCar(string carObjectName)
    {
        // Найдем машину по имени
        carObject = GameObject.Find("EvoX");

        if (carObject != null)
        {
            carPosition = carObject.transform.position;

            // Отключаем гравитацию
            Physics.gravity = Vector3.zero;

            // Сохраняем машину, чтобы она не уничтожалась при загрузке следующей сцены
            DontDestroyOnLoad(carObject);
            gear.gameObject.SetActive(false);
            // Загружаем следующую сцену
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Car object with name " + carObjectName + " not found in the scene!");
        }
    }

    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadJoinRoom(string carObjectName)
    {
        GameObject carObject = GameObject.Find("EvoX");

        if (carObject != null)
        {
            // Сохраняем позицию машины
            carPosition = carObject.transform.position;

            // Сохраняем машину, чтобы она не уничтожалась при загрузке следующей сцены
            DontDestroyOnLoad(carObject);
            gear.gameObject.SetActive(false);
            // Загружаем следующую сцену
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Car object with name " + carObjectName + " not found in the scene!");
        }
        PlayerPrefs.SetString("EvoX", carObjectName);
        SceneManager.LoadScene("JoinRoom");
    }

    public void BackFromSettingsToMenu()
    {
        SceneManager.LoadScene("GarageScene");
    }

    IEnumerator DelayedBackToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GarageScene");
    }

    void OnLevelWasLoaded(int level)
    {
        // Проверяем, загружена ли та сцена, куда мы хотим загрузить машину
        if (SceneManager.GetActiveScene().name == nextSceneName)
        {
            // Находим машину по имени
            GameObject carObject = GameObject.Find("EvoX");

            // Если машина найдена и у нее есть трансформ, устанавливаем сохраненную позицию
            if (carObject != null && carObject.transform != null)
            {
                carObject.transform.position = carPosition;
            }
            else
            {
                Debug.LogError("Car object not found or does not have a transform component!");
            }

            // Устанавливаем гравитацию
            Physics.gravity = new Vector3(0, -9.81f, 0);

            // Вызываем корутину для задержки перед возвратом в меню
            StartCoroutine(DelayedBackToMenu());
        }
    }

    public void BackToMenu()
    {
        GameObject evoXObject = GameObject.Find("EvoX");
        Debug.Log("Back to menu button clicked.");
        // Если объект найден, уничтожить его
        if (evoXObject != null)
        {
            Destroy(evoXObject);
        }
        else
        {
            Debug.LogWarning("Object named 'EvoX' not found.");
        }
        SceneManager.LoadScene("GarageScene");
    }

    public void JoinSoloGame(string carObjectName)
    {
        GameObject carObject = GameObject.Find("EvoX");

        if (carObject != null)
        {
            // Сохраняем позицию машины
            carPosition = carObject.transform.position;

            // Сохраняем машину, чтобы она не уничтожалась при загрузке следующей сцены
            DontDestroyOnLoad(carObject);
            gear.gameObject.SetActive(false);
            // Загружаем следующую сцену
            if (spawnPoint != null) // Проверяем, что точка спавна установлена
            {
                carObject.transform.position = spawnPoint.position; // Устанавливаем позицию спавна
            }
            SceneManager.LoadScene("RoadGameScene");
        }
    }
}

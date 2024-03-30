using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string nextSceneName;
    private Vector3 carPosition;

    public void LoadNextSceneWithCar(string carObjectName)
    {
        // Найдем машину по имени
        GameObject carObject = GameObject.Find("EvoX");

        if (carObject != null)
        {
            // Сохраняем позицию машины
            carPosition = carObject.transform.position;

            // Отключаем гравитацию
            Physics.gravity = Vector3.zero;

            // Сохраняем машину, чтобы она не уничтожалась при загрузке следующей сцены
            DontDestroyOnLoad(carObject);

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

    // Вызывается после загрузки сцены
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
        }
    }
}

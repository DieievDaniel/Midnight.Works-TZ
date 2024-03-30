using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string nextSceneName;
    private Vector3 carPosition;

    public void LoadNextSceneWithCar(string carObjectName)
    {
        // ������ ������ �� �����
        GameObject carObject = GameObject.Find("EvoX");

        if (carObject != null)
        {
            // ��������� ������� ������
            carPosition = carObject.transform.position;

            // ��������� ����������
            Physics.gravity = Vector3.zero;

            // ��������� ������, ����� ��� �� ������������ ��� �������� ��������� �����
            DontDestroyOnLoad(carObject);

            // ��������� ��������� �����
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
            // ��������� ������� ������
            carPosition = carObject.transform.position;

            // ��������� ������, ����� ��� �� ������������ ��� �������� ��������� �����
            DontDestroyOnLoad(carObject);

            // ��������� ��������� �����
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

    // ���������� ����� �������� �����
    void OnLevelWasLoaded(int level)
    {
        // ���������, ��������� �� �� �����, ���� �� ����� ��������� ������
        if (SceneManager.GetActiveScene().name == nextSceneName)
        {
            // ������� ������ �� �����
            GameObject carObject = GameObject.Find("EvoX");

            // ���� ������ ������� � � ��� ���� ���������, ������������� ����������� �������
            if (carObject != null && carObject.transform != null)
            {
                carObject.transform.position = carPosition;
            }
            else
            {
                Debug.LogError("Car object not found or does not have a transform component!");
            }

            // ������������� ����������
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }
}

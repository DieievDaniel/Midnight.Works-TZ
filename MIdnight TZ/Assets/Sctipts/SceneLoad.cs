using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoad : MonoBehaviour
{
    public string nextSceneName;
    public Vector3 carPosition;
    public GameObject carObject;
    public Canvas gear;
    public Transform spawnPoint; // ����� ���������� ��� ������ �� ����� ������

    public void LoadNextSceneWithCar(string carObjectName)
    {
        // ������ ������ �� �����
        carObject = GameObject.Find("EvoX");

        if (carObject != null)
        {
            carPosition = carObject.transform.position;

            // ��������� ����������
            Physics.gravity = Vector3.zero;

            // ��������� ������, ����� ��� �� ������������ ��� �������� ��������� �����
            DontDestroyOnLoad(carObject);
            gear.gameObject.SetActive(false);
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
            gear.gameObject.SetActive(false);
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

    IEnumerator DelayedBackToMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("GarageScene");
    }

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

            // �������� �������� ��� �������� ����� ��������� � ����
            StartCoroutine(DelayedBackToMenu());
        }
    }

    public void BackToMenu()
    {
        GameObject evoXObject = GameObject.Find("EvoX");
        Debug.Log("Back to menu button clicked.");
        // ���� ������ ������, ���������� ���
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
            // ��������� ������� ������
            carPosition = carObject.transform.position;

            // ��������� ������, ����� ��� �� ������������ ��� �������� ��������� �����
            DontDestroyOnLoad(carObject);
            gear.gameObject.SetActive(false);
            // ��������� ��������� �����
            if (spawnPoint != null) // ���������, ��� ����� ������ �����������
            {
                carObject.transform.position = spawnPoint.position; // ������������� ������� ������
            }
            SceneManager.LoadScene("RoadGameScene");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string carObjectName;
    public string nextSceneName;
    public Canvas canvasToDisable; // ������ �� Canvas, ������� ����� ���������

    public void LoadNextSceneWithCar()
    {
        // ��������� Canvas ����� ��������� ��������� �����
        if (canvasToDisable != null)
        {
            canvasToDisable.gameObject.SetActive(false);
        }

        // ���� ������ �� ����� � ������� �����
        GameObject car = GameObject.Find(carObjectName);

        // ���������, ������� �� ������
        if (car != null)
        {
            // ��������� ������ � ��������� �����
            DontDestroyOnLoad(car);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Car object with name " + carObjectName + " not found in the scene!");
        }
    }
}

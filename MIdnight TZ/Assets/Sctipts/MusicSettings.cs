using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;

    private const string volumeKey = "MusicVolume";

    void Start()
    {
        // ��������� ����������� �������� ��������� ��� ������������� �������� �� ��������� (��������, 0.5)
        float savedVolume = PlayerPrefs.GetFloat(volumeKey, 0.5f);
        slider.value = savedVolume;
        audio.volume = savedVolume;
    }

    void Update()
    {
        // ��������� ��������� � ����������� �� �������� ��������
        audio.volume = slider.value;
        // ��������� �������� ���������
        PlayerPrefs.SetFloat(volumeKey, slider.value);
        PlayerPrefs.Save(); // ���������� ������� ����� Save ��� ���������� ���������
    }
}

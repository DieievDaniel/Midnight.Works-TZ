using UnityEngine;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;

    private const string volumeKey = "MusicVolume";

    void Start()
    {
        // Загружаем сохраненное значение громкости или устанавливаем значение по умолчанию (например, 0.5)
        float savedVolume = PlayerPrefs.GetFloat(volumeKey, 0.5f);
        slider.value = savedVolume;
        audio.volume = savedVolume;
    }

    void Update()
    {
        // Обновляем громкость в зависимости от значения слайдера
        audio.volume = slider.value;
        // Сохраняем значение громкости
        PlayerPrefs.SetFloat(volumeKey, slider.value);
        PlayerPrefs.Save(); // Необходимо вызвать метод Save для сохранения изменений
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsGear : MonoBehaviour
{
    public string partName;
    private GarageCamera garageCamera;
  
    void Start()
    {
        garageCamera = FindObjectOfType<GarageCamera>();
    }

  

    public void SelectItem()
    {
        garageCamera.LookAt(transform);
        FindObjectOfType<PartsManagerUI>().GoToSelectMenu(partName);
    }

}
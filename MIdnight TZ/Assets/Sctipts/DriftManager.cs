using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class DriftManager : MonoBehaviour
{
    public TMP_Text totalScoreText;
    public TMP_Text currentScoreText;
    public TMP_Text factorText;
    public TMP_Text driftAngleText;

    public float minimumSpeed = 5;
    public float minimumAngle = 10;
    public float driftingDelay = 0.2f;
    public GameObject driftingObject;
    public Color normalDriftColor;
    public Color nearStopColor;
    public Color driftEndedColor;

    private Rigidbody playerRB;
    private float speed = 0;
    private float driftAngle = 0;
    private float driftFactor = 1;
    private float currentScore;
    private float totalScore;
    private bool isDrifting = false;
    private IEnumerator stopDriftingCoroutine = null;

    void Start()
    {
        driftingObject.SetActive(false);
        // ������� ������ � ����� "Car" � �������� ��� ��������� Rigidbody
        GameObject car = GameObject.FindGameObjectWithTag("Car");
        if (car != null)
        {
            playerRB = car.GetComponent<Rigidbody>();
            if (playerRB == null)
                Debug.LogError("Rigidbody component not found on the car object!");
        }
        else
        {
            Debug.LogError("Car object not found in the scene!");
        }
    }

    void Update()
    {
        ManageDrift();
        ManageUI();
    }

    void ManageDrift()
    {
        speed = playerRB.velocity.magnitude;
        driftAngle = Vector3.Angle(playerRB.transform.forward, (playerRB.velocity + playerRB.transform.forward).normalized);
        if (driftAngle > 120)
        {
            driftAngle = 0;
        }
        if (driftAngle >= minimumAngle && speed > minimumSpeed)
        {
            if (!isDrifting || stopDriftingCoroutine != null)
            {
                StartDrift();
            }
        }
        else
        {
            if (isDrifting && stopDriftingCoroutine == null)
            {
                StopDrift();
            }
        }
        if (isDrifting)
        {
            currentScore += Time.deltaTime * driftAngle * driftFactor;
            driftFactor += Time.deltaTime;
            driftingObject.SetActive(true);
        }
    }

    async void StartDrift()
    {
        if (!isDrifting)
        {
            await Task.Delay(Mathf.RoundToInt(1000 * driftingDelay));
            driftFactor = 1;
        }
        if (stopDriftingCoroutine != null)
        {
            StopCoroutine(stopDriftingCoroutine);
            stopDriftingCoroutine = null;
        }
        currentScoreText.color = normalDriftColor;
        isDrifting = true;
    }

    void StopDrift()
    {
        stopDriftingCoroutine = StoppingDrift();
        StartCoroutine(stopDriftingCoroutine);
    }

    private IEnumerator StoppingDrift()
    {
        yield return new WaitForSeconds(0.1f);
        currentScoreText.color = nearStopColor;
        yield return new WaitForSeconds(driftingDelay * 4f);
        totalScore += currentScore;
        isDrifting = false;
        currentScoreText.color = driftEndedColor;
        yield return new WaitForSeconds(0.5f);
        currentScore = 0;
        driftingObject.SetActive(false);
    }

    void ManageUI()
    {
        totalScoreText.text = "Total: " + (totalScore).ToString("###,###,000");
        factorText.text = driftFactor.ToString("###,###,##0.0") + "X";
        currentScoreText.text = "Current score: " + currentScore.ToString("###,###,000");
        driftAngleText.text = driftAngle.ToString("###,##0") + "�";
    }
}

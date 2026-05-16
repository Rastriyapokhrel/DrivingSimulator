using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 20f;
    public float turnSpeed = 50f;
    public float maxSpeed = 25f;

    [Header("UI Reference")]
    public TextMeshProUGUI speedDisplay;

    [Header("Traffic Light")]
    public TrafficLight nearbyTrafficLight;
    public float trafficLightDetectionRange = 15f;

    private bool redLightWarningShown = false;
    private bool crashWarningShown = false;
    private bool speedZoneWarningShown = false;
    private bool stopSignWarningShown = false;

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        // Car movement
        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);

        // Speed display
        if (speedDisplay != null)
        {
            speedDisplay.text = "Speed: " +
                Mathf.Round(Mathf.Abs(move) * moveSpeed * 3f) + " km/h";
        }

        // Traffic light detection (ONLY if close AND in front)
        if (nearbyTrafficLight != null)
        {
            float distance = Vector3.Distance(
                transform.position,
                nearbyTrafficLight.transform.position);

            Vector3 directionToLight =
                (nearbyTrafficLight.transform.position - transform.position).normalized;

            // Checks if traffic light is in front of the car
            float dotProduct = Vector3.Dot(
                transform.forward,
                directionToLight);

            bool isInFront = dotProduct > 0.5f;

            if (distance <= trafficLightDetectionRange &&
                isInFront &&
                nearbyTrafficLight.isStopped)
            {
                if (Mathf.Abs(move) > 0.1f && !redLightWarningShown)
                {
                    redLightWarningShown = true;

                    if (UIManager.instance != null)
                    {
                        UIManager.instance.ShowWarning(
                            "RED LIGHT!\n" +
                            "You must stop at red and yellow lights!\n" +
                            "This is extremely dangerous!");
                    }

                    Invoke("ResetRedLightWarning", 4f);
                }
            }
            else
            {
                redLightWarningShown = false;
            }
        }
    }

    void ResetRedLightWarning()
    {
        redLightWarningShown = false;
    }

    void ResetCrashWarning()
    {
        crashWarningShown = false;
    }

    void ResetSpeedZoneWarning()
    {
        speedZoneWarningShown = false;
    }

    void ResetStopSignWarning()
    {
        stopSignWarningShown = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Obstacle") &&
            !crashWarningShown)
        {
            crashWarningShown = true;

            if (UIManager.instance != null)
            {
                UIManager.instance.ShowWarning(
                    "CRASH!\n" +
                    "You hit an obstacle!\n" +
                    "Always stay on the road!");
            }

            Invoke("ResetCrashWarning", 4f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        // Finish line
        if (col.CompareTag("Finish"))
        {
            if (UIManager.instance != null)
            {
                UIManager.instance.ShowSuccess(
                    "CONGRATULATIONS!\n" +
                    "You completed the driving course safely!\n" +
                    "You followed all traffic rules!");
            }
        }

        // Speed zone
        if (col.CompareTag("SpeedZone") &&
            !speedZoneWarningShown)
        {
            speedZoneWarningShown = true;

            if (UIManager.instance != null)
            {
                UIManager.instance.ShowWarning(
                    "SPEED LIMIT ZONE\n" +
                    "You are entering a reduced speed area.\n" +
                    "Slow down to drive safely!");
            }

            Invoke("ResetSpeedZoneWarning", 6f);
        }

        // Stop sign
        if (col.CompareTag("StopSign") &&
            !stopSignWarningShown)
        {
            stopSignWarningShown = true;

            if (UIManager.instance != null)
            {
                UIManager.instance.ShowWarning(
                    "STOP SIGN AHEAD\n" +
                    "You must come to a complete stop\n" +
                    "before proceeding!");
            }

            Invoke("ResetStopSignWarning", 6f);
        }
    }
}
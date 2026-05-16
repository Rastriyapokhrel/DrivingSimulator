using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float turnSpeed = 60f;
    public float maxSpeed = 8f;
    public TextMeshProUGUI speedDisplay;

    private Rigidbody rb;
    private bool speedWarningShown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);

        if (speedDisplay != null)
            speedDisplay.text = "Speed: " + Mathf.Round(move * moveSpeed) + " km/h";
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
            UIManager.instance.ShowWarning("CRASH!\nYou hit an obstacle!\nStay on the road!");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Finish"))
            UIManager.instance.ShowSuccess("CONGRATULATIONS!\nYou completed the course safely!");
        if (col.CompareTag("SpeedZone"))
            UIManager.instance.ShowWarning("SPEED LIMIT ZONE\nSlow down now!");
        if (col.CompareTag("StopSign"))
            UIManager.instance.ShowWarning("STOP SIGN\nCome to a complete stop!");
    }
}
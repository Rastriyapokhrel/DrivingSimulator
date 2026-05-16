using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [Header("Materials")]
    public Material redMaterial;
    public Material yellowMaterial;
    public Material greenMaterial;
    public Material offMaterial;

    [Header("Timing (seconds)")]
    public float greenDuration = 5f;
    public float yellowDuration = 2f;
    public float redDuration = 5f;

    public enum LightState { Green, Yellow, Red }
    public LightState currentState = LightState.Green;

    [HideInInspector]
    public bool isStopped = false;

    private float timer;
    private Renderer trafficRenderer;

    void Start()
    {
        trafficRenderer = GetComponent<Renderer>();
        timer = greenDuration;
        SetLight(LightState.Green);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            switch (currentState)
            {
                case LightState.Green:
                    SetLight(LightState.Yellow);
                    timer = yellowDuration;
                    break;
                case LightState.Yellow:
                    SetLight(LightState.Red);
                    timer = redDuration;
                    break;
                case LightState.Red:
                    SetLight(LightState.Green);
                    timer = greenDuration;
                    break;
            }
        }
    }

    void SetLight(LightState state)
    {
        currentState = state;
        isStopped = (state == LightState.Red || state == LightState.Yellow);

        if (trafficRenderer == null) return;

        switch (state)
        {
            case LightState.Red:
                trafficRenderer.material = redMaterial;
                break;
            case LightState.Yellow:
                trafficRenderer.material = yellowMaterial;
                break;
            case LightState.Green:
                trafficRenderer.material = greenMaterial;
                break;
        }
    }
}
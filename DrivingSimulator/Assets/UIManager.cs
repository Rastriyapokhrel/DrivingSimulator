using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Warning Popup")]
    public GameObject warningPopup;
    public TextMeshProUGUI warningText;

    [Header("Success Popup")]
    public GameObject successPopup;
    public TextMeshProUGUI successText;

    void Awake()
    {
        instance = this;
    }

    public void ShowWarning(string message)
    {
        if (warningPopup == null) return;
        warningText.text = message;
        warningPopup.SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void HideWarning()
    {
        if (warningPopup != null)
            warningPopup.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowSuccess(string message)
    {
        if (successPopup == null) return;
        successText.text = message;
        successPopup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
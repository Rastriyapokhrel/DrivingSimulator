using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
 
public class UIManager : MonoBehaviour
{
// Singleton — any script can call UIManager.instance.ShowWarning()
public static UIManager instance;
 
[Header("Warning Popup")]
public GameObject warningPopup; // drag WarningPopup panel here
public TextMeshProUGUI warningText; // drag WarningText here
 
[Header("Success Popup")]
public GameObject successPopup; // drag SuccessPopup panel here
public TextMeshProUGUI successText; // drag SuccessText here
 
void Awake()
{
// Make this the global instance
instance = this;
}
 
// Call this to show a warning/educational popup
public void ShowWarning(string message)
{
if (warningPopup == null) return;
warningText.text = message;
warningPopup.SetActive(true);
// Pause game slightly so player can read
Time.timeScale = 0.1f;
}
 
// Called by the Dismiss button
public void HideWarning()
{
if (warningPopup != null)
warningPopup.SetActive(false);
Time.timeScale = 1f; // resume normal speed
}
 
// Call this when player reaches finish line
public void ShowSuccess(string message)
{
if (successPopup == null) return;
successText.text = message;
successPopup.SetActive(true);
Time.timeScale = 0f; // freeze the game
}
 
// Called by Play Again button
public void RestartGame()
{
Time.timeScale = 1f;
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
}
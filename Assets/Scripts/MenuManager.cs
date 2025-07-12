using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public bool isMenuActive = true;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ToggleMenu()
    {
        if (isMenuActive)
        {
            isMenuActive = false;
            canvasGroup.alpha = 0f;
            Time.timeScale = 1f; // Resume game time
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            return;
        }

        isMenuActive = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f; // Pause game time
        UnityEngine.Cursor.lockState = CursorLockMode.None; // Unlock cursor
    }

    public void LoadLevel(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        isMenuActive = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f; // Resume game time
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Lock cursor
    }

    public void OnApplicationQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

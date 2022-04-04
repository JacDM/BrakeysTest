using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{
    public static bool isPaused=false;

    public GameObject PauseMenuUI;
    NewPlayerControls controls;

    void Awake()
    {
        controls = new NewPlayerControls();
        controls.Gameplay.Options.performed += ctx => { isPaused = !isPaused; };

    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    private void Update()
    {
        if (isPaused == false)
        {
            PauseMenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            //PauseMenuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

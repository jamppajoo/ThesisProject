using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{

    private bool showing = true;

    private Vector3 offset = new Vector3(0, 2000, 0);

    private Vector3 originalPosition;

    private RectTransform pauseMenuRectTransform;

    private Button resumeButton, restartButton, quitButton;


    private bool resumeButtonCanBePressed = false;

    private void Awake()
    {
        pauseMenuRectTransform = gameObject.GetComponent<RectTransform>();
        resumeButton = gameObject.transform.Find("ResumeButton").GetComponent<Button>();
        restartButton = gameObject.transform.Find("RestartButton").GetComponent<Button>();
        quitButton = gameObject.transform.Find("QuitButton").GetComponent<Button>();
    }

    void Start()
    {
        originalPosition = pauseMenuRectTransform.position;

        resumeButton.onClick.AddListener(ResumeButtonPressed);
        restartButton.onClick.AddListener(RestartButtonPressed);
        quitButton.onClick.AddListener(QuitButtonPressed);

        ToggleMenu();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();

        }
        if (Input.GetButtonDown("Start"))
        {
            ToggleMenu();
        }

    }

    public void ToggleMenu()
    {
        showing = !showing;
        if (showing)
        {
            Time.timeScale = 0;
            pauseMenuRectTransform.position = originalPosition;
            resumeButtonCanBePressed = true;
        }

        else
        {
            Time.timeScale = 1;
            pauseMenuRectTransform.position = originalPosition + offset;
            resumeButtonCanBePressed = false;
        }


    }

    private void ResumeButtonPressed()
    {
        if (resumeButtonCanBePressed)
            ToggleMenu();
    }
    private void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void QuitButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}

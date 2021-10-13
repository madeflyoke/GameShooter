using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(EndGame);
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void EndGame()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject wavesScreen;

    private void OnEnable()
    {
        wavesScreen.SetActive(false);
        playButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(EndGame);
    }

    private void StartGame()
    {
        wavesScreen.SetActive(true);
    }

    private void EndGame()
    {
        Application.Quit();
    }
}

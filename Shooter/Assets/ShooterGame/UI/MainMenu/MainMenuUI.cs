using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
 
    [SerializeField] private GameObject wavesScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject mainScreen;

    [SerializeField] private Button OKSettingsButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(()=>Settings(true));
        exitButton.onClick.AddListener(EndGame);

        OKSettingsButton.onClick.AddListener(() => Settings(false));

        wavesScreen.SetActive(false);
        settingsScreen.SetActive(false);
        mainScreen.SetActive(true);     
    }

    private void StartGame()
    {
        mainScreen.SetActive(false);
        wavesScreen.SetActive(true);
    }
    private void Settings(bool isActive)
    {
        if (isActive)
        {
            mainScreen.SetActive(false);
            settingsScreen.SetActive(true);
        }
        else
        {
            settingsScreen.SetActive(false);
            mainScreen.SetActive(true);
        }       
    }
    private void EndGame()
    {
        Application.Quit();
    }
}

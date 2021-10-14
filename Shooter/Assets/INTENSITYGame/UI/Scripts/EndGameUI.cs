using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button mainMenuButton;

    private void OnEnable()
    {      
        retryButton.onClick.AddListener(RetryGame);
        exitButton.onClick.AddListener(ExitGame);
        mainMenuButton.onClick.AddListener(()=>SceneManager.LoadSceneAsync(0));
        StartCoroutine(InteractEnable());
    }

    private IEnumerator InteractEnable()
    {
        yield return new WaitForSeconds(1.5f);
        retryButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    private void RetryGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}

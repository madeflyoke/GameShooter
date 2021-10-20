using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;


public class SpawnMenuUI : MonoBehaviour
{
    public static List<int> enemiesWavesRef = new List<int>();

    [SerializeField] private InputField wavesNumber;
    [SerializeField] private GameObject[] waves;
    [SerializeField] private GameObject generalWave;
    [SerializeField] private Button startButton;
    private int[] enemiesWaves;
    private InputField generalInput;
    private InputField[] customInputs;
    private int numberOfWaves;

    private void Awake()
    {
        wavesNumber.onEndEdit.AddListener(SetNumberOfWaves);
        generalInput = generalWave.GetComponentInChildren<InputField>();
        generalInput.onEndEdit.AddListener(GeneralWaveEnemies);

        startButton.onClick.AddListener(()=> { enemiesWavesRef = enemiesWaves.ToList();SceneManager.LoadSceneAsync(1); });

        customInputs = new InputField[waves.Length];
        generalWave.SetActive(false);
        for (int i = 0; i < waves.Length; i++)//add listeners input fields of custom waves
        {
            int tmp = i; //add listener fix
            customInputs[i] = waves[i].GetComponentInChildren<InputField>();
            customInputs[i].onEndEdit.AddListener((s) => CustomWavesEnemies(s, tmp));
            waves[i].SetActive(false);
        }
        generalWave.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    private void SetNumberOfWaves(string number)
    {
        if (int.TryParse(number, out numberOfWaves))
        {
            generalWave.SetActive(false);  //refresh
            generalInput.text = "";
            startButton.gameObject.SetActive(false);
            foreach (GameObject item in waves)
            {
                item.SetActive(false);
            }
            foreach (InputField item in customInputs)
            {
                item.text = "";
            }                           

            if (numberOfWaves <= 0)//wrong input
            {
                wavesNumber.text = "";
            }
            else if (numberOfWaves > 10) //general input starts
            {
                generalWave.SetActive(true);
                enemiesWaves = new int[numberOfWaves];
            }
            else //custom inputs start
            {
                enemiesWaves = new int[numberOfWaves];
                for (int i = 0; i < numberOfWaves; i++)
                {
                    waves[i].SetActive(true);
                }
            }
        }      
    }

    private void CustomWavesEnemies(string enemies, int index)
    {
        int num;
        if (int.TryParse(enemies,out num))
        {
            if (num <= 0)//wrong input
            {
                customInputs[index].text = "";
                enemiesWaves[index] = 0;
                startButton.gameObject.SetActive(false);
                return;
            }
            else if (num > 32) //max enemies?
            {
                customInputs[index].text = "32";
                num = 32;
            }
            enemiesWaves[index] = num;
            for (int i = 0; i < numberOfWaves; i++)//check null values
            {
                if (enemiesWaves[i]==0)
                {
                    startButton.gameObject.SetActive(false);
                    return;
                }
            }
            startButton.gameObject.SetActive(true);
        }
        
    }

    private void GeneralWaveEnemies(string enemies)
    {
        int num;
        if (int.TryParse(enemies, out num))
        {
            if (num <= 0)//wrong input
            {
                generalInput.text = "";
                for (int i = 0; i < numberOfWaves; i++)
                {
                    enemiesWaves[i] = 0;
                }
                startButton.gameObject.SetActive(false);
                return;
            }
            else if (num > 32) //max enemies?
            {
                generalInput.text = "32";
                num = 32;
            }

            for (int i = 0; i < numberOfWaves; i++)
            {
                enemiesWaves[i] = num;
            }
            startButton.gameObject.SetActive(true);
        }       
    }


}

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class VLCCUi : MonoBehaviour
{

    public static VLCCUi instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private GameObject VLCCPanel;

    [SerializeField] private TMP_Text countDown;
    [SerializeField] private VLCCManager vlccManager;

    [Header("Katakana")]
    [SerializeField] private GameObject katakanaPrefab;
    [SerializeField] private Transform katakanaSpawner;


    [Header("Romanji")]
    [SerializeField] private GameObject romajiTextPrefab;
    [SerializeField] private List<GameObject> romajiList;
    [SerializeField] private RomajiSpawner romajiSpawner;

    [Header("Answer")]
    [SerializeField] private Transform romajiAnswerSpawner;
    [SerializeField] private GameObject romajiAnswerPrefab;

    private void Start()
    {
        vlccManager = VLCCManager.instance;
    }

    private void Update()
    {
        if(vlccManager.GetDuration() > 0)
            UpdateCountDown();
    }

    public void SetUpVLCCPanel()
    {
        VLCCPanel.SetActive(!VLCCPanel.activeSelf);
    }

    public void SpawnKatakana(char x)
    {
        Debug.Log("katakana");
        GameObject obj = Instantiate(katakanaPrefab, katakanaSpawner);
        obj.GetComponent<KatakanaPrefabScript>().SetUp(x);
    }



    public void SpawnRomaji(string x)
    {
        Debug.Log("romaji");
        int index = romajiSpawner.RandomizeRomajiSpawn();
        GameObject obj = Instantiate(romajiTextPrefab, romajiList[index].transform);

        obj.GetComponent<RomajiPrefabScript>().SetUp(x);
    }
    public void SpawnRomajiAnswer(string x)
    {
        Debug.Log("Romaji Answer");
        GameObject obj = Instantiate(romajiAnswerPrefab, romajiAnswerSpawner);
        obj.GetComponent<RomajiAnswerScript>().SetUp(x);
    }



    public void DeleteAll()
    {
        // Katakana
        for (int i = katakanaSpawner.childCount - 1; i >= 0; i--)
        {
            Destroy(katakanaSpawner.GetChild(i).gameObject);
        }

        // Romaji
        if (romajiList != null)
        {
            foreach (GameObject obj in romajiList)
            {
                if (obj == null) continue;

                for (int i = obj.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(obj.transform.GetChild(i).gameObject);
                }
            }
        }

        // Answer
        for (int i = romajiAnswerSpawner.childCount - 1; i >= 0; i--)
        {
            Destroy(romajiAnswerSpawner.GetChild(i).gameObject);
        }
    }



    public void UpdateCountDown()
    {
        countDown.text = vlccManager.GetDuration().ToString("F2");
    }
}

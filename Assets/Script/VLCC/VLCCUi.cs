using UnityEngine;
using TMPro;

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

    [SerializeField] private TMP_Text countDown;
    [SerializeField] private VLCCManager vlccManager;

    [Header("Katakana")]
    [SerializeField] private GameObject katakanaPrefab;
    [SerializeField] private Transform katakanaSpawner;

    [Header("Romanji")]
    [SerializeField] private GameObject romajiPrefab;
    [SerializeField] private Transform romajiSpawner;

    private void Start()
    {
        vlccManager = VLCCManager.instance;
    }

    private void Update()
    {
        if(vlccManager.GetDuration() > 0)
            UpdateCountDown();
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
        GameObject obj = Instantiate(romajiPrefab, romajiSpawner);
        obj.GetComponent<RomajiPrefabScript>().SetUp(x);
    }

    public void UpdateCountDown()
    {
        countDown.text = vlccManager.GetDuration().ToString("F2");
    }
}

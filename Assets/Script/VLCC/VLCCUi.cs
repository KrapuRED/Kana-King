using UnityEngine;
using TMPro;

public class VLCCUi : MonoBehaviour
{
    [SerializeField] private TMP_Text countDown;
    [SerializeField] private VLCCManager vlccManager;

    [Header("Katakana")]
    [SerializeField] private GameObject katakanaPrefab;

    [Header("Rumanji")]
    [SerializeField] private GameObject rumanjiPrefab;


    [SerializeField] private DataVLCC dataVLCC;

    private void Start()
    {
        vlccManager = VLCCManager.instance;
    }

    private void Update()
    {
        if(vlccManager.GetDuration() > 0)
            UpdateCountDown();
    }

    public void SetUpKatakana()
    {
        foreach (char x in dataVLCC.katakana)
        {
            GameObject obj = Instantiate(katakanaPrefab, transform);

            TMP_Text text = obj.GetComponent<TMP_Text>();
            if (text != null)
            {
                text.text = x.ToString();
            }
        }
    }

    public void UpdateCountDown()
    {
        countDown.text = vlccManager.GetDuration().ToString("F2");
    }
}

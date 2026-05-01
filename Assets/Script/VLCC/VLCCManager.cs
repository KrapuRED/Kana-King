using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class VLCCManager : MonoBehaviour
{
    public static VLCCManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    [SerializeField] private float duration = 5f;


    [SerializeField] private DataVLCC dataVLCC;
    [SerializeField] private VLCCUi vlccUI;

    public List<string> romajiOrder;

    private void Update()
    {
        if (duration > 0)
            duration -= Time.deltaTime;
    }

    public float GetDuration()
    {
        return duration;
    }
    public void SetDuration(float x, string name)
    {
        SetUpVLCC(name);
        duration = x;
    }

    public void SetUpVLCC(string name)
    {
        dataVLCC = DatabaseVLCC.instance.FindData(name);
        SetUpKatakana();
        SetUpRomaji();
    }

    public void SetUpKatakana()
    {
        vlccUI = VLCCUi.instance;
        foreach (char x in dataVLCC.katakana)
        {
            vlccUI.SpawnKatakana(x);
        }
    }
    public void SetUpRomaji()
    {
        vlccUI = VLCCUi.instance;
        string[] parts = dataVLCC.romaji.Split(' ');
        foreach (string part in parts)
        {
            romajiOrder.Add(part);
            vlccUI.SpawnRomaji(part);
        }
    }
}

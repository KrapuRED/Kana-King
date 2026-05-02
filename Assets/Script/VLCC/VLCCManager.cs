using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

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

    public bool onVLCC = false;

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
        if(onVLCC == false)
        {
            onVLCC = true;
            VLCCUi.instance.SetUpVLCCPanel();
            dataVLCC = DatabaseVLCC.instance.FindData(name);
            SetUpKatakana();
            SetUpRomaji();
        }
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

    public bool CheckRomajiOrder(string x)
    {
        if (x == romajiOrder[0])
        {
            Debug.Log("correct");
            VLCCUi.instance.SpawnRomajiAnswer(x);
            romajiOrder.RemoveAt(0);
            return true;
        }
        else
        {
            Debug.Log("salah");
            return false;
        }
    }

    public void CheckVLCC()
    {
        if (romajiOrder == null || romajiOrder.Count == 0)
        {
            StartCoroutine(ResetVLCC());
        }

    }

    public IEnumerator ResetVLCC()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("Hello");
        dataVLCC = null;
        VLCCUi.instance.SetUpVLCCPanel();
        romajiOrder.Clear();
        onVLCC = false;
        VLCCUi.instance.DeleteAll();
    }
}

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

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


    [SerializeField] private float duration = 7f;

    [SerializeField] private DataVLCC dataVLCC;
    [SerializeField] private VLCCUi vlccUI;

    public List<string> romajiOrder;

    public bool onVLCC = false;


    public event Action VLCCReward;

    private void Update()
    {
        if (onVLCC)
        {
            if (duration > 0)
            {
                duration -= Time.unscaledDeltaTime;
            }
            else if (onVLCC && duration <= 0)
            {
                onVLCC = false;
                VLCCFailed();
            }
        }

    }

    public float GetDuration()
    {
        return duration;
    }

    public void SetDuration(float x)
    {
        if (!onVLCC)
        {
            SetUpVLCC();
            duration = x;
        }
    }

    public void SetUpVLCC()
    {
        onVLCC = true;
        romajiOrder.Clear(); // Pastikan order bersih sebelum diisi baru

        VLCCUi.instance.SetUpVLCCPanel();
        dataVLCC = DatabaseVLCC.instance.FindRandomData();

        if (dataVLCC != null)
        {
            SetUpKatakana();
            SetUpRomaji();
        }

        PauseSystem.instance.AddPauseRequest();
    }

    //public void SetDuration(float x, string name)
    //{
    //    if (!onVLCC)
    //    {
    //        SetUpVLCC(name);
    //        duration = x;
    //    }
    //}

    //public void SetUpVLCC(string name)
    //{
    //    onVLCC = true;
    //    VLCCUi.instance.SetUpVLCCPanel();
    //    dataVLCC = DatabaseVLCC.instance.FindData(name);
    //    SetUpKatakana();
    //    SetUpRomaji();
    //    PauseSystem.instance.AddPauseRequest();
    //}

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
        if (!onVLCC)
        {
            return false;
        }

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
            VLCCComplete();
        }

    }

    public IEnumerator ResetVLCC()
    {
        onVLCC = false;
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("Hello");
        dataVLCC = null;
        VLCCUi.instance.SetUpVLCCPanel();
        romajiOrder.Clear();
        VLCCUi.instance.DeleteAll();

        VLCCReward = null;
        PauseSystem.instance.RemovePauseRequest();
    }


    public void VLCCFailed()
    {
        Debug.Log("Gagal");
        for(int i = 0; i < romajiOrder.Count; i++)
            VLCCUi.instance.SpawnRomajiAnswer(romajiOrder[i]);
        StartCoroutine(ResetVLCC());
    }

    public void VLCCComplete()
    {
        Debug.Log("Berhasil");
        VLCCReward?.Invoke();
        //ArtefactManager.instance.OpenArtefactManager(ArtefactDatabase.instance.ReturnRandomArtefact());
        StartCoroutine(ResetVLCC());

    }
}

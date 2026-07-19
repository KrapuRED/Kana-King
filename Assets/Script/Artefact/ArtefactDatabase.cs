using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArtefactData
{
    public ArtefactSO artefactSO;
    public bool isActivated;
}

public class ArtefactDatabase : MonoBehaviour
{
    public static ArtefactDatabase instance;

    [SerializeField] private List<ArtefactData> allArtefact = new List<ArtefactData>();
    public List<ArtefactData> AllArtefact => allArtefact;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Mengembalikan artefak acak dari daftar artefak yang AKTIF.
    /// </summary>
    /// <summary>
    /// Mengembalikan artefak acak dari daftar artefak yang BELUM AKTIF (belum dimiliki/belum terbuka).
    /// </summary>
    public ArtefactSO ReturnRandomArtefact()
    {
        List<ArtefactSO> availableArtefacts = new List<ArtefactSO>();

        foreach (var artefact in allArtefact)
        {
            // Kuncinya di sini: mencari yang isActivated == false
            if (!artefact.isActivated && artefact.artefactSO != null)
            {
                availableArtefacts.Add(artefact.artefactSO);
            }
        }

        // Jaga-jaga kalau semua artefak di database SUDAH diaktifkan semua oleh pemain
        if (availableArtefacts.Count == 0)
        {
            Debug.LogWarning("Semua artefak di database sudah aktif! Tidak ada pilihan tersisa.");
            return null;
        }

        // Acak dari list artefak yang masih tersedia
        int randomIndex = Random.Range(0, availableArtefacts.Count);
        return availableArtefacts[randomIndex];
    }


    /// <summary>
    /// Menghitung berapa banyak artefak yang saat ini sedang aktif digunakan pemain.
    /// </summary>
    public int GetActiveArtefactCount()
    {
        int count = 0;
        foreach (var art in allArtefact)
        {
            if (art.isActivated) count++;
        }
        return count;
    }

    public void ActivatedArtefact(ArtefactSO artefact)
    {
        foreach (ArtefactData art in allArtefact)
        {
            if (art.artefactSO == artefact)
            {
                art.isActivated = true;
                ArtefactManager.instance.CheckArtefactBuff(art);
                break;
            }
        }
    }

    public void DeactivatedArtefact(ArtefactSO artefact)
    {
        foreach (ArtefactData art in allArtefact)
        {
            if (art.artefactSO == artefact)
            {
                art.isActivated = false;
                ArtefactManager.instance.CheckArtefactBuff(art);
                break;
            }
        }
    }
}
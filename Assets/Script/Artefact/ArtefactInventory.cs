using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactInventory : MonoBehaviour
{

    public static ArtefactInventory instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("New Artefact")]
    [SerializeField] private Transform newArtefactSpawn;
    [SerializeField] private GameObject newArtefactUIPrefab;

    [Header("Current Artefact")]
    [SerializeField] private Transform currArtefactSpawn;
    [SerializeField] private GameObject currentArtefactUIPrefab;

    [Header("Description Artefact")]
    [SerializeField] private TMP_Text artefactName;
    [SerializeField] private TMP_Text artefactDescription;

    [Header("Screen")]
    [SerializeField] private List<Image> artefactImages = new List<Image>();

    private void Start()
    {
        SetUpArtefactInventory();
    }
    public void SetUpNewArtefact(ArtefactSO artefact)
    {
        foreach (Transform child in newArtefactSpawn)
        {
            Destroy(child.gameObject);
        }

        if (artefact != null)
        {
            GameObject y = Instantiate(newArtefactUIPrefab, newArtefactSpawn);
            y.GetComponent<ArtefactNewUI>().ArtefactNewSetUp(artefact);
        }
    }
    public void SetUpArtefactInventory()
    {
        foreach (Transform child in currArtefactSpawn)
        {
            Destroy(child.gameObject);
        }

        // 2. Sembunyikan semua slot gambar di HUD layar utama terlebih dahulu
        foreach (Image image in artefactImages)
        {
            if (image != null)
            {
                image.sprite = null;
                image.enabled = false;
            }
        }

        int index = 0;

        // 3. Tampilkan ulang visual berdasarkan list artefak aktif saat ini
        foreach (ArtefactSO artefactSO in ArtefactManager.instance.CurrArtefact)
        {
            if (artefactSO != null)
            {
                // A. Buat item di dalam Grid Inventory
                GameObject y = Instantiate(currentArtefactUIPrefab, currArtefactSpawn);
                y.GetComponent<ArtefactCurrentUI>().ArtefactCurrentSetUp(artefactSO);

                // B. Perbarui gambar slot HUD layar utama sesuai index list yang rapat kiri
                if (index < artefactImages.Count)
                {
                    artefactImages[index].sprite = artefactSO.artefactSprite;
                    artefactImages[index].enabled = true;
                    index++;
                }
            }
        }
    }

    public void OpenArtefactDescription(ArtefactSO artefactSO)
    {
        if (artefactSO == null) return;
        artefactName.text = artefactSO.artefactName;
        artefactDescription.text = artefactSO.artefactDescription;
    }

    public void TakeNewArtefact()
    {
        ArtefactManager.instance.AddArtefact();
    }

    public void DidntTakeNewArtefact()
    {
        ArtefactManager.instance.StashArtefact();
    }

}

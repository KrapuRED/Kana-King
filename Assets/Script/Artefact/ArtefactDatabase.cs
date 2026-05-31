using System.Collections.Generic;
using UnityEngine;

public class ArtefactDatabase : MonoBehaviour
{
    public static ArtefactDatabase instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private List<ArtefactScript> allArtefact;
    private List<ArtefactScript> currArtefact;

    public ArtefactScript ReturnRandomArtefact()
    {
        currArtefact = ArtefactManager.instance.ReturnCurrentArtefact();

        if (currArtefact == null || currArtefact.Count == 0)
        {
            return allArtefact[RandomIndex()];
        }

        //List<ArtefactScript> x = ArtefactDatabase.instance.ReturnCurrentArtefact();

        List<ArtefactScript> artefactAvailable = new List<ArtefactScript>();

        foreach (var art in allArtefact)
        {
            bool alreadyHave = false;
            foreach (var owned in currArtefact)
            {
                if (owned == art)
                {
                    alreadyHave = true;
                    break; // Keluar dari loop kecil jika terbukti sudah punya
                }
            }

            // Jika belum punya, berarti ini kandidat suci yang boleh di-gacha
            if (!alreadyHave)
            {
                artefactAvailable.Add(art);
            }
        }

        // 4. Jaga-jaga jika semua artefak di database ternyata SUDAH dimiliki player
        if (artefactAvailable.Count == 0)
        {
            Debug.LogWarning("Semua artefak di database sudah dimiliki oleh player!");
            return null; // Mengembalikan null, atau bisa kamu ganti ke 'return allArtefact[RandomIndex()];' jika boleh duplikat saat penuh
        }

        // 5. Acak dan kembalikan salah satu artefak yang tersedia (pasti tidak duplikat)
        int randomIndexTersedia = Random.Range(0, artefactAvailable.Count);
        return artefactAvailable[randomIndexTersedia];
    }


    private int RandomIndex()
    {
        return Random.Range(0, allArtefact.Count);
    }


    public List<ArtefactScript> ReturnAllArtefact()
    {
        return allArtefact;
    }
        
}

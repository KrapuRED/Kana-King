using System.Collections.Generic;
using UnityEngine;


public enum CreateReward
{
    Coin,
    Exp,
    Artefact
}


[System.Serializable]
public class CrateFill
{
    public CreateReward itemFil;
    public float itemDropChance;
}


public class CrateScript : MonoBehaviour, IInteractable
{

    [SerializeField] private List<CrateFill> itemDrops;
    [SerializeField] private Transform playerLocation;
    bool canInteract;

    [Header("VLCC Setting")]
    [SerializeField] private float duration;

    [Header("Reward Setting")]
    private CreateReward itemDrop;
    [SerializeField] private int coinDrop;
    [SerializeField] private float expDrop;



    private void Awake()
    {
        // Cache the player reference ONLY ONCE when the object is first created
        if (playerLocation == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerLocation = player.transform;
            }
            else
            {
                Debug.LogWarning($"Player tag not found in scene by {gameObject.name}!");
            }
        }
    }

    private void OnEnable()
    {
        // Safe to reset state here for pooling reuse
        canInteract = true;
    }

    private void OnDisable()
    {
        canInteract = false;
    }

    public void Interact()
    {
        if (!canInteract) return;
        canInteract = false;

        // 1. Tentukan drop acak SEBELUM data dihancurkan/dimatikan
        CrateFill selectedDrop = GetRandomCrateFill();
        CreateReward rewardType = selectedDrop != null ? selectedDrop.itemFil : CreateReward.Coin;

        // 2. Cache data hadiah ke variabel lokal agar tidak hilang saat objek hancur
        int finalCoin = coinDrop;
        float finalExp = expDrop;

        // 3. Daftarkan fungsi anonim dengan data ter-cache agar eksekusi pasca-hancur tetap aman
        VLCCManager.instance.VLCCReward += () =>
        {
            ExecuteReward(rewardType, finalCoin, finalExp);
        };

        VLCCManager.instance.SetDuration(duration);

        // 4. Nonaktifkan visual & interaksi peti, kemudian hancurkan objek
        GetComponent<Collider2D>().enabled = false;
        if (TryGetComponent<SpriteRenderer>(out var sr)) sr.enabled = false;

        Destroy(gameObject);
    }

    public CrateFill GetRandomCrateFill()
    {
        if (itemDrops == null || itemDrops.Count == 0) return null;

        float total = 0f;
        foreach (var item in itemDrops)
        {
            total += item.itemDropChance;
        }

        float randomRoll = Random.Range(0f, total);
        foreach (var item in itemDrops)
        {
            randomRoll -= item.itemDropChance;
            if (randomRoll <= 0) return item;
        }

        return itemDrops[itemDrops.Count - 1];
    }

    // Fungsi eksekusi utama yang menerima parameter aman mandiri
    private static void ExecuteReward(CreateReward type, int coins, float exp)
    {
        switch (type)
        {
            case CreateReward.Coin:
                PlayerStat.instance.AddCoin(coins);
                break;
            case CreateReward.Exp:
                Player.instance.AddExp(exp);
                break;
            case CreateReward.Artefact:
                ArtefactManager.instance.OpenArtefactManager(ArtefactDatabase.instance.ReturnRandomArtefact());
                break;
        }
    }
}

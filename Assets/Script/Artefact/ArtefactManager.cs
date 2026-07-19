using UnityEngine;

public class ArtefactManager : MonoBehaviour
{
    public static ArtefactManager instance;

    [SerializeField] private ArtefactSO newArtefact;
    public ArtefactSO NewArtefact => newArtefact;

    [SerializeField] private GameObject artefactInventoryPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        OpenArtefactManager(ArtefactDatabase.instance.ReturnRandomArtefact());
    }

    public void OpenArtefactManager(ArtefactSO artefact)
    {
        artefactInventoryPanel.SetActive(true);
        newArtefact = artefact;
        ArtefactInventory.instance.SetUpArtefactInventory();
        ArtefactInventory.instance.SetUpNewArtefact(newArtefact);
        PauseSystem.instance.AddPauseRequest();
    }

    public void AddArtefact()
    {
        if (CheckArtefactInventorySpace())
        {
            ArtefactDatabase.instance.ActivatedArtefact(newArtefact);
        }
        else
        {
            Debug.LogWarning("Inventory Penuh! Tidak bisa menambah artefak baru.");
        }

        artefactInventoryPanel.SetActive(false);
        PauseSystem.instance.RemovePauseRequest();
    }

    public void StashArtefact()
    {
        newArtefact = null;
        artefactInventoryPanel.SetActive(false);
        PauseSystem.instance.RemovePauseRequest();
    }

    public void DeleteArtefact(ArtefactSO artefact)
    {
        ArtefactDatabase.instance.DeactivatedArtefact(artefact);
        ArtefactInventory.instance.SetUpArtefactInventory();
    }

    public void CheckArtefactBuff(ArtefactData artefactData)
    {
        if (artefactData?.artefactSO == null) return;

        switch (artefactData.artefactSO.artefactBuff)
        {
            case TypeBuff.RegenHPEachSecond:
                if (artefactData.isActivated)
                    Player.instance.HealArtefactActivated(artefactData.artefactSO.buffValue);
                else
                    Player.instance.HealArtefactDisable();
                break;

            case TypeBuff.RegenHPWhenDamaging:
                if (artefactData.isActivated)
                    PlayerAttackMelee.instance.AddArtefactBuff(artefactData.artefactSO.buffValue);
                else
                    PlayerAttackMelee.instance.RemoveArtefactBuff();
                break;

            case TypeBuff.IncreaseDamagePercentage:
                if (artefactData.isActivated)
                    PlayerStat.instance.AddPercentBuff(StatType.Attack, artefactData.artefactSO.buffValue);
                else
                    PlayerStat.instance.RemovePercentBuff(StatType.Attack, artefactData.artefactSO.buffValue);
                break;
        }
    }

    /// <summary>
    /// Mengecek apakah slot inventory aktif saat ini masih kurang dari batas maksimal (misal: max 3).
    /// </summary>
    public bool CheckArtefactInventorySpace()
    {
        // Sekarang memanggil fungsi hitung barunya, bukan menghitung seluruh isi database lagi.
        return ArtefactDatabase.instance.GetActiveArtefactCount() < 3;
    }
}
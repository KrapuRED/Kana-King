using UnityEngine;

public class HealingFromAttackArtefact : ArtefactScript
{
    [SerializeField] private float regenPercentageAttackBuff = 30f;

    protected override void ArtefactActive()
    {
        Debug.Log("Arte heal attack aktif");

        PlayerAttackMelee.instance.AddArtefactBuff(regenPercentageAttackBuff);

    }
    protected override void ArtefactDisable()
    {
        Debug.Log("Arte heal attack mati");

        PlayerAttackMelee.instance.RemoveArtefactBuff();
    }
}

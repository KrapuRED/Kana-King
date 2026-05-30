using UnityEngine;

public class AddPercentageAttackArtefact : ArtefactScript
{
    [SerializeField] private float attackBuff = 30f;

    protected override void ArtefactActive()
    {
        Debug.Log("Arte buff damage aktif");

        PlayerStat.instance.AddPercentBuff(StatType.Attack, attackBuff);

    }
    protected override void ArtefactDisable()
    {
        Debug.Log("Arte buff damage mati");

        PlayerStat.instance.RemovePercentBuff(StatType.Attack, attackBuff);
    }
}

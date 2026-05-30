using UnityEngine;

public class ExpScript : ItemScript
{
    public float expValue = 0;

    [SerializeField] private Player playerScript;

    public void SetEXPValue(float value)
    {
        expValue = value;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected override void OnHitPlayer()
    {
        playerScript.AddExp(expValue);
        base.OnHitPlayer();
    }
}

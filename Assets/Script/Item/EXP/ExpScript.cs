using UnityEngine;

public class ExpScript : ItemScript
{
    public float expValue = 0;

    public void SetEXPValue(float value)
    {
        expValue = value;
    }

    protected override void OnHitPlayer()
    {
        Player.instance.AddExp(expValue);
        Destroy(gameObject);
    }
}

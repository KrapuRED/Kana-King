using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{

    public static PlayerUI instance;

    private void Awake()
    {
        if(instance == null )
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthText;

    [Header("Experience")]
    [SerializeField] private Slider expBar;

    [Header("Stat")]
    [SerializeField] private TMP_Text healthStatText;
    [SerializeField] private TMP_Text attackStatText;
    [SerializeField] private TMP_Text defendStatText;
    [SerializeField] private TMP_Text critStatText;

    private void Start()
    {
        HealthUISetUp();
        ExperienceUISetUp();
    }
    public void HealthUISetUp()
    {
        healthBar.minValue = 0f;

        // 2. Batas paling kanan/maksimum slider diisi darah maksimal player
        healthBar.maxValue = Player.instance.maxHealth;

        // 3. Nilai isi slider (panjang bar) mengikuti darah player saat ini
        healthBar.value = Player.instance.CurrentHealth;

        healthText.text = $"{healthBar.value.ToString()}/{healthBar.maxValue.ToString()}";
    }

    public void ExperienceUISetUp()
    {
        expBar.minValue = 0f;

        expBar.maxValue = Player.instance.MaxPlayerExp;

        expBar.value = Player.instance.CurrentPlayerExp;
    }

    public void StatUISetUp()
    {
        healthStatText.text = $"Health = {PlayerStat.instance.GetStat(StatType.Health)}";
        attackStatText.text = $"Attack = {PlayerStat.instance.GetStat(StatType.Attack)}";
        defendStatText.text = $"Defense = {PlayerStat.instance.GetStat(StatType.Defense)}";
        critStatText.text = $"Critical = {PlayerStat.instance.GetStat(StatType.Critical)}";
    }
}

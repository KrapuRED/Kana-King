using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour, IDamageable
{

    public static Player instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }



    [Header("Stats")]
    public float maxHealth => PlayerStat.instance.GetStat(StatType.Health);
    [SerializeField] private float currentHealth;
    [SerializeField] private float damage = 10f;
    public float CurrentHealth => currentHealth;
    public float Damage => damage;


    [Header("EXP & LEVEL")]
    [SerializeField] private float currPlayerExp;
    public float CurrentPlayerExp => currPlayerExp;
    [SerializeField] private float maxPlayerExp;
    public float MaxPlayerExp => maxPlayerExp;
    [SerializeField] private int level;


    [Header("Lose")]
    [SerializeField] private LoseScript loseScript;


    [Header("Taking Damage")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float delayAnimationDamage = 0.3f;
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] private Light2D light;

    [Header("Healing Artefact")]
    [SerializeField] private float healingAmount = 5f;
    [SerializeField] private float healInterval = 1f;
    private Coroutine healCoroutine;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage - StatCalculationManager.instance.DefendDamage();
        if (finalDamage < 0)
            finalDamage = 0;

        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        cinemachineImpulseSource.GenerateImpulse();
        StartCoroutine(TakingDamageAnimation());

        PlayerUI.instance.HealthUISetUp();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Healing(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        PlayerUI.instance.HealthUISetUp();
    }
    public void HealingMaxHp(float percentAmount)
    {
        float totalHeal = maxHealth * (percentAmount / 100f);
        currentHealth += totalHeal;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        PlayerUI.instance.HealthUISetUp();
    }

    private void Die()
    {
        Debug.Log("Player Die");
        loseScript.LoseUISetUp();
        //Destroy(gameObject);
    }

    public void AddExp(float amount)
    {
        currPlayerExp += amount;
        if(currPlayerExp >= maxPlayerExp)
        {
            currPlayerExp -= maxPlayerExp;
            OnLevelUp();
        }
        PlayerUI.instance.ExperienceUISetUp();
    }

    public void OnLevelUp()
    {
        level++;
        //maxPlayerExp += (30 + (level * 15));
        maxPlayerExp = (30 + (level * 15));
        PlayerUI.instance.HealthUISetUp();
        ShopManager.instance.OpenShop();
    }

    IEnumerator TakingDamageAnimation()
    {
        Color damageColor = Color.red;
        Color normalColor = Color.white;
        float elapsedTime = 0f;

        spriteRenderer.color = damageColor;
        light.color = damageColor;

        while (elapsedTime < delayAnimationDamage)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / delayAnimationDamage; // Mengubah nilai menjadi rentang 0.0 sampai 1.0

            Color currentColor = Color.Lerp(damageColor, normalColor, t);

            spriteRenderer.color = currentColor;
            light.color = currentColor;

            yield return null; // Tunggu hingga frame berikutnya
        }

        spriteRenderer.color = normalColor;
        light.color = normalColor;
    }



    public void HealArtefactActivated(float heal)
    {
        healingAmount = heal;

        if (healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
        }

        // Mulai jalankan healing per detik
        healCoroutine = StartCoroutine(HealOverTime());
    }


    public void HealArtefactDisable()
    {
        Debug.Log("Arte regen mati");
        if (healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
            healCoroutine = null; // Kosongkan kembali referensinya
        }
    }


    private IEnumerator HealOverTime()
    {
        while (true)
        {
            // Panggil fungsi heal dari player kamu
            if (Player.instance != null)
            {
                Player.instance.Healing(healingAmount);
                Debug.Log($"Player di-heal sebesar {healingAmount}");
            }

            // Tunggu selama interval yang ditentukan (misal 1 detik) sebelum lanjut looping
            yield return new WaitForSeconds(healInterval);
        }
    }

}

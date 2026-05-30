using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

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
    [SerializeField] private float maxPlayerExp;
    [SerializeField] private int level;


    [Header("Lose")]
    [SerializeField] private LoseScript loseScript;


    [Header("Taking Damage")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float delayAnimationDamage = 0.3f;
    [SerializeField] private CinemachineImpulseSource cinemachineImpulseSource;

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
        currentHealth -= damage;
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
    }

    public void OnLevelUp()
    {
        level++;
        ShopManager.instance.OpenShop();
    }

    IEnumerator TakingDamageAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(delayAnimationDamage);
        spriteRenderer.color = Color.white;
    }



    public void HealArtefactActivated()
    {
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

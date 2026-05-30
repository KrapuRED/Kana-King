using System.Collections;
using UnityEngine;

public class RegenHPArtefact : ArtefactScript
{
    [SerializeField] private float healingAmount = 5f;
    [SerializeField] private float healInterval = 1f;

    private Coroutine healCoroutine;
    protected override void ArtefactActive()
    {
        Debug.Log("Arte regen aktif");

        if (healCoroutine != null)
        {
            StopCoroutine(healCoroutine);
        }

        // Mulai jalankan healing per detik
        healCoroutine = StartCoroutine(HealOverTime());

    }
    protected override void ArtefactDisable()
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

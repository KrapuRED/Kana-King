using UnityEngine;
using DG.Tweening;

public class BossNotification : MonoBehaviour
{
    [SerializeField] private CanvasGroup bossCanvas;

    private void Awake()
    {
        // Memastikan diawal CanvasGroup benar-benar transparan / tidak terlihat
        if (bossCanvas != null)
        {
            bossCanvas.alpha = 0f;
        }
    }

    // Ubah method dari 'public PlayBossNotification()' menjadi 'public void PlayBossNotification()'
    public void PlayBossNotification()
    {
        if (bossCanvas == null) return;

        // Hentikan tween sebelumnya jika notifikasi dipanggil berulang kali
        bossCanvas.DOKill();

        // Pastikan mulai dari invisible
        bossCanvas.alpha = 0f;

        // Jalankan efek blinking selama 3 detik
        // DOFade(1f, 0.3f) -> Fade-in ke alpha 1 dalam 0.3 detik
        // SetLoops(10, LoopType.Yoyo) -> Berkedip bolak-balik (5 kali in, 5 kali out = 10 x 0.3s = total 3 detik)
        bossCanvas.DOFade(1f, 0.3f)
            .SetLoops(10, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // Setelah 3 detik selesai, pastikan alpha kembali ke 0 (invisible)
                bossCanvas.alpha = 0f;
            });
    }
}
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static PauseSystem instance;

    [SerializeField] private GameObject pauseMenuPanel;

    // Mengganti bool menjadi int untuk menghitung berapa banyak UI yang meminta pause
    [SerializeField] private int pauseRefCount = 0;

    // Game dianggap pause jika ada minimal 1 UI yang meminta pause
    public bool IsPaused => pauseRefCount > 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        // Contoh khusus untuk tombol Escape (Pause Menu utama)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel.activeSelf)
            {
                // Tutup pause menu utama
                pauseMenuPanel.SetActive(false);
                RemovePauseRequest();
            }
            else
            {
                // Buka pause menu utama
                pauseMenuPanel.SetActive(true);
                AddPauseRequest();
            }
        }
    }

    // Fungsi ini dipanggil SETIAP KALI ada UI baru yang muncul dan butuh pause
    public void AddPauseRequest()
    {
        pauseRefCount++;
        UpdateTimeScale();
    }

    // Fungsi ini dipanggil SETIAP KALI ada UI yang ditutup
    public void RemovePauseRequest()
    {
        pauseRefCount--;

        // Pengaman agar angka tidak minus jika ada error di script lain
        if (pauseRefCount < 0) pauseRefCount = 0;

        UpdateTimeScale();
    }

    // Fungsi internal untuk mengatur jalannya waktu game
    private void UpdateTimeScale()
    {
        if (pauseRefCount > 0)
        {
            Time.timeScale = 0f; // Tetap freeze selama masih ada UI yang aktif
            Debug.Log($"Game Paused. Jumlah UI yang nge-pause: {pauseRefCount}");
        }
        else
        {
            Time.timeScale = 1f; // Waktu jalan lagi HANYA jika semua UI sudah ditutup (Count = 0)
            Debug.Log("Game Resumed. Semua UI pause sudah bersih.");
        }
    }
}
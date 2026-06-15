using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractSystem : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactRadius = 2f; // Jarak jangkauan lingkaran
    [SerializeField] private LayerMask interactLayer;

    private Collider2D hitCollider; // Menggantikan RaycastHit2D
    [SerializeField] private GameObject interactIcon;

    private void Update()
    {
        // Mendeteksi objek di dalam radius lingkaran dari posisi player
        hitCollider = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);

        // Mengaktifkan/menonaktifkan ikon interaksi
        if (interactIcon != null)
        {
            interactIcon.SetActive(hitCollider != null);
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        // "started" atau "performed" (tanpa hold) akan langsung memicu kode ini begitu tombol ditekan
        if (!ctx.performed) return;

        if (hitCollider != null)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                Debug.Log("Berhasil Interaksi lewat Lingkaran!");
                interactable.Interact();
            }
        }
    }

    // --- VISUALISASI GIZMOS LINGKARAN ---
    private void OnDrawGizmos()
    {
        // Jika mendeteksi objek di dalam area lingkaran
        if (Application.isPlaying && hitCollider != null)
        {
            Gizmos.color = Color.green; // Berubah hijau jika ada objek yang bisa diinteraksi
        }
        else
        {
            Gizmos.color = Color.red; // Merah jika kosong
        }

        // Menggambar lingkaran kawat di sekitar Player
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
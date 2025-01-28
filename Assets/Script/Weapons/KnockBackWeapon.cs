using UnityEngine;

public class KnockBackWeapon : MonoBehaviour
{
    public float knockbackAmount = 0.1f;  // Quantité de recul visuel
    public float knockbackDuration = 0.1f; // Durée du recul
    private Vector3 originalPosition; // Position de départ de l'arme
    private float knockbackTime = 0f; // Temps restant pour le recul

    void Start()
    {
        originalPosition = transform.localPosition;  // Enregistre la position d'origine de l'arme
    }

    void Update()
    {
        // Si l'arme est en recul, ramène-la à sa position originale progressivement
        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, knockbackTime / knockbackDuration);
        }
    }

    public void StartKnockback()
    {
        knockbackTime = knockbackDuration;  // Réinitialise le temps de recul
        transform.localPosition -= transform.right * knockbackAmount;  // Déplace l'arme en arrière
    }
}

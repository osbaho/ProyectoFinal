using UnityEngine;
using UnityEngine.UI;

public class AbilityIconUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image cooldownRadial;

    private Color normalColor;
    private Color cooldownColor = Color.gray;
    private float cooldownTime = 0f;
    private float cooldownLeft = 0f;
    private bool isOnCooldown = false;
    private Sprite originalSprite;

    void Start()
    {
        if (iconImage != null)
            normalColor = iconImage.color;
        else
            normalColor = Color.white;
    }

    public void SetIcon(Sprite sprite)
    {
        if (iconImage != null)
        {
            iconImage.sprite = sprite;
            originalSprite = sprite; // Siempre actualiza el sprite base aquí
        }
        else
        {
            Debug.LogWarning("AbilityIconUI: iconImage no asignado.");
        }
    }

    public void StartCooldown(float cooldown)
    {
        cooldownTime = cooldown;
        cooldownLeft = cooldown;
        isOnCooldown = true;
        SetGrayscale(true);
        if (cooldownRadial != null)
        {
            cooldownRadial.gameObject.SetActive(true);
            cooldownRadial.fillAmount = 1f;
        }
    }

    void Update()
    {
        if (isOnCooldown)
        {
            cooldownLeft -= Time.deltaTime;
            if (cooldownRadial != null && cooldownTime > 0)
                cooldownRadial.fillAmount = Mathf.Clamp01(cooldownLeft / cooldownTime);

            if (cooldownLeft <= 0f)
            {
                isOnCooldown = false;
                SetGrayscale(false);
                if (cooldownRadial != null)
                {
                    cooldownRadial.fillAmount = 0f;
                    cooldownRadial.gameObject.SetActive(false);
                }
                // Solo restaura el sprite si está vacío
                if (iconImage != null && iconImage.sprite == null && originalSprite != null)
                    iconImage.sprite = originalSprite;
            }
        }
    }

    private void SetGrayscale(bool value)
    {
        if (iconImage != null)
            iconImage.color = value ? cooldownColor : normalColor;
    }
}

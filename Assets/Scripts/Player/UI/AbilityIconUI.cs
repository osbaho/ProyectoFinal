using UnityEngine;
using UnityEngine.UI;

public class AbilityIconUI : MonoBehaviour
{
    public Image iconImage;
    public Image cooldownRadial;
    private Color normalColor = Color.white;
    private Color cooldownColor = Color.gray;
    private float cooldownTime = 0f;
    private float cooldownLeft = 0f;
    private bool isOnCooldown = false;

    public void SetIcon(Sprite sprite)
    {
        if (iconImage != null)
            iconImage.sprite = sprite;
    }

    public void StartCooldown(float cooldown)
    {
        cooldownTime = cooldown;
        cooldownLeft = cooldown;
        isOnCooldown = true;
        SetGrayscale(true);
        if (cooldownRadial != null)
            cooldownRadial.fillAmount = 1f;
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
                    cooldownRadial.fillAmount = 0f;
            }
        }
    }

    private void SetGrayscale(bool value)
    {
        if (iconImage != null)
            iconImage.color = value ? cooldownColor : normalColor;
    }
}

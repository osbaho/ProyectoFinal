using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider resourceBar;

    public MonoBehaviour playerComponent;
    private HealthComponent health;
    private ManaComponent mana;

    void Start()
    {
        var holderMB = playerComponent as MonoBehaviour;
        PlayableStatHolder psh = null;

        if (playerComponent is PlayableStatHolder p)
        {
            psh = p;
            health = psh.Health;
            mana = psh.Mana;
        }
        else if (playerComponent is NonPlayableStatHolder npsh)
        {
            health = npsh.Health;
            mana = null;
        }
        else if (holderMB != null)
        {
            health = holderMB.GetComponent<HealthComponent>();
            mana = holderMB.GetComponent<ManaComponent>();
        }
        else
        {
            Debug.LogError("PlayerUI: playerComponent no es un tipo válido.");
            health = null;
            mana = null;
        }

        if (playerComponent != null)
        {
            if (health != null)
            {
                health.OnHealthChanged += UpdateHealthBar;
                UpdateHealthBar(health.CurrentHealth, health.MaxHealth);
            }
            if (mana != null)
            {
                mana.OnManaChanged += UpdateResourceBar;
                UpdateResourceBar(mana.CurrentMana, mana.MaxMana);
            }
        }
    }

    void OnDestroy()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateHealthBar;
        if (mana != null)
            mana.OnManaChanged -= UpdateResourceBar;
    }

    void UpdateHealthBar(int current, int max)
    {
        UIUtils.SetSliderValue(healthBar, current, max);
    }

    void UpdateResourceBar(int current, int max)
    {
        UIUtils.SetSliderValue(resourceBar, current, max);
    }
}

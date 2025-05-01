using UnityEngine.UI;

public static class UIUtils
{
    public static void SetSliderValue(Slider slider, int current, int max)
    {
        if (slider != null)
            slider.value = (float)current / (max > 0 ? max : 1f);
    }
}

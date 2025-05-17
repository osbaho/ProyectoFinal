using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public static class UIUtils
    {
        public static void SetFillAmount(Image image, int current, int max)
        {
            if (image != null)
                image.fillAmount = (float)current / (max > 0 ? max : 1f);
        }
    }
}

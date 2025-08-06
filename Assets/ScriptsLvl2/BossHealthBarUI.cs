using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarUI : MonoBehaviour
{
    public Image fillImage; // Arrastrá la imagen "Fill" acá

    private void Start()
    {
        // Arranca invisible
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Setea la vida visual del jefe (0 a 1)
    /// </summary>
    public void SetHealth(float value, float max)
    {
        if (fillImage != null)
            fillImage.fillAmount = Mathf.Clamp01(value / max);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

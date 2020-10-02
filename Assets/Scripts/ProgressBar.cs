using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{

#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RadialProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    [SerializeField]
    private Image mask;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private Color fillColor;
    [SerializeField]
    private float maximum;
    [SerializeField]
    private float current;


    void Update()
    {
        UpdateProgressBarUI();
    }

    public void UpdateProgressBarUI()
    {
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
        fill.color = fillColor;
    }

    public void SetMaximum(float maximum)
    {
        this.maximum = maximum;
    }

    public void SetCurrent(float current)
    {
        this.current = current;
    }
}

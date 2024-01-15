using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)] //�ν����� â���� 0~1 ��ũ�ѷ� ���� ����
    public float DayTime;
    public float FullDayLength;  //�Ϸ�
    public float StartTime = 0.4f;
    private float _timeRate;
    public Vector3 Noon;  //���� ���� ����

    [Header("Sun")]
    public Light Sun;
    public Gradient SunColor;
    public AnimationCurve SunIntensity;

    [Header("Moon")]
    public Light Moon;
    public Gradient MoonColor;
    public AnimationCurve MoonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve LightingIntensityMultiplier;  //ȯ�汤
    public AnimationCurve ReflectionIntensityMultiplier;  //�ݻ籤

    private void Start()
    {
        _timeRate = 1.0f / FullDayLength; //��ŭ�� ���ϴ��� ��� 1/�Ϸ�
        DayTime = StartTime; //���۽ð��� ���ؼ� ��ħ���� �����ϴ� ������ �����͸� �����ϸ� �ʿ� ���� ��
    }

    private void Update()
    {
        DayTime = (DayTime + _timeRate * Time.deltaTime) % 1.0f; //�ۼ�Ƽ���� ����ϱ� ���� 1.0f�� ������. 0 ~ 0.9999 ������ ��밡��
        UpdateLighting(Sun, SunColor, SunIntensity);
        UpdateLighting(Moon, MoonColor, MoonIntensity);

        RenderSettings.ambientIntensity = LightingIntensityMultiplier.Evaluate(DayTime);
        RenderSettings.reflectionIntensity = ReflectionIntensityMultiplier.Evaluate(DayTime);
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(DayTime);  //AnimationCurve.Evaluate(*)�� *�� ���� Ŀ�갪�� �������ش�. (�׷���)

        lightSource.transform.eulerAngles = (DayTime - ((lightSource == Sun) ? 0.25f : 0.75f)) * Noon * 4.0f;
        lightSource.color = colorGradient.Evaluate(DayTime);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}

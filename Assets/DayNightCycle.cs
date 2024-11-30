using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField, Range(0, 30)] private float timeOfDay;
    [SerializeField] private float defaultSunRotationSpeed = 1f;
    [SerializeField] private float endlessSunRotationSpeed = 0.2f;

    [Header("Mode Settings")]
    public bool isEndlessMode = false;

    [Header("LightingPreset")]
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient sunColor;

    private float sunRotationSpeed;

    void Start()
    {
        // Set initial rotation speed based on mode
        UpdateRotationSpeed();
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if (timeOfDay > 30)
            timeOfDay = 0;

        SunRotation();
        UpdateLighting();
    }

    private void OnValidate()
    {
        UpdateRotationSpeed();
        SunRotation();
        UpdateLighting();
    }

    private void UpdateRotationSpeed()
    {
        // Set rotation speed based on mode
        sunRotationSpeed = isEndlessMode ? endlessSunRotationSpeed : defaultSunRotationSpeed;
    }

    private void SunRotation()
    {
        float sunRotation = Mathf.Lerp(-50, 230, timeOfDay / 30);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }

    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 30;
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
    }
}
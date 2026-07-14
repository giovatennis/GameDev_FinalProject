using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;

    [Header("Phase Lengths (seconds)")]
    public float dayLengthSeconds = 60f;
    public float nightLengthSeconds = 60f;
    public float transitionSeconds = 5f;

    private float dayIntensity = 0.2f;
    private float nightIntensity = 0.05f;

    private Color dayAmbientColor = new Color(0.55f, 0.58f, 0.62f);
    private Color nightAmbientColor = new Color(0.01f, 0.012f, 0.018f);

    private Color daySkyTint = new Color(0.35f, 0.45f, 0.6f);
    private Color nightSkyTint = new Color(0.005f, 0.0065f, 0.01f);

    public bool isNight { get; private set; }

    private enum Phase { Day, ToNight, Night, ToDay }
    private Phase phase = Phase.Day;
    private float phaseTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phase = Phase.Day;
        phaseTimer = 0f;
        isNight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(sun == null)
        {
            return;
        }

        phaseTimer += Time.deltaTime;

        switch(phase)
        {
            case Phase.Day:
                if(phaseTimer >= Mathf.Max(0f, dayLengthSeconds - transitionSeconds))
                {
                    phase = Phase.ToNight;
                    phaseTimer = 0f;
                }
                break;

            case Phase.ToNight:
                if(phaseTimer >= transitionSeconds)
                {
                    phase = Phase.Night;
                    phaseTimer = 0f;
                    isNight = true;
                }
                break;

            case Phase.Night:
                if(phaseTimer >= Mathf.Max(0f, nightLengthSeconds - transitionSeconds))
                {
                    phase = Phase.ToDay;
                    phaseTimer = 0f;
                }
                break;

            case Phase.ToDay:
                if(phaseTimer >= transitionSeconds)
                {
                    phase = Phase.Day;
                    phaseTimer = 0f;
                    isNight = false;
                }
                break;
        }

        UpdateLighting();
    }

    private void UpdateLighting()
    {
        float lightAmount = GetLightAmount();

        // simple day/night sun arc - tweak angles to match your scene's orientation
        float sunAngle = Mathf.Lerp(-10f, 190f, lightAmount);
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        sun.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lightAmount);

        RenderSettings.ambientLight = Color.Lerp(nightAmbientColor, dayAmbientColor, lightAmount);

        if(RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(nightSkyTint, daySkyTint, lightAmount));
            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(0.08f, 0.65f, lightAmount));
        }
    }

    // Returns 1 = full day brightness, 0 = full night darkness, blended during transitions
    private float GetLightAmount()
    {
        switch(phase)
        {
            case Phase.Day:
                return 1f;
            case Phase.Night:
                return 0f;
            case Phase.ToNight:
                return 1f - Mathf.Clamp01(phaseTimer / transitionSeconds);
            case Phase.ToDay:
                return Mathf.Clamp01(phaseTimer / transitionSeconds);
        }
        return 1f;
    }
}

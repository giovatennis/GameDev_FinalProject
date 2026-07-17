using UnityEngine;

public class DayNightMusic : MonoBehaviour
{

    public DayNightCycle dayNightCycle;

    public AudioSource dayMusic;
    public AudioSource nightMusic;

    private float fadeSpeed = 1f;
    private float dayMaxVolume = 0.06f;
    private float nightMaxVolume = 0.06f;

    private bool musicStarted;

    // Update is called once per frame
    void Update()
    {
        if(!musicStarted || dayNightCycle == null || dayMusic == null || nightMusic == null)
        {
            return;
        }

        float targetDayVolume = dayNightCycle.isNight ? 0f : dayMaxVolume;
        float targetNightVolume = dayNightCycle.isNight ? nightMaxVolume : 0f;

        dayMusic.volume = Mathf.MoveTowards(dayMusic.volume, targetDayVolume, fadeSpeed * Time.deltaTime);
        nightMusic.volume = Mathf.MoveTowards(nightMusic.volume, targetNightVolume, fadeSpeed * Time.deltaTime);
    }

    // Call this when returning to the start menu (pause -> quit) so day/night music doesn't
    // keep playing underneath the menu
    public void StopMusic()
    {
        musicStarted = false;
        if(dayMusic != null)
        {
            dayMusic.Stop();
        }
        if(nightMusic != null)
        {
            nightMusic.Stop();
        }
    }

    // Call this once the start menu closes so day/night music doesn't play underneath the menu
    public void StartMusic()
    {
        if(musicStarted)
        {
            return;
        }
        musicStarted = true;

        if(dayMusic != null)
        {
            dayMusic.loop = true;
            dayMusic.volume = 0f;
            dayMusic.Play();
        }
        if(nightMusic != null)
        {
            nightMusic.loop = true;
            nightMusic.volume = 0f;
            nightMusic.Play();
        }
    }
}

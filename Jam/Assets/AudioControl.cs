using UnityEngine;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour {

    public float filterThreshold = 10;
    public AudioMixer filterMixer;
    public AudioMixerSnapshot[] filterSnapshots;
    public float[] weights;
    
    public void BlendSnapshots(float value, float maxValue)
    {
        weights[0] = value;
        weights[1] = maxValue - value;
        filterMixer.TransitionToSnapshots(filterSnapshots, weights, 0.1f);
    }
}

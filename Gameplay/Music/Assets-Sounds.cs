using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    //references to different settings of a sound that shows up in Unity.

    public string name;
    public AudioClip clip;

    //Range allows us to define a range for volume and pitch in Unity
    [Range(0f,1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    //allows a sound to loop
    public bool loop;

    //Adds a public variable to be used in another class
    //HideInInspector hides the variable in Unity
    [HideInInspector]
    public AudioSource source;

}

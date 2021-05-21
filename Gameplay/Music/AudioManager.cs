using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //custom class that holds sounds
    public Sound[] sounds;

    //Makes sure we only have one instance of the AudioManager
    public static AudioManager instance;


    //Is used when the game is initialized
    void Awake()
    {

        //Destroys Audiomanager if there is more than one
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //makes our sounds unaffected while changing scenes, it will always play.
        DontDestroyOnLoad(gameObject);

        //looks for a sound in our Sound array
        foreach(Sound s in sounds)
        {

            //s.source allows us to use and controll the requested
            //setting, ie volume, pitch, loop, clip and Audiosource
            s.source = gameObject.AddComponent < AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //plays the main theme when the game starts!
    private void Start()
    {
        Play("Theme");
    }


    // public class that takes a name of a sound, looks for it in the
    //Sound array and plays it.
    public void Play (string name)
    {
        //lambda function that allows us to serach for a sound in Sound array.
        // Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));

        // checks that we're not trying to play a sound that isn't there!
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }

        //plays the sound
        s.source.Play();
    }
}

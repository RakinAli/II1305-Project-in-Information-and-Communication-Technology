using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
   public AudioSource buzzer;
   public AudioSource ka_ching;

    public void PlayBuzzer() {
        buzzer.Play();
    }

    public void PlayKa_Ching() {
        ka_ching.Play();
    }

}

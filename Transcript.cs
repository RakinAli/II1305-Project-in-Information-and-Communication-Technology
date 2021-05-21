using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transcript : MonoBehaviour
{
    [SerializeField] public Dialog text;
    public bool isDone;

    void Start()
    {
        isDone = false;
    }

}

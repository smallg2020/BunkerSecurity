using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform hourHandT, minuteHandT, secondHandT;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.clocks.Add(this);
    }

    public void UpdateTime(float[] t)
    {
        hourHandT.localRotation = Quaternion.Euler(new Vector3(0, 0, t[0] * 30));
        minuteHandT.localRotation = Quaternion.Euler(new Vector3(0, 0, t[1] * 6));
        secondHandT.localRotation = Quaternion.Euler(new Vector3(0, 0, t[2] * 6));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerNPC : MonoBehaviour
{
    [SerializeField]
    float burnTime = 1;
    [SerializeField]
    ParticleSystem flamerP;

    Animator anim;
    float flameTime = 0;
    bool burning = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (flameTime < 0)
            return;

        flameTime -= Time.deltaTime;
        if (flameTime < 0)
        {
            StartCoroutine(FlamerOff());
            burning = false;
        }
    }

    public void StartFlamer()
    {
        flameTime = burnTime;
        if (!burning)
        {
            StartCoroutine(FlamerOn());
            burning = true;
        }
    }

    IEnumerator FlamerOn()
    {
        anim.SetBool("FlamerOn", true);
        yield return new WaitForSeconds(0.5f);
        flamerP.Play();
    }

    IEnumerator FlamerOff()
    {
        flamerP.Stop();
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("FlamerOn", false);
    }
}

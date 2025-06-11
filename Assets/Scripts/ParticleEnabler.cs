using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ParticleEnabler : MonoBehaviour
{

    public VisualEffect _visualEffect;

    public void Start()
    {
        StartCoroutine(EnableAndDisable());
    }

    IEnumerator EnableAndDisable()
    {
        _visualEffect.enabled = true;
        yield return new WaitForSeconds(5.0f);
        _visualEffect.enabled = false;
        yield return new WaitForSeconds(5.0f);

        StartCoroutine(EnableAndDisable());
    }
}

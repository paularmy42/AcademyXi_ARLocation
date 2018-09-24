using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineExample : MonoBehaviour
{

    public Transform cube;
    public Renderer cubeRenderer;
    public float duration;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Fader(new Color(1f, 0.5f, 001f)));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator TestCoroutine()
    {
        cube.Translate(0.5f, 0f, 0f);
        yield return new WaitForSeconds(2f);
        cube.Translate(-0.5f, 0f, 0f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TestCoroutine());
    }

    IEnumerator Fader(Color _targetColour)
    {
        Color _startColour = Color.white;
        float t = 0;
        while (t < 1f)
        {
            Debug.Log(t);
            cubeRenderer.material.color = Color.Lerp(_startColour, _targetColour, t);
            t += Time.deltaTime / duration;
            yield return null;
        }
        yield break;
    }

}

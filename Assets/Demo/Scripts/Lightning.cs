using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour 
{
	Light light;
	public Vector2 intensity = new Vector2(10f, 50f);
	public Vector2 speed = new Vector2(2f, 5f);
	float targetIntensity;
	float targetSpeed;
	[Range(0f, 100f)]
	public float lightningPercentage = 1f;
	bool isActive = false;

	void Start()
	{
		light = GetComponent<Light>();
	}

	void Update () 
	{
		float x = Random.Range(0, 100);
		if(x > 100 - lightningPercentage)
		{
			if(!isActive)
			{
				targetIntensity = Random.Range(intensity.x, intensity.y);
				targetSpeed = Random.Range(speed.x, speed.y);
				StartCoroutine(IncreaseLightning());
			}	
		}
	}

	IEnumerator IncreaseLightning()
	{
		isActive = true;
		while(light.intensity < targetIntensity)
		{
			light.intensity += Time.deltaTime * 100f * targetSpeed;
			yield return null;
		}
		StartCoroutine(DecreaseLightning());
	}

	IEnumerator DecreaseLightning()
	{
		while(light.intensity > 0f)
		{
			light.intensity -= Time.deltaTime * 100f * targetSpeed;
			yield return null;
		}
		isActive = false;
	}
}

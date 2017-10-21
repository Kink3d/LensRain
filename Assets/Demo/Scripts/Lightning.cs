using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour 
{
	Light lightningLight; // Light component
	public Vector2 intensity = new Vector2(10f, 50f); // Min/max intensity
	public Vector2 speed = new Vector2(2f, 5f); // Min/max speed
	float targetIntensity; // Randomly generated intensity
	float targetSpeed; // Randomly generated speed
	[Range(0f, 100f)] public float lightningPercentage = 1f; // Percentage of lightning per frame
	bool isActive = false; // Is currently lightning active

	void Start()
	{
		lightningLight = GetComponent<Light>(); // Get light
	}

	void Update () 
	{
		float x = Random.Range(0, 100); // Get random number
		if(x > 100 - lightningPercentage) // If within lighting percentage
		{
			if(!isActive) // If not currently active lightning
			{
				targetIntensity = Random.Range(intensity.x, intensity.y); // Get intensity from range
				targetSpeed = Random.Range(speed.x, speed.y); // Get speed from range
				StartCoroutine(IncreaseLightning()); // Start lightning
			}	
		}
	}

	IEnumerator IncreaseLightning()
	{
		isActive = true; // Set active
		while(lightningLight.intensity < targetIntensity) // While increasing
		{
			lightningLight.intensity += Time.deltaTime * 100f * targetSpeed; // Increase light
			yield return null; // Yield
		}
		StartCoroutine(DecreaseLightning()); // Start decrease
	}

	IEnumerator DecreaseLightning()
	{
		while(lightningLight.intensity > 0f) // Whilse decreasing
		{
			lightningLight.intensity -= Time.deltaTime * 100f * targetSpeed; // Decrease
			yield return null; // Yield
		}
		isActive = false; // Set inactive
	}
}

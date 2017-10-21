using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Settings class
[Serializable]
[PostProcess(typeof(LensRainRenderer), PostProcessEvent.AfterStack, "Custom/Lens Rain")]
public class LensRain : PostProcessEffectSettings 
{
	public BoolParameter debug = new BoolParameter { value = false };
	[Range(0f, 1f)] public FloatParameter strength = new FloatParameter { value = 0f };
	[Range(0f, 1f)] public FloatParameter amount = new FloatParameter { value = 0.5f };
	[Range(0f, 1f)] public FloatParameter size = new FloatParameter { value = 0.5f };
	[Range(-1f, 1f)] public FloatParameter direction = new FloatParameter { value = 0f };
}

// Renderer class
public sealed class LensRainRenderer : PostProcessEffectRenderer<LensRain>
{
	// Called on render
	public override void Render(PostProcessRenderContext context)
    {
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/PostProcessing/Extensions/LensRain")); // Get a property sheet

		InitializeObjects(context.camera); // Initialize objects
		LensRainRelay.Instance.RelayParameters(settings); // Relay settings

		sheet.properties.SetFloat("_Debug", settings.debug == true ? 1 : 0); // Set debug
		sheet.properties.SetTexture("_RainTex", LensRainRelay.Instance.m_LensRainTexture); // Set texture
		sheet.properties.SetFloat("_Strength", settings.strength); // Set strength
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0); // Blit
    }

	// Called when layer is disabled
	public override void Release()
	{
		if(LensRainRelay.Instance != null) // If RainRelay exists
			LensRainRelay.Instance.Destroy(); // Call destroy on it
	}

	// Initialize objects
	void InitializeObjects(Camera camera)
	{
		if(LensRainRelay.Instance == null) // If RainRelay doesnt exist
			camera.gameObject.AddComponent<LensRainRelay>(); // Create it
	}
}

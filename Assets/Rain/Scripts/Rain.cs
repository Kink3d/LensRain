using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

// Settings class
[Serializable]
[PostProcess(typeof(RainRenderer), PostProcessEvent.AfterStack, "Custom/Rain")]
public class Rain : PostProcessEffectSettings 
{
	public BoolParameter debug = new BoolParameter { value = false };
	[Range(0f, 1f)] public FloatParameter strength = new FloatParameter { value = 0f };
	[Range(0f, 1f)] public FloatParameter amount = new FloatParameter { value = 0.5f };
	[Range(0f, 1f)] public FloatParameter size = new FloatParameter { value = 0.5f };
	[Range(-1f, 1f)] public FloatParameter direction = new FloatParameter { value = 0f };
}

// Renderer class
public sealed class RainRenderer : PostProcessEffectRenderer<Rain>
{
	// Called on render
	public override void Render(PostProcessRenderContext context)
    {
		var sheet = context.propertySheets.Get(Shader.Find("Hidden/PostProcessing/Extensions/Rain")); // Get a property sheet

		InitializeObjects(context.camera); // Initialize objects
		RainRelay.Instance.RelayParameters(settings); // Relay settings

		sheet.properties.SetFloat("_Debug", settings.debug == true ? 1 : 0); // Set debug
		sheet.properties.SetTexture("_RainTex", RainRelay.Instance.m_RainTexture); // Set texture
		sheet.properties.SetFloat("_Strength", settings.strength); // Set strength
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0); // Blit
    }

	// Called when layer is disabled
	public override void Release()
	{
		if(RainRelay.Instance != null) // If RainRelay exists
			RainRelay.Instance.Destroy(); // Call destroy on it
	}

	// Initialize objects
	void InitializeObjects(Camera camera)
	{
		if(RainRelay.Instance == null) // If RainRelay doesnt exist
			camera.gameObject.AddComponent<RainRelay>(); // Create it
	}
}

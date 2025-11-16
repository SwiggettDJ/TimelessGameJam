Diffuse = 0;
Color = float3(0,0,0);

#ifndef SHADERGRAPH_PREVIEW

    uint pixelLightCount = GetAdditionalLightsCount();
	
	LIGHT_LOOP_BEGIN(pixelLightCount)
		//get light color and direction
		lightIndex = GetPerObjectLightIndex(lightIndex);
		Light light = GetAdditionalPerObjectLight(lightIndex, WorldPosition);
		
		//calculate shadows
		light.shadowAttenuation = AdditionalLightRealtimeShadow(lightIndex, WorldPosition, light.direction);
		float atten = light.distanceAttenuation * light.shadowAttenuation;
		
		//calculate diffuse and specular
		float NdotL = saturate(dot(WorldNormal, light.direction));
		float thisDiffuse = atten * NdotL;
		
		//accumulate light
		Diffuse += thisDiffuse;
		Color += light.color * thisDiffuse;
	LIGHT_LOOP_END

#endif
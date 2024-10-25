shader_type spatial;
render_mode specular_disabled, shadows_disabled, ambient_light_disabled;

uniform vec3 start_color : source_color;
uniform vec3 end_color : source_color;
const vec3 lightDirection = vec3(-0.5, 1.0, 0.5);
void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
	
	float light = clamp( dot(NORMAL, lightDirection), 0.0, 1.0);
	DIFFUSE_LIGHT = mix(end_color, start_color, light);
	//DIFFUSE_LIGHT = color_lerp(start_color, end_color, light);
}

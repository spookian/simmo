shader_type spatial;
render_mode blend_mix, unshaded, depth_draw_opaque;

global uniform vec4 highlight_color;

instance uniform bool highlighted;
instance uniform bool billboarded;
uniform sampler2D tex : filter_nearest;
uniform vec3 base_color;

void vertex() {
	// Called for every vertex the material is visible on.
	if (billboarded)
	{
		MODELVIEW_MATRIX = VIEW_MATRIX * mat4(
			MAIN_CAM_INV_VIEW_MATRIX[0],
			MAIN_CAM_INV_VIEW_MATRIX[1],
			MAIN_CAM_INV_VIEW_MATRIX[2],
			MODEL_MATRIX[3]);

		MODELVIEW_MATRIX = MODELVIEW_MATRIX * mat4(
		vec4(length(MODEL_MATRIX[0].xyz), 0.0, 0.0, 0.0),
		vec4(0.0, length(MODEL_MATRIX[1].xyz), 0.0, 0.0),
		vec4(0.0, 0.0, length(MODEL_MATRIX[2].xyz), 0.0),
		vec4(0.0, 0.0, 0.0, 1.0));
		MODELVIEW_NORMAL_MATRIX = mat3(MODELVIEW_MATRIX);
	}
}

void fragment() {

	// Called for every pixel the material is visible on.
	float nalpha = highlight_color.w;
	vec4 col = texture(tex, UV) * vec4(base_color, 1.0);

	if (highlighted == true)
	{
		ALBEDO = col.xyz * (1.0 - nalpha) + highlight_color.xyz * nalpha;
		ALPHA = col.w;
	}
	else
	{
		ALBEDO = col.xyz;
		ALPHA = col.w;
	}
	ALPHA_SCISSOR_THRESHOLD = 0.5;
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}

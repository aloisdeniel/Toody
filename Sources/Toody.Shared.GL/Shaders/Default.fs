#version 300 es

in highp vec2 v_coordinates;
in lowp vec4 v_color;
uniform lowp sampler2D textureSampler;
out lowp vec4 color;

void main()               
{                          
	color = v_color * texture(textureSampler, v_coordinates);
}
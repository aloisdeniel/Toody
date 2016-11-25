#version 300 es

in highp vec2 v_coordinates;
uniform lowp sampler2D textureSampler;
out lowp vec4 color;

void main()               
{                          
	color = texture(textureSampler, v_coordinates);
}
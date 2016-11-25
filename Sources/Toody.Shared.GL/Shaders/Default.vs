#version 300 es

in vec4 i_position;

uniform mat4 u_projection;

out vec2 v_coordinates;

void main()
{
    gl_Position = u_projection * vec4(i_position.xy, 0.0f, 1.0f); ;
	v_coordinates = i_position.zw;
}
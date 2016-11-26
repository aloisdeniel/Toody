#version 300 es

in vec4 i_position;
in vec4 i_color;

uniform mat4 u_projection;
uniform mat4 u_view;

out vec2 v_coordinates;
out vec4 v_color;

void main()
{
    gl_Position = u_projection * u_view * vec4(i_position.xy, 0.0f, 1.0f);
	v_coordinates = i_position.zw;
	v_color = i_color;
}
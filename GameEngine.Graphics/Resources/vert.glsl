#version 460 core

layout (location = 0) in vec3 in_position;
layout (location = 1) in vec2 in_uvs;

uniform mat4 model_matrix;
uniform mat4 view_matrix;
uniform mat4 proj_matrix;

out vec2 TextCoords;

void main()
{
	TextCoords = in_uvs;
	gl_Position = proj_matrix * view_matrix * model_matrix * vec4(in_position, 1.0);
}
#version 460 core

in vec2 TextCoords;

out vec4 out_color;

void main()
{
    out_color = vec4(TextCoords.x, TextCoords.y, 0.0, 1.0);
}
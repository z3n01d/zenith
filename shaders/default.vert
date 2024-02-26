#version 460 core

layout (location = 0) in vec3 vertexPosition;
layout (location = 1) in vec2 textureCoordinate;

out vec2 uv;

uniform mat4 viewMatrix;

void main()
{
    uv = textureCoordinate;
    gl_Position = viewMatrix * vec4(vertexPosition, 1.0);
}
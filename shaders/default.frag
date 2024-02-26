#version 460 core

out vec4 fragColor;

in vec2 uv;

void main()
{
    //fragColor = vec4(1.0,1.0,1.0,1.0);
    fragColor = vec4(uv,1.0,1.0);
}
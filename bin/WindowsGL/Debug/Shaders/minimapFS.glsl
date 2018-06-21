#version 330 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D minimapTexture;

void main()
{             
    FragColor = vec4(vec3(texture(minimapTexture, TexCoords)), 1.0);
    //FragColor = vec4(1.0, 1.0, 1.0 , 1.0);
}
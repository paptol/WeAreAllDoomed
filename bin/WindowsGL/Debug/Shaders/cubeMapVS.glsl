#version 330 core
layout (location = 0) in vec3 aPos;

out vec3 TexCoords;

uniform mat4 projection;
uniform mat4 view;


void main()
{
    TexCoords = aPos;
	// Setting the z component as the w component so perspective division results in
	// the cubemap being drawn in front with value 1 for depth
	vec4 pos = projection * view * vec4(aPos, 1.0);
    gl_Position = pos.xyww;
}  
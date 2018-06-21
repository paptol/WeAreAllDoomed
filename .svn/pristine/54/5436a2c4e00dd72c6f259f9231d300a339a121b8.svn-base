#version 330
 
in vec3 a_Position;
in vec3 a_Normal;
in vec2 a_TexCoord;
in vec3 a_Tangent;
in vec3 a_Bitangent;

out vec3 v_FragPos;
out vec2 v_TexCoord;
out vec3 v_Normal;
out mat3 v_TBN;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;


void main()
{
	gl_Position = projection * view * model * vec4(a_Position, 1.0);
	v_FragPos = vec3(model * vec4(a_Position, 1.0));
	
	// Adjusting the normal for non uniform transformations.
	v_Normal = mat3(transpose(inverse(model))) * a_Normal;

	v_TexCoord = a_TexCoord;


	// Setting up TBN matrix for normal mapping
	vec3 T = normalize(vec3(model * vec4(a_Bitangent,   0.0)));
    vec3 N = normalize(vec3(model * vec4(a_Normal,    0.0)));

	// applying the Gram-Schmidt process to re-orthogonalize TBN vectors.
    // re-orthogonalize T with respect to N
	T = normalize(T - dot(T, N) * N);
	// then retrieve perpendicular vector B with the cross product of T and N
	vec3 B = cross(N, T);

	v_TBN = mat3(T, B, N);

}
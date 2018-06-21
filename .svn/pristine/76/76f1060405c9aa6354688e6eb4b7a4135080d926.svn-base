#version 330 core
struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

struct PointLight {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
struct SpotLight {
    vec3 position;
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;   

    float cutOff;
    float outerCutOff; 
	float constant;
    float linear;
    float quadratic;
};

  
#define NR_POINT_LIGHTS 4 
uniform PointLight lights[NR_POINT_LIGHTS];
uniform SpotLight flashLight;
Material material;

uniform vec3 viewPos;
uniform sampler2D s_texture;
uniform sampler2D s_normal;

in vec3 v_Normal;
in vec3 v_FragPos;
in vec2 v_TexCoord;
in mat3 v_TBN;

out vec4 FragColor;

bool textureOn = true;
uniform bool isNormalMap;
  
// Function prototypes
vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir); 
vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir);

void main() 
{
	material.ambient = vec3(1.0,0.5,0.31);
	material.diffuse = vec3(1.0,0.5,0.31);
	material.specular = vec3(0.5,0.5,0.5);
	material.shininess = 32.0;

    vec3 result =vec3(0.0,0.0,0.0); 
	vec3 norm;

	if (isNormalMap)
	{
		// obtain normal from normal map in range [0,1]
		vec3 normal = texture(s_normal, v_TexCoord).rgb;
		// transform normal vector to range [-1,1]
		norm = normalize(normal * 2.0 - 1.0); 
		// transform the normal vector into tangent space to correct normal mapping 
		norm = normalize(v_TBN * norm); 
	}
	else
	{
		norm = normalize(v_Normal);
	}


	vec3 viewDir = normalize(viewPos - v_FragPos);

	// Swapping the normals if the player is on the opposite side of the quad
	vec3 viewToFrag = normalize(v_FragPos - viewPos);
	float directionViewToFrag = dot(viewToFrag, v_Normal);
	if (directionViewToFrag > 0) // If they face same direction
	{
			norm = vec3 (-norm.x,-norm.y,-norm.z); // swap the normal
	}

	// calculating all the point lights
	for(int i = 0; i < NR_POINT_LIGHTS; i++)
		result += CalcPointLight(lights[i], norm, v_FragPos, viewDir);
			

	//result += CalcSpotLight(flashLight, norm, v_FragPos, viewDir);


   FragColor = vec4(result, 1.0);
}

vec3 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
	vec3 lightDir = normalize(light.position - v_FragPos);
	float diff = max(dot(normal, lightDir), 0.0);

	//SPECULAR 
	vec3 reflectDir = reflect(-lightDir, normal);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

	// Calculate the result
	vec3 ambient;
	vec3 diffuse; 
	vec3 specular;

	if (textureOn)
	{
		ambient  = light.ambient * vec3(texture(s_texture, v_TexCoord));;
		diffuse  = light.diffuse * diff * vec3(texture(s_texture, v_TexCoord));
		specular = light.specular * spec * vec3(texture(s_texture, v_TexCoord)); 
	}
	else // material render
	{
		ambient  = light.ambient * material.ambient;
		diffuse  = light.diffuse * diff * material.diffuse;
		specular = light.specular * spec * material.specular;   
	}

	return ambient + diffuse + specular;
}

// calculates the color when using a spot light.
vec3 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
        // DIFFUSE
		vec3 lightDir = normalize(light.position - fragPos);
        float diff = max(dot(normal, lightDir), 0.0);
        //SPECULAR 
        vec3 reflectDir = reflect(-lightDir, normal);
        float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
        
		// Calculate the result
		vec3 ambient;
		vec3 diffuse; 
		vec3 specular;

		if (textureOn)
		{
			ambient  = light.ambient * vec3(texture(s_texture, v_TexCoord));;
			diffuse  = light.diffuse * diff * vec3(texture(s_texture, v_TexCoord));;
			specular = light.specular * spec * vec3(texture(s_texture, v_TexCoord));;   
		}
		else // material render
		{
			ambient  = light.ambient * material.ambient;
			diffuse  = light.diffuse * diff * material.diffuse;
			specular = light.specular * spec * material.specular;   
		}



		// Spotlight soft edges
		float theta     = dot(lightDir, normalize(-light.direction));
		float epsilon   = light.cutOff - light.outerCutOff;
		float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);   
		diffuse  *= intensity;
		specular *= intensity;

		// Attenuation
		float SpotDistance = length(light.position - fragPos);
		float SpotAttenuation = 1.0 / (light.constant + light.linear * SpotDistance + light.quadratic * (SpotDistance * SpotDistance));
		ambient  *= SpotAttenuation; 
		diffuse  *= SpotAttenuation;
        specular *= SpotAttenuation; 

		// RESULT SPOTLIGHT
		return ambient + diffuse + specular;
}
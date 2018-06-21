using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;

namespace OpenGL_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    ///   /// <param name="e">Provides a snapshot of timing values.</param>
    public class MyGame : GameWindow
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1100;

        public Matrix4 projection;
        EntityManager entityManager;
        SystemManager systemManager;
        
        CubeMap skybox = new CubeMap();
        Quad minimap = new Quad();

        public static float dt;
        public static float dtt;
        public Camera playerCamera;
        private static Vector2 oldCameraPosition;
        private static Vector2 newCameraPosition;
        public PathFindingNodes pathFinding;

        public static MyGame gameInstance;

        public static MazeLevel currentLevelLoaded;

        public MyGame() : base(WIDTH, HEIGHT)
        {
            gameInstance = this;

            playerCamera = new Camera();

            entityManager = new EntityManager();
            systemManager = new SystemManager();
            AudioContext AC = new AudioContext();
        }

        private void CreateEntities()
        {
            Entity newEntity;

            Vector3 emPos = new Vector3(10.0f, 10.0f, 10.0f);

            newEntity = new Entity("Player");
            newEntity.AddComponent(new ComponentInput());
            newEntity.AddComponent(new ComponentPosition(0.0f, 0.0f, 0.0f));
            newEntity.AddComponent(new ComponentVelocity(0, 0, 0));
            newEntity.AddComponent(new ComponentScale(0, 0, 0));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.green));       
            newEntity.AddComponent(new ComponentAudioEmitter("player_shot", emPos));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            currentLevelLoaded = new Level1();
            
            currentLevelLoaded.loadEntities(entityManager);
            pathFinding = new PathFindingNodes();
            newEntity = new Entity("Drone1");

            Vector3 emiPos = new Vector3(0.0f, 0.0f, 0.0f);


            newEntity.AddComponent(new ComponentPosition(4f, 0.0f, -7f));
            newEntity.AddComponent(new ComponentRotation(0, 0, 0));
            newEntity.AddComponent(new ComponentScale(0.2f, 0.2f, 0.2f));
            newEntity.AddComponent(new ComponentAI());
            newEntity.AddComponent(new ComponentVelocity(0, 0, 0));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.red));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/drone/diffuse.png"));
            newEntity.AddComponent(new ComponentTexture("Textures/drone/normal.png"));
            newEntity.AddComponent(new ComponentAudioEmitter("drone_disable", emiPos));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);


            newEntity = new Entity("Drone2");
         

            newEntity.AddComponent(new ComponentPosition(22f, 0.0f, -23f));
            newEntity.AddComponent(new ComponentRotation(0, 0, 0));
            newEntity.AddComponent(new ComponentScale(0.2f, 0.2f, 0.2f));
            newEntity.AddComponent(new ComponentAI());
            newEntity.AddComponent(new ComponentVelocity(0, 0, 0));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.red));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/drone/diffuse.png"));
            newEntity.AddComponent(new ComponentTexture("Textures/drone/normal.png"));
            newEntity.AddComponent(new ComponentAudioEmitter("drone_disable", emiPos));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Drone3");


            newEntity.AddComponent(new ComponentPosition(15f, 0.0f, -10f));
            newEntity.AddComponent(new ComponentRotation(0, 0, 0));
            newEntity.AddComponent(new ComponentScale(0.2f, 0.2f, 0.2f));
            newEntity.AddComponent(new ComponentAI());
            newEntity.AddComponent(new ComponentVelocity(0, 0, 0));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.red));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/drone/diffuse.png"));
            newEntity.AddComponent(new ComponentTexture("Textures/drone/normal.png"));
            newEntity.AddComponent(new ComponentAudioEmitter("drone_disable", emiPos));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Health");

            Vector3 ePos = new Vector3(11f, -0.1f, -13.5f);

            newEntity.AddComponent(new ComponentPosition(11f, -0.1f, -13.5f));
            newEntity.AddComponent(new ComponentRotation(0f, 90f, 0f));
            newEntity.AddComponent(new ComponentScale(0.1f, 0.2f, 0.1f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/heart.png"));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.black));
            newEntity.AddComponent(new ComponentAudioEmitter("item_collect", ePos));
            newEntity.AddComponent(new ComponentPickUp(0, 50, 0));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);


            newEntity = new Entity("Ammo");

            Vector3 eePos = new Vector3(11f, -0.1f, -14.5f);

            newEntity.AddComponent(new ComponentPosition(11f, -0.1f, -14.5f));
            newEntity.AddComponent(new ComponentRotation(0f, 90f, 0f));
            newEntity.AddComponent(new ComponentScale(0.1f, 0.25f, 0.1f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/Ammo.png"));
            newEntity.AddComponent(new ComponentAudioEmitter("item_collect", eePos));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.black));
            newEntity.AddComponent(new ComponentPickUp(10, 0, 0));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Drone_Dea");

            Vector3 emitPos = new Vector3(12f, -0.1f, -12.5f);

            newEntity.AddComponent(new ComponentPosition(12f, -0.1f, -12.5f));
            newEntity.AddComponent(new ComponentRotation(0f, 90f, 0f));
            newEntity.AddComponent(new ComponentScale(0.1f, 0.2f, 0.1f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/robot.png"));
            newEntity.AddComponent(new ComponentAudioEmitter("item_collect", emitPos));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.black));
            newEntity.AddComponent(new ComponentPickUp(0, 0, 5));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Drone_Dea1");

            Vector3 tPos = new Vector3(12f, -0.1f, -12.5f);

            newEntity.AddComponent(new ComponentPosition(20f, -0.1f, -15f));
            newEntity.AddComponent(new ComponentRotation(0f, 90f, 0f));
            newEntity.AddComponent(new ComponentScale(0.1f, 0.2f, 0.1f));
            newEntity.AddComponent(new ComponentGeometry("Geometry/cubeGeometry.txt"));
            newEntity.AddComponent(new ComponentTexture("Textures/robot.png"));
            newEntity.AddComponent(new ComponentAudioEmitter("power_item_sound", tPos));
            newEntity.AddComponent(new ComponentMinimapTrack(ComponentMinimapTrack.squareColour.black));
            newEntity.AddComponent(new ComponentPickUp(0, 0, 5));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            // CREATING LIGHT ENTITIES
            Vector3 ambient = new Vector3(0.08f, 0.08f, 0.08f);
            Vector3 diffuse = new Vector3(0.1f, 0.1f, 0.1f);
            Vector3 specular = new Vector3(0.5f, 0.5f, 0.5f);

            newEntity = new Entity("pointLight");
            newEntity.AddComponent(new ComponentPosition(new Vector3(6.25f, 10.0f, -6.25f)));
            newEntity.AddComponent(new ComponentLightEmitter(ambient, diffuse, specular));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("pointLight");
            newEntity.AddComponent(new ComponentPosition(new Vector3(6.25f, 10.0f, -18.75f)));
            newEntity.AddComponent(new ComponentLightEmitter(ambient, diffuse, specular));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("pointLight");
            newEntity.AddComponent(new ComponentPosition(new Vector3(18.75f, 10.0f, -6.25f)));
            newEntity.AddComponent(new ComponentLightEmitter(ambient, diffuse, specular));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("pointLight");
            newEntity.AddComponent(new ComponentPosition(new Vector3(18.75f, 10.0f, -18.75f)));
            newEntity.AddComponent(new ComponentLightEmitter(ambient, diffuse, specular));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("spotlight");
            newEntity.AddComponent(new ComponentPosition(playerCamera.Position));
            newEntity.AddComponent(new ComponentLightEmitter(
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(0.8f, 0.8f, 0.8f),
                new Vector3(1.0f, 1.0f, 1.0f)));
            newEntity.AddComponent(new ComponentLightDirection(playerCamera.Front, 12.5f, 17.5f));
            newEntity.AddComponent(new ComponentAlive());
            entityManager.AddEntity(newEntity);
        }

        

        public static float DotProduct(Vector2 vA, Vector2 vB)
        {
            float dot = (vA.X * vB.X) + (vA.Y * vB.Y);
            return dot;
        }


        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemMinimap(minimap);
            systemManager.AddSystem(newSystem);
            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);
            newSystem = new SystemLighting();
            systemManager.AddSystem(newSystem);
            newSystem = new SystemInput();
            systemManager.AddSystem(newSystem);
            newSystem = new SystemPhysics();
            systemManager.AddSystem(newSystem);
            newSystem = new SystemAI();
            systemManager.AddSystem(newSystem);
            newSystem = new SystemPickUp();
            systemManager.AddSystem(newSystem);
            newSystem = new SystemAudio();
            systemManager.AddSystem(newSystem);
        }

        /// <summary>
        /// Allows the game to setup the environment and matrices.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            this.CursorVisible = false;
            //GL.Enable(EnableCap.CullFace);



            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), WIDTH / HEIGHT, 0.01f, 100f);

            CreateEntities();
            CreateSystems();
            skybox.setupSkybox();
            minimap.setup();

            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // ------------------------ TIMING ------------------------
            dt = (float)(e.Time);
            // TODO: Add your update logic here

            OldCameraPosition = new Vector2(playerCamera.Position.X, playerCamera.Position.Z);          
            systemManager.ActionSystems(entityManager);
            

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            skybox.renderCubemap();

            systemManager.RenderSystems(entityManager);
            minimap.Render();



            GL.Flush();
            SwapBuffers();
        }

        public static Vector2 OldCameraPosition
        {
            get { return oldCameraPosition; }
            set { oldCameraPosition = value; }
        }

        public static Vector2 NewCameraPosition
        {
            get { return newCameraPosition; }
            set { newCameraPosition = value; }
        }


        /// <summary>
        /// Mouse is contained inside the GameWindow class.
        /// </summary>
        public static Vector2 GetMousePosition()
        {
            return new Vector2(gameInstance.Mouse.X, gameInstance.Mouse.Y);
        }

        public static MouseDevice GetMouse()
        {
            return gameInstance.Mouse;
        }

        public static void ExitGame()
        {
            gameInstance.Exit();
        }
    }
}

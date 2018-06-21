using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using OpenGL_Game.Objects;
using System.IO;
using OpenTK.Audio.OpenAL;
using OpenTK;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;

namespace OpenGL_Game.Managers
{
    public static class ResourceManager
    {
        static Dictionary<string, Geometry> geometryDictionary = new Dictionary<string, Geometry>();
        static Dictionary<string, int> textureDictionary = new Dictionary<string, int>();
        static Dictionary<string, int> soundDictionary = new Dictionary<string, int>();

        static Vector3 listenerPosition;
        static Vector3 listenerDirection;
        static Vector3 listenerUp;
        public static Geometry LoadGeometry(string filename, bool LoadTangents)
        {
            Geometry geometry;
            geometryDictionary.TryGetValue(filename, out geometry);
            if (geometry == null)
            {
                geometry = new Geometry();
                geometry.LoadObject(filename, LoadTangents);
                geometryDictionary.Add(filename, geometry);
            }

            return geometry;
        }

        internal static ASound LoadWave(Vector3 emitPos, string soundName)
        {
            throw new NotImplementedException();
        }

        public static int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int texture;
            textureDictionary.TryGetValue(filename, out texture);
            if (texture == 0)
            {
                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);

                // We will not upload mipmaps, so disable mipmapping (otherwise the texture will not appear).
                // We can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
                // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, ((int)All.Repeat));
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, ((int)All.Repeat));
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);








                 
           
                Bitmap bmp = new Bitmap(filename);
                BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

                bmp.UnlockBits(bmp_data);
            }
 
            return texture;
        }


        internal static byte[] LoadWave(FileStream fileStream, out int channels, out int bits, out int rate, out int chunkSize)
        {
            throw new NotImplementedException();
        }

        //This is used for playing an individual sound, not for beginning of the project
        public static ASound LoadAudio(Vector3 emitterPosition, string soundName)
        {
            // Setup OpenAL Listener - each component listener has these now
            listenerPosition = new Vector3(0, 0, 3);
            listenerDirection = new Vector3(0, 0, -1);
            listenerUp = Vector3.UnitY;

            // reserve a Handle for the audio file
            int myBuffer = AL.GenBuffer();
            
            // Load a .wav file from disk.
            int channels, bits_per_sample, sample_rate;
            byte[] sound_data = LoadWave(
                File.Open("Audio/" + soundName + ".wav", FileMode.Open),
                out channels,
                out bits_per_sample,
                out sample_rate);
            ALFormat sound_format =
                channels == 1 && bits_per_sample == 8 ? ALFormat.Mono8 :
                channels == 1 && bits_per_sample == 16 ? ALFormat.Mono16 :
                channels == 2 && bits_per_sample == 8 ? ALFormat.Stereo8 :
                channels == 2 && bits_per_sample == 16 ? ALFormat.Stereo16 :
                (ALFormat)0; // unknown
            AL.BufferData(myBuffer, sound_format, sound_data, sound_data.Length, sample_rate);
            if (AL.GetError() != ALError.NoError)
            {
                // respond to load error etc.
            }

            // Create a sounds source using the audio clip
            int mySource = AL.GenSource(); // gen a Source Handle
             AL.Source(mySource, ALSourcei.Buffer, myBuffer); // attach the buffer to a source
            AL.Source(mySource, ALSourceb.Looping, true); // source loops infinitely
            emitterPosition = new Vector3(0.0f, 0.0f, 0.0f);
            AL.Source(mySource, ALSource3f.Position, ref emitterPosition);
            AL.SourcePlay(mySource);


            int myBuffer1 = AL.GenBuffer();
            int channels1, bits_per_sample1, sample_rate1;
            byte[] sound_data1 = LoadWave(
                File.Open("Audio/battle.wav", FileMode.Open),
                out channels1,
                out bits_per_sample1,
                out sample_rate1);
            ALFormat sound_format1 =
                channels1 == 1 && bits_per_sample1 == 8 ? ALFormat.Mono8 :
                channels1 == 1 && bits_per_sample1 == 16 ? ALFormat.Mono16 :
                channels1 == 2 && bits_per_sample1 == 8 ? ALFormat.Stereo8 :
                channels1 == 2 && bits_per_sample1 == 16 ? ALFormat.Stereo16 :
                (ALFormat)0; // unknown

            AL.BufferData(myBuffer1, sound_format1, sound_data1, sound_data1.Length, sample_rate1);
            if (AL.GetError() != ALError.NoError)
            {
                // respond to load error etc.
            }
            // Create a sounds source using the audio clip
           int mySource1 = AL.GenSource(); // gen a Source Handle
            AL.Source(mySource1, ALSourcei.Buffer, myBuffer1); // attach the buffer to a source
            AL.Source(mySource1, ALSourceb.Looping, true); // source loops infinitely
            Vector3 emitterPosition1 = new Vector3(600.0f, 500.0f, 500.0f);
            AL.Source(mySource1, ALSource3f.Position, ref emitterPosition1);
            AL.SourcePlay(mySource1);

            int myBuffer11 = AL.GenBuffer();
            int channels11, bits_per_sample11, sample_rate11;
            byte[] sound_data11 = LoadWave(
                File.Open("Audio/power_item_sound.wav", FileMode.Open),
                out channels11,
                out bits_per_sample11,
                out sample_rate11);
            ALFormat sound_format11=
                channels11 == 1 && bits_per_sample11 == 8 ? ALFormat.Mono8 :
                channels11 == 1 && bits_per_sample11 == 16 ? ALFormat.Mono16 :
                channels11 == 2 && bits_per_sample11 == 8 ? ALFormat.Stereo8 :
                channels11 == 2 && bits_per_sample11 == 16 ? ALFormat.Stereo16 :
                (ALFormat)0; // unknown

            AL.BufferData(myBuffer11, sound_format11, sound_data11, sound_data11.Length, sample_rate11);
            if (AL.GetError() != ALError.NoError)
            {
                // respond to load error etc.
            }
            // Create a sounds source using the audio clip
            int mySource11 = AL.GenSource(); // gen a Source Handle
            AL.Source(mySource11, ALSourcei.Buffer, myBuffer11); // attach the buffer to a source
            AL.Source(mySource11, ALSourceb.Looping, true); // source loops infinitely
            Vector3 emitterPosition11 = new Vector3(500.0f, 0f, 100.0f);
            AL.Source(mySource11, ALSource3f.Position, ref emitterPosition11);
            AL.SourcePlay(mySource11);



            return new ASound(myBuffer, mySource, sound_data, emitterPosition);
        }

        /// <summary>
        /// Load a WAV file.
        /// </summary>
        public static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
               
                // update OpenAL
                AL.Listener(ALListener3f.Position, ref listenerPosition);
                AL.Listener(ALListenerfv.Orientation, ref listenerDirection, ref listenerUp);
            }

        }
        public static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1:
                    return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2:
                    return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default:
                    throw new NotSupportedException("The specified sound format is not supported.");
            }

        }

    }
}

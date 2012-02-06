using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;

namespace Caven.Common.AudioProxies
{
    /// <summary>
    /// Bass Proxy for Bass.Net API
    /// </summary>
    public class BassProxy
    {
        /// <summary>
        /// Default sample rate used at initialization
        /// </summary>
        private const int DefaultSampleRate = 44100;

        /// <summary>
        /// Shows whether the proxy is already disposed
        /// </summary>
        private bool _alreadyDisposed;

        /// <summary>
        /// Reads the mono from file.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <param name="samplerate">Output sample rate.</param>
        /// <param name="milliseconds">Milliseconds to read.</param>
        /// <param name="startmillisecond">Start millisecond.</param>
        /// <returns>Array of samples</returns>
        /// <remarks>
        /// Seeking capabilities of Bass were not used because of the possible
        /// timing errors on different formats.
        /// </remarks>
        public float[] ReadMonoFromFile(string filename, int samplerate, int milliseconds, int startmillisecond)
        {
            int totalMilliseconds = milliseconds <= 0 ? Int32.MaxValue : milliseconds + startmillisecond;
            float[] data = null;

            // Decode the stream
            int sourceStream = Bass.BASS_StreamCreateFile(filename, 0, 0,
                                                    BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_MONO |
                                                    BASSFlag.BASS_SAMPLE_FLOAT);
            // Creating a stream failed.
            if (sourceStream == 0)
            {
                throw new Exception(Bass.BASS_ErrorGetCode().ToString());
            }

            // Create final Mixer Stream
            int mixerStream = BassMix.BASS_Mixer_StreamCreate(samplerate, 1,
                                                              BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_MONO |
                                                              BASSFlag.BASS_SAMPLE_FLOAT);
            if (mixerStream == 0)
            {
                throw new Exception(Bass.BASS_ErrorGetCode().ToString());
            }

            if (BassMix.BASS_Mixer_StreamAddChannel(mixerStream, sourceStream, BASSFlag.BASS_MIXER_FILTER))
            {
                int bufferSize = samplerate * 10 * 4; // Read 10 seconds each time.

                var buffer = new float[bufferSize];
                var chunks = new List<float[]>();

                int size = 0;
                while ((float)size / samplerate * 1000 < totalMilliseconds)
                {
                    int bytesRead = Bass.BASS_ChannelGetData(mixerStream, buffer, bufferSize);

                    if (bytesRead == 0)
                    {
                        break;
                    }

                    var chunk = new float[bytesRead / 4]; // Each float contains 4 bytes
                    Array.Copy(buffer, chunk, bytesRead / 4);
                    chunks.Add(chunk);
                    size += bytesRead / 4;
                }

                // Check if there are enough samples to return the data.
                if ((float)size / samplerate * 1000 < (milliseconds + startmillisecond))
                {
                    return null;
                }

                var start = (int)((float)startmillisecond * samplerate / 1000);

                int end = (milliseconds <= 0)
                              ? size
                              : (int)((float)(startmillisecond + milliseconds) * samplerate / 1000);
                data = new float[size];
                int index = 0;

                // Concat the pieces of the chunks.
                foreach (float[] chunk in chunks)
                {
                    Array.Copy(chunk, 0, data, index, chunk.Length);
                    index += chunk.Length;
                }

                // Select specific part of the song.
                if (start != 0 || end != size)
                {
                    var temp = new float[end - start];
                    Array.Copy(data, start, temp, 0, end - start);
                    data = temp;
                }
            }
            else
            {
                throw new Exception(Bass.BASS_ErrorGetCode().ToString());
            }

            return data;
        }
    }
}

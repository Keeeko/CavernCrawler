using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace CavernCrawler
{
    class AudioManager
    {
        GlobalResource globalResource;
        List<string> audioTrackPaths;
        Dictionary<int, Music> musicTracks;
        Music currentTrack;
        int currentTrackNumber;
        bool repeat;
        bool shuffle;

        public AudioManager(GlobalResource theGlobalResource)
        {
            globalResource = theGlobalResource;
            repeat = true;

           // musicTracks[0] = new Music(@"Content\Audio\Music\0.mp3");
            currentTrackNumber = 0;
        }

        public void Update()
        {

        }

        public void PlayTrack()
        {
            if(musicTracks.ContainsKey(currentTrackNumber))
            {
                currentTrack = musicTracks[currentTrackNumber];
                currentTrack.Play();
                
                if(repeat)
                {
                    currentTrack.Loop = true;
                }
                else
                {
                    currentTrack.Loop = false;
                }
            }
        }

        public void SetTrack(int trackNumber)
        {
            currentTrackNumber = trackNumber;
        }

        public void SetTrackVolume(float volume)
        {
            currentTrack.Volume = volume;
        }
    }
}

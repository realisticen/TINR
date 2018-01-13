using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.GameCode.Util
{
    static class SoundEngine
    {
        private static Dictionary<SoundEffectType, SoundEffect> soundEffects= new Dictionary<SoundEffectType, SoundEffect>();
        private static Dictionary<SongType, Song> songs= new Dictionary<SongType, Song>();
        public static float SoundEffectVolume = 1.0f;

        public static void LoadSounds(ContentManager content)
        {
            soundEffects.Add(SoundEffectType.COIN_DROP, content.Load<SoundEffect>("coin/coin_drop"));
            songs.Add(SongType.MENU, content.Load<Song>("menu/menumusic"));
            songs.Add(SongType.GAME, content.Load<Song>("background/backgroundmusic"));

            MediaPlayer.IsRepeating = true;
        }

        public static void PlaySong(SongType song)
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(songs[song]);
        }

        public static void PlaySoundEffect(SoundEffectType effect)
        {
            soundEffects[effect].Play(SoundEffectVolume, 0, 0);
        }
    }

    enum SongType
    {
        MENU,
        GAME
    }

    enum SoundEffectType
    {
        COIN_DROP
    }
}

using ReLogic.Utilities;
using Terraria.Audio;

namespace YogsothothsYardMod.Core.Handler
{
    public static class GlobalsHandlers
    {
        /// <summary>
        /// <para>原版的音效方法，但是引入了模组的ModSFXVolume选项以求全局降低mod物品音量</para>
        /// <para>操作手感上接近于原本的SoundEngine</para>
        /// <paramref name="variantType"/>为如果当前音效id有多种音效时的差分，在不输入的情况下不会进行任何操作
        /// </summary>
        public static SlotId ScarletSound(SoundStyle soundType, Vector2 pos, float volume = 1f, int instances = 1, float pitch = 0, float pitchVariance = 0f, int? variantType = null)
        {
            SoundStyle slot;
            if (!variantType.HasValue)
                slot = soundType with { MaxInstances = instances, Volume = volume, Pitch = pitch, PitchVariance = pitchVariance };
            else
                slot = soundType with { MaxInstances = instances, Volume = volume, Pitch = pitch, PitchVariance = pitchVariance, Variants = [variantType.Value] };
            SlotId sound = SoundEngine.PlaySound(slot, pos);
            return sound;
        }
    }
}

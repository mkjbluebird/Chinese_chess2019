using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class CharacterSprite
    {
        public Sprite Sprite { get; set; }
        public CharacterData Data { get; set; }

        public CharacterSprite(CharacterData data, Sprite sprite)
        {
            Data = data;
            Sprite = sprite;            
        }
    }
}

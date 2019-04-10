using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{

    public class Font
    {
        Texture _texture;
        public Dictionary<char, CharacterData> _characterData;

        public Font(Texture texture, Dictionary<char, CharacterData> characterData)
        {
            _texture = texture;
            _characterData = characterData;
        }

        public Vector MeasureFont(string text)
        {
            return MeasureFont(text, -1);
        }

        public Vector MeasureFont(string text, double maxWidth)
        {
            Vector dimensions = new Vector();

            foreach (char c in text)
            {
                CharacterData data = _characterData[c];
                dimensions.X += data.XAdvance;
                dimensions.Y = Math.Max(dimensions.Y, data.Height + data.YOffset);
            }
            return dimensions;
        }

        public CharacterSprite CreateSprite(char c)
        {
            CharacterData charData = _characterData[c];
            Sprite sprite = new Sprite();
            Sprite sprite2 = new Sprite();
            
            int tempId = _texture.Id;
            int tempWidth = _texture.Width;
            int tempHeight = _texture.Height;

            // Setup UVs
            if (charData.page == 0)
            {
                SetFont(charData, sprite, _texture);                
            }
            else if (charData.page == 1)
            {
                _texture.Id = Convert.ToInt32(_texture.Id2);
                _texture.Width = Convert.ToInt32(_texture.Width2);
                _texture.Height = Convert.ToInt32(_texture.Height2);
                SetFont(charData, sprite, _texture);                
                _texture.Id = tempId;
                _texture.Width = tempWidth;
                _texture.Height = tempHeight;
            }
            return new CharacterSprite(charData, sprite);
        }

        private void SetFont(CharacterData charData, Sprite sprite, Texture texture)
        {
            sprite.Texture = _texture;
            Point topLeft = new Point((float)charData.X / (float)_texture.Width,
                                    (float)charData.Y / (float)_texture.Height);
            Point bottomRight = new Point(topLeft.X + ((float)charData.Width / (float)_texture.Width),
                                        topLeft.Y + ((float)charData.Height / (float)_texture.Height));
            sprite.SetUVs(topLeft, bottomRight);
            sprite.SetWidth(charData.Width);
            sprite.SetHeight(charData.Height);
            sprite.SetColor(new Color(1, 1, 1, 1));
        }

    }


}

using UnityEngine;

namespace PearlSoft.Scripts.Runtime.ModularUI.Elements
{
    public class ElementUtility : MonoBehaviour
    {

        private const float CARBON_FONT_SIZE_WIDTH_PIXEL_RATIO = 0.55f;

        public static int GetMaxRowsInField(int textSize, float fieldHeight)
        {
            return (int) (fieldHeight / textSize);
        }

        public static int GetCharactersPerLine(int textSize, float fieldWidth)
        {
            return Mathf.FloorToInt(fieldWidth / (textSize * CARBON_FONT_SIZE_WIDTH_PIXEL_RATIO));
        }

    }
}
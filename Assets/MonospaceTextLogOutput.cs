using System;
using System.Collections.Generic;
using PearlSoft.Scripts.Runtime.Common;
using PearlSoft.Scripts.Runtime.ModularUI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace PearlSoft.Scripts.Runtime.ScreenUI.OutputElements
{
    public class MonospaceTextLogOutput : PearlBehaviour
    {

        // RPB: A text/console log manager for monospace fonts and Unity UI text boxes

        #region Public Static Fields

        public static Dictionary<MonospaceTextLogOutput, List<string>> Instances = new Dictionary<MonospaceTextLogOutput, List<string>>();

        #endregion

        #region Event Functions

        #region Protected Methods

        protected override void OnDestroy()
        {
            Instances[this] = null;
            base.OnDestroy();
        }

        #endregion

        #endregion

        #region Private Fields

        // RPB: only works with monospace

        [SerializeField]
        private int m_totalLines = 5;

        [SerializeField]
        private int m_lineLength = 8;

        [SerializeField]
        private Text m_text = default;

        [SerializeField]
        private List<string> m_identifiers;

        [SerializeField]
        private bool m_useTimestamps;

        [SerializeField]
        private bool m_autoResizeOnStart = true;

        private Queue<string> m_lines = new Queue<string>();

        #endregion

        #region Public Methods

        public void Initialize()
        {
            m_lines = new Queue<string>(m_totalLines);

            if (m_autoResizeOnStart)
            {
                Resize();

            }

            Instances[this] = m_identifiers;
            SetInitialized();
            UpdateTextDisplay();
        }

        public void LogText(string text)
        {
            LogTextInternal(text);
        }

        public void SetIdentifiers(List<string> identifiers)
        {
            m_identifiers = identifiers;
            Instances[this] = m_identifiers;
        }

        public void Resize()
        {
            var textSize = m_text.fontSize;

            m_totalLines = ElementUtility.GetMaxRowsInField(textSize, m_text.rectTransform.rect.height);
            m_lineLength = ElementUtility.GetCharactersPerLine(textSize, m_text.rectTransform.rect.width);
        }

        #endregion

        #region Private Methods

        private void UpdateTextDisplay()
        {
            var constructedString = string.Empty;
            var linesArray = m_lines.ToArray();

            for (var i = 0; i < linesArray.Length; i++)
            {
                constructedString += linesArray[i] + "\n";
            }

            m_text.text = constructedString;
        }

        private void LogTextInternal(string text)
        {
            if (m_useTimestamps)
            {
                text = DateTime.Now.ToShortTimeString() + ": " + text;
            }

            // RPB: take the text and split it into lines
            var numberOfLines = Mathf.CeilToInt(text.Length / (float) m_lineLength);

            while (m_lines.Count >= m_totalLines)
            {
                m_lines.Dequeue();
            }

            for (var i = 0; i < numberOfLines; i++)
            {
                var splitText = string.Empty;

                if ((i + 1) * m_lineLength > text.Length)
                {
                    splitText = text.Substring(i * m_lineLength);
                }
                else
                {
                    splitText = text.Substring(i * m_lineLength, m_lineLength);
                }

                while (m_lines.Count >= m_totalLines)
                {
                    m_lines.Dequeue();
                }

                m_lines.Enqueue(splitText);
            }

            UpdateTextDisplay();
        }

        #endregion

    }
}
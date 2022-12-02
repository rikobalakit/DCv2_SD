using System;
using System.Collections;
using PearlSoft.Scripts.Runtime.Common;
using PearlSoft.Scripts.Runtime.ScreenUI.OutputElements;
using UnityEngine;

namespace PearlSoft.Scripts.Runtime.ScreenUI.InputElements
{
    public class MonospaceTextLogStringInput : PearlBehaviour
    {

        #region Serialized Fields

        #region Private Fields

        [SerializeField]
        private string m_targetIdentifier = LogOutputs.LOG; // RPB: defaulting to the status log since that's when this will probably be used.

        #endregion

        #endregion

        #region Event Functions

        #region Private Methods

        private IEnumerator Start()
        {
            yield return null;

            SetInitialized();
        }

        #endregion

        #endregion

        #region Public Static Methods

        public static void ReceiveSystemLogText(string message)
        {
            ReceiveLogText(message, LogOutputs.LOG);
        }
        
        public static void ReceiveLogText(string message, string targetIdentifier)
        {
            foreach (var textLogInstance in MonospaceTextLogOutput.Instances)
            {
                if (textLogInstance.Value.Contains(targetIdentifier))
                {
                    textLogInstance.Key.LogText(message);
                }
            }
        }

        #endregion

        #region Public Methods

        public void SetTarget(string targetIndentifier)
        {
            m_targetIdentifier = targetIndentifier;
        }

        public void ReceiveLogText(string message)
        {
            foreach (var textLogInstance in MonospaceTextLogOutput.Instances)
            {
                if (textLogInstance.Value.Contains(m_targetIdentifier))
                {
                    textLogInstance.Key.LogText(message);
                }
            }
        }

        #endregion

    }
}
//---------------------------------------------------------------------------
// <copyright file="RibbonToolTipService.cs" company="Microsoft Corporation">
//     Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//---------------------------------------------------------------------------

namespace Microsoft.Windows.Controls.Ribbon
{
    #region Using declarations

    using System;
    using System.Windows.Input;

    #endregion

    /// <summary>
    ///   Implements Ribbon's special ToolTip service.
    /// </summary>
    internal class RibbonToolTipService
    {
        #region Constructors

        internal RibbonToolTipService()
        {
            InputManager.Current.PostProcessInput += new ProcessInputEventHandler(OnPostProcessInput);
        }

        #endregion Constructors

        #region Internal Methods

        private void OnPostProcessInput(object sender, ProcessInputEventArgs e)
        {
            if (e.StagingItem.Input.RoutedEvent == Keyboard.KeyDownEvent)
            {
                KeyEventArgs keyArgs = (KeyEventArgs)e.StagingItem.Input;
                ProcessKeyDown(sender, keyArgs);
            }
        }

        private void ProcessKeyDown(object sender, KeyEventArgs e)
        {
            // Dismiss ToolTips on KeyDown

            if (CurrentToolTip != null)
            {
                CurrentToolTip.IsOpen = false;
            }
        }

        #endregion Internal Methods

        #region Properties

        internal static RibbonToolTipService Current
        {
            get 
            { 
                if (_current == null)
                {
                    _current = new RibbonToolTipService();
                }

                return _current; 
            }
        }

        internal RibbonToolTip CurrentToolTip
        {
            get { return _currentToolTip; }
            set { _currentToolTip = value; }
        }

        #endregion Properties

        #region Data

        private RibbonToolTip _currentToolTip;

        [ThreadStatic]
        private static RibbonToolTipService _current;

        #endregion Data
    }
}

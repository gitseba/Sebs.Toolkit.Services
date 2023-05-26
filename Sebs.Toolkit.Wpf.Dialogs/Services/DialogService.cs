using Microsoft.WindowsAPICodePack.Dialogs;
using System;

namespace Sebs.Wpf.Services.Dialogs
{
    /// <summary>
    /// Purpose: 
    /// Created by: Sebastian
    /// Created at: 8/18/2022 2:47:38 PM
    /// </summary>
    public class DialogService : IDialogService
    {
        public CommonOpenFileDialog? Dialog { get; private set; }

        public string OpenDialog()
        {
            using (Dialog = new CommonOpenFileDialog())
            {
                Dialog.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                Dialog.IsFolderPicker = true;

                if (Dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    return Dialog.FileName;
                }

                return Dialog.InitialDirectory;
            }
        }
    }
}
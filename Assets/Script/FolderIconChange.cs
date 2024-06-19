using System.IO;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public static class FolderIconChange
{
    #region Variable

    [DllImport("shell32.dll", CharSet = CharSet.Auto)]
    private static extern void SHChangeNotify(long wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

    private const long SHCNE_ASSOCCHANGED = 0x08000000L;
    private const uint SHCNF_IDLIST = 0x0000;

    public static string _ErrorMessage;

    #endregion

    public static void Change(string folderPath, string iconPath)
    {
        try
        {
            if (!Directory.Exists(folderPath))
            {
                _ErrorMessage = $" folder path {folderPath} is not Exite ?";
                return;
            }

            if (!File.Exists(iconPath))
            {
                _ErrorMessage = $" icon Path  {folderPath} is not Exite ?";
                return;
            }

            string desktopIniPath = Path.Combine(folderPath, "desktop.ini");

            // Remove read-only attribute from folder if it's set
            FileAttributes folderAttributes = File.GetAttributes(folderPath);
            if ((folderAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                folderAttributes &= ~FileAttributes.ReadOnly;
                File.SetAttributes(folderPath, folderAttributes);
            }

            // Remove existing desktop.ini if it exists to ensure we can write a new one
            if (File.Exists(desktopIniPath))
            {
                File.SetAttributes(desktopIniPath, FileAttributes.Normal);
                File.Delete(desktopIniPath);
            }

            // Write the desktop.ini file
            using (StreamWriter writer = new StreamWriter(desktopIniPath))
            {
                writer.WriteLine("[.ShellClassInfo]");
                writer.WriteLine($"IconResource={iconPath},0");
            }

            // Set the desktop.ini file attributes to hidden and system
            File.SetAttributes(desktopIniPath, FileAttributes.Hidden | FileAttributes.System);

            // Set the folder attributes to read-only and system
            File.SetAttributes(folderPath, FileAttributes.ReadOnly | FileAttributes.System);

            // Notify the system that the icon has changed
            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);

        }
        
        catch (UnauthorizedAccessException ex) { }
        
        catch (Exception ex) { }
        
    }


    public static void SingleFolderChangeIcon(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            try
            {
                Change(folderPath, ReRenamning.GetIconeName(folderPath));
            }
            catch (System.Exception ex) { }
        }
        else
        {
            _ErrorMessage = $" folder Path  {folderPath} is not Exite ";
        }
    }


    public static void MultipalFoldersIconChange(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            string[] directories = Directory.GetDirectories(folderPath);

            foreach (string directory in directories)
            {
                Change(directory, ReRenamning.GetIconeName(directory));
            }
        }
        else
        {
            _ErrorMessage = $" Directory Path  {folderPath} is not Exite ";
        }
    }
    
}
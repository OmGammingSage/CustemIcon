using UnityEngine;
using System;
using System.IO;

public static class ReRenamning
{
    // Get icon name
    public static string GetIconeName(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            return null;
        }

        string iconname = Directory.GetFiles(folderPath, "*.ico", SearchOption.AllDirectories)[0];
        string directory = Path.GetDirectoryName(iconname);
        return iconname;
    }

    public static void Rename(string folderPath, string IcoName)
    {
        if (folderPath != null)
        {
            try
            {
                string[] iconFiles = Directory.GetFiles(folderPath, "*.ico", SearchOption.AllDirectories);

                if (iconFiles.Length == 0)
                {
                    return;
                }

                string iconFile = iconFiles[0];
                string directory = Path.GetDirectoryName(iconFile);
                string extension = Path.GetExtension(iconFile);
                string newFileName = $"{IcoName}{extension}";
                string newFilePath = Path.Combine(directory, newFileName);

                // Rename the file
                if (!File.Exists(newFilePath))
                {
                    File.Move(iconFile, newFilePath);
                }
            }

            catch (UnauthorizedAccessException ex)
            {
            }

            catch (Exception ex)
            {
            }
        }
        else
        {
            FolderIconChange._ErrorMessage = $" folder Path Must be Selected ";
        }
    }

    public static void RenameIconToDeffrentNameToChildFolders(string directoryPath, NameType type)
    {
        if (directoryPath != null)
        {
            IconName nameType = new IconName();
            nameType.ChangeType(type);

            string[] directories = Directory.GetDirectories(directoryPath);

            foreach (string directory in directories)
            {
                string IconPath = Directory.GetFiles(directory, "*.ico")[0];

                if (IconPath != null)
                {
                    var extension = Path.GetExtension(IconPath);
                    string newdirectory = Path.GetDirectoryName(IconPath);
                    var newFileName = nameType.GetName() + extension;
                    var newFilePath = Path.Combine(newdirectory, newFileName);

                    try
                    {
                        if (!File.Exists(newFilePath))
                        {
                            File.Move(IconPath, newFilePath);
                        }
                    }

                    catch (Exception ex)
                    {
                    }
                }
            }
        }
        else
        {
            FolderIconChange._ErrorMessage = $" folder Path Must be Selected ";
        }
    }

    public static void RenameIconToSameNameToChildFolders(string directoryPath, string IcoName)
    {
        if (directoryPath != null)
        {
            try
            {
                string[] directories = Directory.GetDirectories(directoryPath);

                foreach (string directory in directories)
                {
                    Rename(directory, IcoName);
                }
            }
            catch (System.Exception ex)
            {
            }
        }
        else
        {
            FolderIconChange._ErrorMessage = $" folder Path Must be Selected ";
        }
    }

    public static void RenameIcons(string directoryPath, NameType type)
    {
        if (directoryPath != null)
        {
            IconName nameType = new IconName();
            nameType.ChangeType(type);

            var files = Directory.GetFiles(directoryPath, "*.ico");
            foreach (var file in files)
            {
                var extension = Path.GetExtension(file);
                var newFileName = nameType.GetName() + extension;
                var newFilePath = Path.Combine(directoryPath, newFileName);

                try
                {
                    if (!File.Exists(newFilePath))
                    {
                        File.Move(file, newFilePath);
                    }
                }

                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            FolderIconChange._ErrorMessage = $" folder Path Must be Selected ";
        }
    }

    public static void RenameFolders(string directoryPath, NameType type)
    {
        if (directoryPath != null)
        {
            IconName nameType = new IconName();
            nameType.ChangeType(type);

            var directories = Directory.GetDirectories(directoryPath);

            foreach (var dir in directories)
            {
                string newFolderName;
                string newFolderPath;

                newFolderName = nameType.GetName();
                newFolderPath = Path.Combine(directoryPath, newFolderName);

                try
                {
                    if (!Directory.Exists(newFolderPath))
                    {
                        Directory.Move(dir, newFolderPath);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            FolderIconChange._ErrorMessage = $" folder Path Must be Selected ";
        }
    }
}

public enum NameType
{
    CapitalAlphabet,
    SmallAlphabet,
    Number
}

// Change Icon name 0 2 3 4...n || A-Z || a-z
public class IconName
{
    private NameType type;
    private int _Increment = 0;

    public void ChangeType(NameType newType)
    {
        type = newType;
        if (type == NameType.Number)
        {
            _Increment = 1;
        }
        else
        {
            _Increment = 0;
        }
    }

    public string GetName()
    {
        string name = "";

        if (type == NameType.Number)
        {
            name = _Increment.ToString();
        }
        else
        {
            name = GenerateAlphabetName(_Increment, type == NameType.CapitalAlphabet);
        }

        _Increment++;
        return name;
    }

    private string GenerateAlphabetName(int number, bool isCapital)
    {
        string name = "";
        char offset = isCapital ? 'A' : 'a';

        do
        {
            name = (char)(offset + (number % 26)) + name;
            number = (number / 26) - 1;
        } while (number >= 0);

        return name;
    }
}
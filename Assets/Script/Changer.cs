using UnityEngine;

public  class Changer : NextPrevie
{
    internal string folderPath;

    internal  string _IconName = "Icon";

    internal  NameType _Type;

    
    public void SingleFolderIconChangeUseToPath()
    {
        FolderIconChange.Change(folderPath,_IconName);
    }
    
    
    public  void RenameAllIconSameFile()
    {
        ReRenamning.RenameIcons(folderPath, _Type);
    }
    
    
    public void RenameIconToDeffrentNameToChildFolders()
    {
        ReRenamning.RenameIconToDeffrentNameToChildFolders(folderPath, _Type);
    }

    public void RenameAllIconToSameName()
    {
        ReRenamning. RenameIconToSameNameToChildFolders(folderPath,_IconName);
    }
    

    public  void SingleIconChange()
    {
        FolderIconChange.SingleFolderChangeIcon(folderPath);
    }
    
    
    public  void MultipalFoldersIconChange()
    {
        FolderIconChange.MultipalFoldersIconChange(folderPath);
    }
    

    public  void RenameAllFolder()
    {
        ReRenamning.RenameFolders(folderPath,_Type);
    }
}
using System;
using UnityEngine;
using SimpleFileBrowser;
using TMPro;
using Unity.VisualScripting;

public class FileBrowserScript : Changer
{
    [SerializeField] private TextMeshProUGUI _FolderPath;

    [SerializeField] private TextMeshProUGUI _IconPath;
    [SerializeField] private TextMeshProUGUI _ErrorMessage;

    [SerializeField] private TMP_Dropdown _NameType;

    [SerializeField] private TMP_InputField _IconNameInputField;
    


    private string _Path;

    private string FolderPath = "Folder Path";
    private string IconName = "Icon Path";

    private bool IsIconBrowse;
    private bool IsDropDown;
    private bool IsBrowseFormIcon;


    private void Start()
    {
        RemoveText();
    }


    public void OpenFileBrowser(bool BrowseFormIcon)
    {
        IsBrowseFormIcon = BrowseFormIcon;
        if (IsBrowseFormIcon)
        {
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Icon Files", ".ico", ".png", ".jpg", ".jpeg"));
            FileBrowser.ShowLoadDialog(OnSuccess, OnCancel, FileBrowser.PickMode.Files, false, null, "Select Icon File",
                "Select");
        }
        else
        {
            FileBrowser.ShowLoadDialog(OnSuccess, OnCancel, FileBrowser.PickMode.Folders, false, null, "Select Folder",
                "Select");
        }
    }


    private void OnSuccess(string[] paths)
    {
        _Path = string.Join("/", paths);

        if (IsBrowseFormIcon)
        {
            _IconPath.text = _Path;
        }
        else
        {
            _FolderPath.text = _Path;
        }
    }


    private void OnCancel()
    {
    }


    public void RemoveText()
    {
        _FolderPath.text = FolderPath;
        _IconPath.text = IconName;
    }


    public void ChangeValueDropdown()
    {
        _Type = (NameType)_NameType.value;
    }


    public override void ChangeImage(int index)
    {
        base.ChangeImage(index);
        if (_CurrentIndex >= 3)
        {
            if (_CurrentIndex ==3)
            {
                _IconNameInputField.gameObject.SetActive(true);
            }
            else
            {
                _IconNameInputField.gameObject.SetActive(false);
            }
            _IconPath.transform.parent.gameObject.SetActive(false);
            _NameType.gameObject.SetActive(true);
            IsIconBrowse = false;
            IsDropDown = true;
        }
        else
        {
            _IconNameInputField.gameObject.SetActive(false);
            IsDropDown = false;
            if (_CurrentIndex == 1)
            {
                IsIconBrowse = true;
                _IconPath.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                IsIconBrowse = false;
                _IconPath.transform.parent.gameObject.SetActive(false);
            }

            _NameType.gameObject.SetActive(false);
        }
    }


    public void ApplicationQuite()
    {
#if UNITY_STANDALONE
        // Quit the application
        Application.Quit();
#endif

        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Change()
    {
        ReMoveError();
        switch (_CurrentIndex)
        {
            case 0:
            {
                folderPath = _FolderPath.text;
                SingleIconChange();
                break;
            }
            case 1:
            {
                folderPath = _FolderPath.text;
                _IconName = _IconPath.text;
                SingleFolderIconChangeUseToPath();
                break;
            }
            case 2:
            {
                folderPath = _FolderPath.text;
                MultipalFoldersIconChange();
                break;
            }
            case 3:
            {
                
                folderPath = _FolderPath.text;
                if (_IconNameInputField.text == null)
                {
                    _Type = (NameType)_NameType.value;
                    RenameIconToDeffrentNameToChildFolders();
                }
                else
                {
                    _IconName = _IconNameInputField.text;
                    RenameAllIconToSameName();
                }
                break;
            }
            case 4:
            {
                folderPath = _FolderPath.text;
                _Type = (NameType)_NameType.value;
                RenameAllIconSameFile();
                break;
            }
            case 5:
            {
                folderPath = _FolderPath.text;
                _Type = (NameType)_NameType.value;
                RenameAllFolder();
                break;
            }
        }
        _ErrorMessage.text = FolderIconChange._ErrorMessage;
    }
    
    public void ReMoveError()
    {
        _ErrorMessage.text = "";
    }
}
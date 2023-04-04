using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PDI_Feather_Tracking_WPF.ViewModel;

public class MenuItem : ViewModelBase
{
    private readonly Type _contentType;
    private readonly object? _dataContext;
    private readonly ModuleEnum? _moduleEnum;


    public MenuItem(string name, Type contentType, UserControl content, ModuleEnum? moduleEnum = null)
    {
        if (moduleEnum != null)
        {
            Messenger.Default.Register<User?>(this, update_module_access);
            _moduleEnum = moduleEnum;
            update_module_access(null);
        }
        Name = name;
        _contentType = contentType;
        _content = content;
    }

    private void update_module_access(User? obj)
    {
        IsVisible = General.CheckAccessibility(obj, _moduleEnum);
        Messenger.Default.Send(General.RefreshUserAccess);
    }

    #region Property

    public string Name { get; }

    private UserControl? _content;

    public UserControl Content => _content;

    private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;

    public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
    {
        get => _horizontalScrollBarVisibilityRequirement;
        set => _horizontalScrollBarVisibilityRequirement = value;
    }

    private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;

    public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
    {
        get => _verticalScrollBarVisibilityRequirement;
        set => _verticalScrollBarVisibilityRequirement = value;
    }

    private Thickness _marginRequirement = new(16);

    public Thickness MarginRequirement
    {
        get => _marginRequirement;
        set => _marginRequirement = value;
    }

    private bool isVisible = true;

    public bool IsVisible
    {
        get { return isVisible; }
        private set { isVisible = value; RaisePropertyChanged(nameof(IsVisible)); }
    }

    #endregion
}

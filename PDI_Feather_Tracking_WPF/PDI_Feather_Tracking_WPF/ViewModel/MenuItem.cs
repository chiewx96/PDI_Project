using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PDI_Feather_Tracking_WPF.ViewModel;

public class MenuItem : ViewModelBase
{
    private readonly Type _contentType;
    private readonly object? _dataContext;

    private UserControl? _content;
    private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
    private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
    private Thickness _marginRequirement = new(16);

    public MenuItem(string name, Type contentType, UserControl content)
    {
        Name = name;
        _contentType = contentType;
        _content = content;
    }

    public string Name { get; }
    public UserControl Content => _content;

    public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement
    {
        get => _horizontalScrollBarVisibilityRequirement;
        set => _horizontalScrollBarVisibilityRequirement = value;
    }

    public ScrollBarVisibility VerticalScrollBarVisibilityRequirement
    {
        get => _verticalScrollBarVisibilityRequirement;
        set => _verticalScrollBarVisibilityRequirement = value;
    }

    public Thickness MarginRequirement
    {
        get => _marginRequirement;
        set => _marginRequirement = value;
    }

}

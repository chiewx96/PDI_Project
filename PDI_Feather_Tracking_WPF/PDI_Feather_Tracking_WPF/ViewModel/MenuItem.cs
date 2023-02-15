using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PDI_Feather_Tracking_WPF.ViewModel;

public class MenuItem : ViewModelBase
{
    private readonly Type _contentType;
    private readonly object? _dataContext;

    private object? _content;
    private ScrollBarVisibility _horizontalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
    private ScrollBarVisibility _verticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto;
    private Thickness _marginRequirement = new(16);

    public MenuItem(string name, Type contentType, object? dataContext = null)
    {
        Name = name;
        _contentType = contentType;
        _dataContext = dataContext;
    }

    public string Name { get; }


    public object? Content => _content ??= CreateContent();

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

    private object? CreateContent()
    {
        var content = Activator.CreateInstance(_contentType);
        if (_dataContext != null && content is FrameworkElement element)
        {
            element.DataContext = _dataContext;
        }

        return content;
    }
}

using System;
using System.Windows;
using System.Windows.Data;

namespace ES.Tools.UI
{
  /// <summary>
  /// Watches a dependency property of a <see cref="DependencyObject"/> and rises an event.
  /// </summary>
  /// <typeparam name="T">The type of the dependency property</typeparam>
  public class DependencyPropertyWatcher<T> : DependencyObject, IDisposable
  {
    /// <summary>
    /// Called when the dependency property changes.
    /// </summary>
    public event EventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="DependencyPropertyWatcher{T}"/> class.
    /// </summary>
    /// <param name="target">Target Dependency Object</param>
    /// <param name="propertyPath">Path of Property</param>
    public DependencyPropertyWatcher(DependencyObject target, string propertyPath)
    {
      Target = target;
      var binding = new Binding { Source = target, Path = new PropertyPath(propertyPath), Mode = BindingMode.OneWay };
      BindingOperations.SetBinding(this, ValueProperty, binding);
    }

    /// <summary>
    /// Value property.
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(object), typeof(DependencyPropertyWatcher<T>), new PropertyMetadata(null, OnPropertyChanged));

    /// <summary>
    /// Gets the target Dependency Object
    /// </summary>
    public DependencyObject Target { get; }

    /// <summary>
    /// Gets the current Value
    /// </summary>
    public T Value => (T)GetValue(ValueProperty);

    /// <summary>
    /// Called when the Property is updated
    /// </summary>
    public static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
      var source = (DependencyPropertyWatcher<T>)sender;
      source.PropertyChanged?.Invoke(source, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Called when object is being disposed.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        BindingOperations.ClearBinding(this, ValueProperty);
        ClearValue(ValueProperty);
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace ES.Tools.UI
{
  /// <summary>
  /// The shared resource dictionary is a specialized resource dictionary
  /// that loads it content only once. If a second instance with the same source
  /// is created, it only merges the resources from the cache.
  /// </summary>
  public class SharedResourceDictionary : ResourceDictionary
  {
    // Internal cache of dictionaries
    private static readonly Dictionary<Uri, ResourceDictionary> _sharedDictionaries = new();

    // A value indicating whether the application is in designer mode.
    private static readonly Lazy<bool> _isInDesignerModeLazy = new(()=> (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue);

    // Local member of the source uri
    private Uri _sourceUri;

    /// <summary>
    /// Gets or sets the uniform resource identifier (URI) to load resources from.
    /// </summary>
    public new Uri Source
    {
      get => _isInDesignerModeLazy.Value ? _sourceUri : base.Source;
      set
      {
        // Always load the dictionary by default in designer mode.
        if (_isInDesignerModeLazy.Value)
        {
          base.Source = value;
        }
        else
        {
          _sourceUri = value;

          // Try to add dictionary to concurrent dictionary.
          // If this fails, it was probably already added before.
          if (!_sharedDictionaries.ContainsKey(value))
          {
            // If the dictionary is not yet loaded, add it to the dictionary...
            _sharedDictionaries[value] = this;
            // then load it by setting the source of the base class
            base.Source = value;
          }
          else
          {
            // If the dictionary is already loaded, get it from the cache
            MergedDictionaries.Add(_sharedDictionaries[value]);
          }
        }
      }
    }
  }
}

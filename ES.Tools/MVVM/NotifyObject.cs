using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ES.Tools.MVVM
{
  public abstract class NotifyObject : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
    {
      if (Equals(member, val))
      {
        return;
      }

      member = val;
      OnPropertyChanged(propertyName);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged<T>(Expression<Func<T>> expression)
    {
      if (expression == null)
      {
        throw new ArgumentNullException(nameof(expression));
      }

      var memberExpression = (MemberExpression)expression.Body;
      OnPropertyChanged(memberExpression.Member.Name);
    }
  }
}

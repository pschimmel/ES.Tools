# PasswordBoxBehavior
Namespace: **ES.Tools.Behaviors**

The **PasswordBoxBehavior** is an extension for WPF *PasswordBox*es.

### Properties

**SelectAllTextOnFocus**

Set the *SelectAllTextOnFocus* attached dependency property to *true* to select the password text when the *PasswordBox* gets focused.

### Usage

``` XML
<TextBox behaviors:PasswordBoxBehavior.SelectAllTextOnFocus="true" Password="Example" />
```

### Remarks

> See also [**TextBoxBehavior**](TextBoxBehavior.md).
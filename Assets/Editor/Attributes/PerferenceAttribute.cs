using System;

[System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class PerferenceSettingMethodAttribute : System.Attribute
{
    public PerferenceSettingMethodAttribute(string reloadMethodName)
    {
        refreshMethod = reloadMethodName;
    }


    public string refreshMethod;
}
using NinjaTrader.Gui;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;

[TypeConverter(typeof(ExpandableObjectConverter))]
public class DotExpandableParameters
{
    public override string ToString()
    {
        return "";
    }

    [XmlIgnore]
    [Display(Name = "Is dot auto scalable", Order = 0, GroupName = "Dot parameters")]
    public bool IsDotAutoScale
    { get; set; }

    [Browsable(false)]
    public string IsDotAutoScaleSerializable
    {
        get { return IsDotAutoScale.ToString(); }
        set { IsDotAutoScale = Convert.ToBoolean(value); }
    }

    [XmlIgnore]
    [Display(Name = "Up dot color", Order = 1, GroupName = "Dot parameters")]
    public Brush UpDotColor
    { get; set; }

    [Browsable(false)]
    public string UpDotColorSerializable
    {
        get { return Serialize.BrushToString(UpDotColor); }
        set { UpDotColor = Serialize.StringToBrush(value); }
    }

    [XmlIgnore]
    [Display(Name = "Down dot color", Order = 2, GroupName = "Dot parameters")]
    public Brush DowDotColor
    { get; set; }

    [Browsable(false)]
    public string DowDotColorSerializable
    {
        get { return Serialize.BrushToString(DowDotColor); }
        set { DowDotColor = Serialize.StringToBrush(value); }
    }

    [XmlIgnore]
    [Display(Name = "Up outline dot color", Order = 3, GroupName = "Dot parameters")]
    public Brush UpDotOutlineColor
    { get; set; }

    [Browsable(false)]
    public string UpDotOutlineColorSerializable
    {
        get { return Serialize.BrushToString(UpDotColor); }
        set { UpDotOutlineColor = Serialize.StringToBrush(value); }
    }

    [XmlIgnore]
    [Display(Name = "Down outline dot color", Order = 4, GroupName = "Dot parameters")]
    public Brush DownDotOutlineColor
    { get; set; }

    [Browsable(false)]
    public string DownDotOutlineColorSerializable
    {
        get { return Serialize.BrushToString(DownDotOutlineColor); }
        set { DownDotOutlineColor = Serialize.StringToBrush(value); }
    }
}

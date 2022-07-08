using System;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;

namespace EquipBestItem.Widgets;

public class CoefficientsSliderWidget: SliderWidget
{
    private float _valueFloatReleased;
    
    [Editor(false)]
    public float ValueFloatReleased
    {
        get => this._valueFloatReleased;
        set
        {
            if (Math.Abs(this._valueFloatReleased - value) < 1E-05f) return;
            
            this._valueFloatReleased = value;
            base.OnPropertyChanged(this._valueFloatReleased, "ValueFloatReleased");
        }
    }
    
    public CoefficientsSliderWidget(UIContext context) : base(context)
    {
    }

    protected override void OnMouseReleased()
    {
        base.OnMouseReleased();
        ValueFloatReleased = ValueFloat;
    }
}
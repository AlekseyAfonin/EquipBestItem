using TaleWorlds.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Widgets;

public class EquipButtonWidget : ButtonWidget
{
    private bool _lastState;

    public EquipButtonWidget(UIContext context) : base(context)
    {
    }

    // Show the buttons on the slots, even if there are no better items. This will open the filter settings
    protected override void OnLateUpdate(float dt)
    {
        base.OnLateUpdate(dt);

        if (Input.IsKeyPressed(InputKey.LeftAlt))
        {
            _lastState = IsDisabled;
            IsDisabled = false;
        }

        if (Input.IsKeyReleased(InputKey.LeftAlt)) IsDisabled = _lastState;
    }

    // Block the main function of the button, but leave the option to open the settings via right mouse button
    protected override void OnMousePressed()
    {
        if (Input.IsKeyDown(InputKey.LeftAlt)) return;

        base.OnMousePressed();
    }

    protected override void OnMouseReleased()
    {
        if (Input.IsKeyDown(InputKey.LeftAlt)) return;

        base.OnMouseReleased();
    }
}
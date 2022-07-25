using TaleWorlds.GauntletUI;
using TaleWorlds.InputSystem;

namespace EquipBestItem.Widgets;

public class EquipButtonWidget : ButtonWidget
{
    private bool _lastState;

    public EquipButtonWidget(UIContext context) : base(context)
    {
    }

    // Показываем кнопки на слотах, даже если нет предметов лучше. Это позволит открыть настройки фильтров
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

    // Блокируем основную функцию кнопки, но оставляем возможность открыть настройки через ПКМ
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
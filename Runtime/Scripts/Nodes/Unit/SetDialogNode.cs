using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: Set Dialog Node")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("Set Dialog Node")]
    [UnitCategory("Reflectis\\Flow")]
    public class SetDialogNode : Unit
    {
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlInput InputTrigger { get; private set; }
        [DoNotSerialize]
        [PortLabelHidden]
        public ControlOutput OutputTrigger { get; private set; }

        public ValueInput Talkable { get; private set; }
        [PortLabel("Dialog Part")]
        public ValueInput NewDialogPart { get; private set; }

        protected override void Definition()
        {
            Talkable = ValueInput<Talkable>(nameof(Talkable));
            NewDialogPart = ValueInput<DialogPart>(nameof(NewDialogPart), null);

            InputTrigger = ControlInput(nameof(InputTrigger), (f) =>
            {
                Talkable targetTalkable = f.GetValue<Talkable>(Talkable);
                DialogPart newDialogPart = f.GetValue<DialogPart>(NewDialogPart);
                targetTalkable.SetDialog(newDialogPart);

                return OutputTrigger;
            });

            OutputTrigger = ControlOutput(nameof(OutputTrigger));

            Succession(InputTrigger, OutputTrigger);
        }
    }
}

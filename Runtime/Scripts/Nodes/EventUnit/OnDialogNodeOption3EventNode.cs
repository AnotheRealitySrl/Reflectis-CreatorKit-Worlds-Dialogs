using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: On Dialog Node Option 3")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("On Dialog Node Option 3")]
    [UnitCategory("Events\\Reflectis")]
    public class OnDialogNodeOption3EventNode : EventUnit<Null>
    {
        public static string eventName = "OnDialogNodeOption3";

        protected override bool register => true;

        protected GraphReference graphReference;

        protected DialogPart dialogPartReference;

        [DoNotSerialize]
        [PortLabel("Dialog Part")]
        public ValueInput DialogPartReference { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            DialogPartReference = ValueInput<DialogSystem>(nameof(DialogPartReference));
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);

            using (var flow = Flow.New(instance))
            {
                dialogPartReference = flow.GetValue<DialogPart>(DialogPartReference);
            }
            dialogPartReference.Node.onOption3.AddListener(OnDialogNodeOption3);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogPartReference.Node.onOption3.RemoveListener(OnDialogNodeOption3);
        }

        public void OnDialogNodeOption3()
        {
            Trigger(graphReference, null);
        }
    }
}

using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: On Dialog Node Option 2")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("On Dialog Node Option 2")]
    [UnitCategory("Events\\Reflectis")]
    public class OnDialogNodeOption2EventNode : EventUnit<Null>
    {
        public static string eventName = "OnDialogNodeOption2";

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
            dialogPartReference.Node.onOption2.AddListener(OnDialogNodeOption2);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogPartReference.Node.onOption2.RemoveListener(OnDialogNodeOption2);
        }

        public void OnDialogNodeOption2()
        {
            Trigger(graphReference, null);
        }
    }
}

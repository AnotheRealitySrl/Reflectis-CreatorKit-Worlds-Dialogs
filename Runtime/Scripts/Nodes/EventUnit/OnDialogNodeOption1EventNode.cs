using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: On Dialog Node Option 1")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("On Dialog Node Option 1")]
    [UnitCategory("Events\\Reflectis")]
    public class OnDialogNodeOption1EventNode : EventUnit<Null>
    {
        public static string eventName = "OnDialogNodeOption1";

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
            dialogPartReference.Node.onOption1.AddListener(OnDialogNodeOption1);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogPartReference.Node.onOption1.RemoveListener(OnDialogNodeOption1);
        }

        public void OnDialogNodeOption1()
        {
            Trigger(graphReference, null);
        }
    }
}

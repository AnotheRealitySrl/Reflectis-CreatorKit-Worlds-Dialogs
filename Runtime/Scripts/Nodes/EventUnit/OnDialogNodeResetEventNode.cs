using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: On Dialog Node Reset")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("On Dialog Node Reset")]
    [UnitCategory("Events\\Reflectis")]
    public class OnDialogNodeResetEventNode : EventUnit<Null>
    {
        public static string eventName = "OnDialogNodeReset";

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
            dialogPartReference.OnDialogReset.AddListener(OnDialogNodeReset);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogPartReference.OnDialogReset.RemoveListener(OnDialogNodeReset);
        }

        private void OnDialogNodeReset()
        {
            Trigger(graphReference, null);
        }
    }
}

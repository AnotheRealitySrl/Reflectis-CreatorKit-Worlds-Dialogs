using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: Dialog System Ready")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("Dialog System Ready")]
    [UnitCategory("Events\\Reflectis")]
    public class DialogSystemReadyEventNode : EventUnit<DialogSystem>
    {
        public static string eventName = "DialogSystemReady";

        protected override bool register => true;

        protected GraphReference graphReference;

        protected DialogSystem dialogSystemReference;

        [DoNotSerialize]
        [PortLabel("Dialog System")]
        public ValueInput DialogSystemReference { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            DialogSystemReference = ValueInput<DialogSystem>(nameof(DialogSystemReference));
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);

            using (var flow = Flow.New(instance))
            {
                dialogSystemReference = flow.GetValue<DialogSystem>(DialogSystemReference);
            }
            dialogSystemReference.dialogSystemReady.AddListener(OnDialogSystemReady);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogSystemReference.dialogSystemReady.RemoveListener(OnDialogSystemReady);
        }

        private void OnDialogSystemReady()
        {
            Trigger(graphReference, dialogSystemReference);
        }
    }
}

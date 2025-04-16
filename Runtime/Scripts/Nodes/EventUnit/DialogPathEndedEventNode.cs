using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: Dialog Path Ended")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("Dialog Path Ended")]
    [UnitCategory("Events\\Reflectis")]
    public class DialogPathEndedEventNode : EventUnit<Null>
    {
        public static string eventName = "DialogPathEnded";

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
            dialogSystemReference.dialogPathEnded.AddListener(OnDialogPathEnded);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogSystemReference.dialogPathEnded.RemoveListener(OnDialogPathEnded);
        }

        private void OnDialogPathEnded()
        {
            Trigger(graphReference, null);
        }
    }
}

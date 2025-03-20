using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: Dialog Changed")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("Dialog Changed")]
    [UnitCategory("Events\\Reflectis")]
    public class DialogChangedEventNode : EventUnit<DialogNode>
    {
        public static string eventName = "DialogChanged";

        protected override bool register => true;

        protected GraphReference graphReference;

        protected DialogSystem dialogSystemReference;

        [DoNotSerialize]
        [PortLabel("Dialog System")]
        public ValueInput DialogSystemReference { get; private set; }

        [DoNotSerialize]
        [PortLabel("Dialog Node")]
        public ValueOutput ChangedDialogNode { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            DialogSystemReference = ValueInput<DialogSystem>(nameof(DialogSystemReference));
            ChangedDialogNode = ValueOutput<DialogNode>(nameof(ChangedDialogNode));
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);

            using (var flow = Flow.New(instance))
            {
                dialogSystemReference = flow.GetValue<DialogSystem>(DialogSystemReference);
            }
            dialogSystemReference.dialogChanged.AddListener(OnDialogChanged);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        protected override void AssignArguments(Flow flow, DialogNode args)
        {
            flow.SetValue(ChangedDialogNode, args);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogSystemReference.dialogChanged.RemoveListener(OnDialogChanged);
        }

        private void OnDialogChanged(DialogNode node)
        {
            Trigger(graphReference, node);
        }
    }
}

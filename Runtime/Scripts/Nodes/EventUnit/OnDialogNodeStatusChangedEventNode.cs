using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: Dialog Node Status Changed")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("Dialog Node Status Changed")]
    [UnitCategory("Events\\Reflectis")]
    public class OnDialogNodeStatusChangedEventNode : EventUnit<(DialogNode.DialogStatus, DialogNode.DialogStatus)>
    {
        public static string eventName = "OnDialogNodeStatusChanged";

        protected override bool register => true;

        protected GraphReference graphReference;

        protected DialogPart dialogPartReference;

        [DoNotSerialize]
        [PortLabel("Dialog Part")]
        public ValueInput DialogPartReference { get; private set; }

        [DoNotSerialize]
        [PortLabel("Old Dialog Status")]
        public ValueOutput OldDialogStatus { get; private set; }

        [DoNotSerialize]
        [PortLabel("New Dialog Status")]
        public ValueOutput NewDialogStatus { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            DialogPartReference = ValueInput<DialogSystem>(nameof(DialogPartReference));
            OldDialogStatus = ValueOutput<DialogNode.DialogStatus>(nameof(OldDialogStatus));
            NewDialogStatus = ValueOutput<DialogNode.DialogStatus>(nameof(NewDialogStatus));
        }

        public override void Instantiate(GraphReference instance)
        {
            base.Instantiate(instance);

            using (var flow = Flow.New(instance))
            {
                dialogPartReference = flow.GetValue<DialogPart>(DialogPartReference);
            }
            dialogPartReference.Node.onStatusChanged.AddListener(OnDialogNodeStatusChanged);
        }

        public override EventHook GetHook(GraphReference reference)
        {
            graphReference = reference;

            return new EventHook(eventName);
        }

        protected override void AssignArguments(Flow flow, (DialogNode.DialogStatus, DialogNode.DialogStatus) args)
        {
            flow.SetValue(OldDialogStatus, args.Item1);
            flow.SetValue(NewDialogStatus, args.Item2);
        }

        public override void Uninstantiate(GraphReference instance)
        {
            base.Uninstantiate(instance);

            dialogPartReference.Node.onStatusChanged.RemoveListener(OnDialogNodeStatusChanged);
        }

        private void OnDialogNodeStatusChanged(DialogNode.DialogStatus status)
        { 
            Trigger(graphReference, (status, dialogPartReference.Node.Status));
        }
    }
}

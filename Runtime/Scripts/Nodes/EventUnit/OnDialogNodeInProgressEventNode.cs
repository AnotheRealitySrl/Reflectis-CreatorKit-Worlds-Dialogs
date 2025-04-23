using Reflectis.SDK.Core.VisualScripting;
using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: On Dialog Node In Progress")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("On Dialog Node In Progress")]
    [UnitCategory("Events\\Reflectis")]
    public class OnDialogNodeInProgressEventNode : UnityEventUnit<Null>
    {
        public static string eventName = "OnDialogNodeInProgress";

        protected override bool register => true;

        [DoNotSerialize]
        [PortLabel("Dialog Part")]
        public ValueInput DialogPartReference { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            DialogPartReference = ValueInput<DialogSystem>(nameof(DialogPartReference));
        }


        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(eventName);
        }

        protected override UnityEvent GetEvent(GraphReference reference)
        {
            return Flow.New(reference).GetValue<DialogPart>(DialogPartReference).OnDialogInProgress;
        }

        protected override Null GetArguments(GraphReference reference)
        {
            return null;
        }
    }
}

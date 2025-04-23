using Reflectis.SDK.Core.VisualScripting;
using Reflectis.SDK.Dialogs;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace Reflectis.CreatorKit.Worlds.Dialogs
{
    [UnitTitle("Reflectis Dialogs: Dialog Canceled")]
    [UnitSurtitle("Dialogs")]
    [UnitShortTitle("Dialog Canceled")]
    [UnitCategory("Events\\Reflectis")]
    public class DialogCanceledEventNode : UnityEventUnit<Null>
    {
        public static string eventName = "DialogCanceled";

        protected override bool register => true;

        [DoNotSerialize]
        [PortLabel("Dialog System")]
        public ValueInput DialogSystemReference { get; private set; }

        protected override void Definition()
        {
            base.Definition();
            DialogSystemReference = ValueInput<DialogSystem>(nameof(DialogSystemReference));
        }

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(eventName);
        }

        protected override UnityEvent GetEvent(GraphReference reference)
        {
            return Flow.New(reference).GetValue<DialogSystem>(DialogSystemReference).dialogCanceled;
        }

        protected override Null GetArguments(GraphReference reference)
        {
            return null;
        }
    }
}

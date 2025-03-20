using Unity.VisualScripting;

namespace Reflectis.CreatorKit.Worlds.Dialogs.Editor
{
    [Descriptor(typeof(DialogSystemReadyEventNode))]
    public class DialogSystemReadyEventDescriptor : UnitDescriptor<DialogSystemReadyEventNode>
    {
        public DialogSystemReadyEventDescriptor(DialogSystemReadyEventNode unit) : base(unit) { }

        protected override string DefinedSummary()
        {
            return "This event will be triggered right after the referenced Dialog System " +
                "is done with its initial setup.";
        }

        protected override void DefinedPort(IUnitPort port, UnitPortDescription description)
        {
            base.DefinedPort(port, description);
        }
    }
}

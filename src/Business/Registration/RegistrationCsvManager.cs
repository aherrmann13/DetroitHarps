namespace DetroitHarps.Business.Registration
{
    using System.Linq;
    using DetroitHarps.Business.Registration.Models;
    using Tools;
    using Tools.Csv;

    public class RegistrationCsvManager : IRegistrationCsvManager
    {
        private readonly IRegistrationManager _manager;
        private readonly IEventAccessor _eventAccessor;
        private readonly ICsvWriter _csvWriter;

        public RegistrationCsvManager(
            IRegistrationManager manager,
            IEventAccessor eventAccessor,
            ICsvWriter csvWriter)
        {
            Guard.NotNull(manager, nameof(manager));
            Guard.NotNull(eventAccessor, nameof(eventAccessor));
            Guard.NotNull(csvWriter, nameof(csvWriter));

            _manager = manager;
            _eventAccessor = eventAccessor;
            _csvWriter = csvWriter;
        }

        public byte[] GetAllRegisteredParents() =>
            _csvWriter.GetAsCsv(_manager.GetAllRegisteredParents().ToList());

        public byte[] GetAllRegisteredChildren()
        {
            var pivot = new ListPivot<RegisteredChildModel, RegisteredChildEventModel>(
                nameof(RegisteredChildModel.Events),
                x => x.Events,
                x => _eventAccessor.GetName(x.EventId),
                x => x.Answer.ToString());

            return _csvWriter.GetAsCsv(
                _manager.GetAllRegisteredChildren().ToList(),
                pivot);
        }
    }
}
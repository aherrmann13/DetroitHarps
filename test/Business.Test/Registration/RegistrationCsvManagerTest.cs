namespace DetroitHarps.Business.Test.Registration
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.DataTypes;
    using DetroitHarps.Business.Registration.Models;
    using Moq;
    using Tools.Csv;
    using Xunit;

    public class RegistrationCsvManagerTest
    {
        private readonly Mock<IRegistrationManager> _managerMock;
        private readonly Mock<IEventAccessor> _eventAccessorMock;
        private readonly Mock<ICsvWriter> _csvWriterMock;
        private readonly RegistrationCsvManager _manager;

        public RegistrationCsvManagerTest()
        {
            _managerMock = new Mock<IRegistrationManager>();
            _eventAccessorMock = new Mock<IEventAccessor>();
            _csvWriterMock = new Mock<ICsvWriter>();
            _manager = new RegistrationCsvManager(
                _managerMock.Object,
                _eventAccessorMock.Object,
                _csvWriterMock.Object);
        }

        [Fact]
        public void NullRegistrationManagerInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RegistrationCsvManager(
                    null,
                    _eventAccessorMock.Object,
                    _csvWriterMock.Object));
        }

        [Fact]
        public void NullEventAccessorInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RegistrationCsvManager(
                    _managerMock.Object,
                    null,
                    _csvWriterMock.Object));
        }

        [Fact]
        public void NullCsvWriterInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RegistrationCsvManager(
                    _managerMock.Object,
                    _eventAccessorMock.Object,
                    null));
        }

        [Fact]
        public void GetAllRegistrationParentsInstrumentsCorrectObjectsTest()
        {
            var list = new List<RegisteredParentModel>
            {
                new RegisteredParentModel()
            };

            var byteArray = Guid.NewGuid().ToByteArray();
            _managerMock.Setup(x => x.GetAllRegisteredParents())
                .Returns(list);

            _csvWriterMock.Setup(x =>
                x.GetAsCsv(It.IsAny<IList<RegisteredParentModel>>()))
                .Returns(byteArray);

            var result = _manager.GetAllRegisteredParents();

            Assert.Equal(byteArray, result);
            _managerMock.Verify(x => x.GetAllRegisteredParents(), Times.Once);
            _csvWriterMock.Verify(
                x => x.GetAsCsv(
                    It.Is<IList<RegisteredParentModel>>(y => y.Count == 1)),
                    Times.Once);
        }

        [Fact]
        public void GetAllRegistrationChildrenInstrumentsCorrectObjectsTest()
        {
            var list = new List<RegisteredChildModel>
            {
                new RegisteredChildModel()
            };
            var byteArray = Guid.NewGuid().ToByteArray();
            _managerMock.Setup(x => x.GetAllRegisteredChildren())
                .Returns(list);

            _csvWriterMock.Setup(x =>
                x.GetAsCsv<RegisteredChildModel, RegisteredChildEventModel>(
                    It.IsAny<IList<RegisteredChildModel>>(),
                    It.IsAny<ListPivot<RegisteredChildModel, RegisteredChildEventModel>>()))
                .Returns(byteArray);

            var result = _manager.GetAllRegisteredChildren();

            Assert.Equal(byteArray, result);
            _managerMock.Verify(x => x.GetAllRegisteredChildren(), Times.Once);
            _csvWriterMock.Verify(
                x => x.GetAsCsv(
                    It.Is<IList<RegisteredChildModel>>(y => y.Count == 1),
                    It.IsAny<ListPivot<RegisteredChildModel, RegisteredChildEventModel>>()),
                    Times.Once);
        }

        [Fact]
        public void GetAllRegistrationChildrenListPivotAccessesEventsTest()
        {
            ListPivot<RegisteredChildModel, RegisteredChildEventModel> listPivot = null;
            _csvWriterMock.Setup(x =>
                x.GetAsCsv<RegisteredChildModel, RegisteredChildEventModel>(
                    It.IsAny<IList<RegisteredChildModel>>(),
                    It.IsAny<ListPivot<RegisteredChildModel, RegisteredChildEventModel>>()))
                .Callback<ICollection<RegisteredChildModel>, ListPivot<RegisteredChildModel, RegisteredChildEventModel>>(
                    (x, y) => listPivot = y);

            var registeredChildModel = new RegisteredChildModel
            {
                Events = new List<RegisteredChildEventModel>()
            };
            _manager.GetAllRegisteredChildren();

            Assert.Equal(
                registeredChildModel.Events,
                listPivot.ListAccess(registeredChildModel));
        }

        [Fact]
        public void GetAllRegistrationChildrenMapsEventTitleToColumnNameTest()
        {
            ListPivot<RegisteredChildModel, RegisteredChildEventModel> listPivot = null;
            _csvWriterMock.Setup(x =>
                x.GetAsCsv<RegisteredChildModel, RegisteredChildEventModel>(
                    It.IsAny<IList<RegisteredChildModel>>(),
                    It.IsAny<ListPivot<RegisteredChildModel, RegisteredChildEventModel>>()))
                .Callback<ICollection<RegisteredChildModel>, ListPivot<RegisteredChildModel, RegisteredChildEventModel>>(
                    (x, y) => listPivot = y);

            var testName = Guid.NewGuid().ToString();
            _eventAccessorMock.Setup(x => x.GetName(It.IsAny<int>()))
                .Returns(testName);

            var registeredChildModel = new RegisteredChildModel
            {
                Events = new List<RegisteredChildEventModel>
                {
                    new RegisteredChildEventModel
                    {
                        EventId = 1,
                        Answer = Answer.Yes
                    }
                }
            };
            _manager.GetAllRegisteredChildren();

            var name = listPivot.ColumnNameAccess(registeredChildModel.Events[0]);

            Assert.Equal(testName, name);
            _eventAccessorMock.Verify(
                x => x.GetName(
                    It.Is<int>(y => y == registeredChildModel.Events[0].EventId)),
                    Times.Once);
        }

        [Fact]
        public void GetAllRegistrationChildrenMapsEventAnswerToColumnValueTest()
        {
            ListPivot<RegisteredChildModel, RegisteredChildEventModel> listPivot = null;
            _csvWriterMock.Setup(x =>
                x.GetAsCsv<RegisteredChildModel, RegisteredChildEventModel>(
                    It.IsAny<IList<RegisteredChildModel>>(),
                    It.IsAny<ListPivot<RegisteredChildModel, RegisteredChildEventModel>>()))
                .Callback<ICollection<RegisteredChildModel>, ListPivot<RegisteredChildModel, RegisteredChildEventModel>>(
                    (x, y) => listPivot = y);

            var registeredChildModel = new RegisteredChildModel
            {
                Events = new List<RegisteredChildEventModel>
                {
                    new RegisteredChildEventModel
                    {
                        EventId = 1,
                        Answer = Answer.Yes
                    }
                }
            };
            _manager.GetAllRegisteredChildren();

            var answer = listPivot.ColumnValueAccess(registeredChildModel.Events[0]);

            Assert.Equal(registeredChildModel.Events[0].Answer.ToString(), answer);
        }
    }
}
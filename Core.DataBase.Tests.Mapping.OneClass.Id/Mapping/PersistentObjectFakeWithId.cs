using Core.DataBase.Objects;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.Tests.Mapping.OneClass.Id.Mapping
{
    [Class(Table = nameof(PersistentObjectFakeWithId))]
    public class PersistentObjectFakeWithId : PersistentObjectWithId
    {
        [Id(Column = nameof(Id), TypeType = typeof(string), Name = nameof(Id))]
        public override string Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        protected PersistentObjectFakeWithId()
        {
        }

        public PersistentObjectFakeWithId(string id)
        {
            _id = id;
        }
    }
}
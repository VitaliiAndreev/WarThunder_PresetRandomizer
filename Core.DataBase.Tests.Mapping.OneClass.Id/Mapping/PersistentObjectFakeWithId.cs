using Core.DataBase.Enumerations;
using Core.DataBase.Objects;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.Tests.Mapping.OneClass.Id.Mapping
{
    [Class(Table = nameof(PersistentObjectFakeWithId))]
    public class PersistentObjectFakeWithId : PersistentObjectWithId
    {
        [Id(Column = nameof(Id), TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        protected PersistentObjectFakeWithId()
        {
        }

        public PersistentObjectFakeWithId(long id)
        {
            Id = id;
        }
    }
}
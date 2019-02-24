using Core.DataBase.Objects;
using NHibernate.Mapping.Attributes;
using System;

namespace Core.DataBase.Tests.Mapping.OneClass.Id.Mapping
{
    [Class(Table = nameof(PersistentObjectFakeWithId))]
    public class PersistentObjectFakeWithId : PersistentObjectWithId
    {
        [Id(Column = nameof(Id), TypeType = typeof(Guid), Name = nameof(Id))]
        public override Guid Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        protected PersistentObjectFakeWithId()
        {
        }

        public PersistentObjectFakeWithId(Guid id)
        {
            _id = id;
        }
    }
}
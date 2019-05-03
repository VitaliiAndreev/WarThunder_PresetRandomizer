using Core.DataBase.Tests.Mapping.OneClass.Id.Mapping;
using NHibernate.Mapping.Attributes;
using System;

namespace Core.DataBase.Tests.Mapping.OneClass.IdAndName.Mapping
{
    [Class(Table = nameof(PersistentObjectFakeWithIdAndName))]
    public class PersistentObjectFakeWithIdAndName : PersistentObjectFakeWithId
    {
        [Id(Column = nameof(Id), TypeType = typeof(long), Name = nameof(Id), Generator = "hilo")]
        public override long Id { get; protected set; }

        [Property()]
        public virtual string Name { get; protected set; }

        protected PersistentObjectFakeWithIdAndName()
        {
        }

        public PersistentObjectFakeWithIdAndName(string name)
        {
            Id = -1L;
            Name = name;
        }
    }
}
﻿// Copyright (c) IxMilia.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;

namespace IxMilia.Iges.Entities
{
    public class IgesOffsetSurface : IgesEntity
    {
        public override IgesEntityType EntityType { get { return IgesEntityType.OffsetSurface; } }

        public IgesVector Direction { get; set; }
        public double Distance { get; set; }
        public IgesEntity Surface { get; set; }

        public IgesOffsetSurface()
            : this(IgesVector.ZAxis, 0.0, null)
        {
        }

        public IgesOffsetSurface(IgesVector direction, double distance, IgesEntity surface)
            : base()
        {
            Direction = direction;
            Distance = distance;
            Surface = surface;
        }

        internal override int ReadParameters(List<string> parameters, IgesReaderBinder binder)
        {
            Direction.X = Double(parameters, 0);
            Direction.Y = Double(parameters, 1);
            Direction.Z = Double(parameters, 2);
            Distance = Double(parameters, 3);
            binder.BindEntity(Integer(parameters, 4), e => Surface = e);
            return 5;
        }

        internal override IEnumerable<IgesEntity> GetReferencedEntities()
        {
            yield return Surface;
        }

        internal override void WriteParameters(List<object> parameters, IgesWriterBinder binder)
        {
            parameters.Add(Direction.X);
            parameters.Add(Direction.Y);
            parameters.Add(Direction.Z);
            parameters.Add(Distance);
            parameters.Add(binder.GetEntityId(Surface));
        }
    }
}

﻿using System.Windows.Media.Media3D;
using Metropolis.Api.Core.Domain;
using Metropolis.Domain;

namespace Metropolis.Layout
{
    public class SquaredLayout : AbstractLayout
    {
        public override void ModelCity(Model3DGroup cityScape, CodeBase codeBase)
        {
            Reset(cityScape);
            SetCityLights(cityScape);

            if (codeBase.NumberOfTypes == 0) return;
            RenderSquareBlock(cityScape, codeBase.AllClasses, new Point3D(0, 0, 0), 
                codeBase.Graph.MinLinesOfCode, codeBase.Graph.MaxLinesOfCode);
        }
    }
}
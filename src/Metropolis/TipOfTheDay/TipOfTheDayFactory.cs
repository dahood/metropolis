﻿using System;
using System.Collections.Generic;

namespace Metropolis.TipOfTheDay
{
    public class TipOfTheDayFactory : ITipOfTheDayFactory
    {
        private int current = -1;

        private static readonly List<Func<ITipOfTheDay>> Tips = new List<Func<ITipOfTheDay>>
        {
            () => new WelcomeToolTip(),
            () => new DefaultCaseControlTip(),
            () => new NestedIfThenElseCodeSmellTip(),
            () => new InversionOfControlTip(),
            () => new CSharpTipOfTheDay()
        };


        public ITipOfTheDay Next  => Tips[NextIndex]();

        public int NextIndex 
        {
            get
            {
                return current = current == Tips.Count - 1 ? current = 0 : current += 1;
            }
        }
        
    }
}
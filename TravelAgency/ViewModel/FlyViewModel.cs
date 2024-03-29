﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAgency.Models;

namespace TravelAgency.ViewModel
{
    public class FlyViewModel
    {

        public Fly fly {get; set;}

        public List<Fly> flyList { get; set; }

        public List<Fly> increase { get; set; }

        public List<Fly> decrease { get; set; }

        public List<Fly> rated { get; set; }

    }
}
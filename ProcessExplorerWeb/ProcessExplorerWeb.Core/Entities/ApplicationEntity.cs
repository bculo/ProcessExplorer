﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessExplorerWeb.Core.Entities
{
    public class ApplicationEntity : Entity<Guid>
    {
        //NOTE: ID not autogenerated

        /// <summary>
        /// Application name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Application started
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// Application closed
        /// </summary>
        public DateTime Closed { get; set; }


        /// <summary>
        /// Reference on user session
        /// </summary>
        public Guid SessionId { get; set; }
        public virtual ProcessExplorerUserSession Session { get; set; }
    }
}

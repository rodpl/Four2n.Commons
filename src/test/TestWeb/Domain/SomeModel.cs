// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeModel.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the SomeModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TestWeb.Domain
{
    using System.ComponentModel.DataAnnotations;

    public partial class SomeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }
    }
}
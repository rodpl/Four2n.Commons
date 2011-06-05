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

    using Controllers;

    [MetadataType(typeof(BindingTestController.SomeModelMetadata))]
    public partial class SomeModel
    {
    }
}
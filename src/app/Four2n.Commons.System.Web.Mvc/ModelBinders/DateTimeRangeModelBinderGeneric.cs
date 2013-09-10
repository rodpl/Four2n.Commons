// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeModelBinderGeneric.cs" company="Daniel Dabrowski - rod.42n.pl">
//   Copyright (c) Daniel Dabrowski - rod.42n.pl. All rights reserved.
// </copyright>
// <summary>
//   Defines the DateTimeRangeModelBinderGeneric type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Four2n.Commons.System.Web.Mvc.ModelBinders
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Web.Mvc;

    /// <summary>
    /// Generic class for DataTimeRange classes binding.
    /// </summary>
    /// <typeparam name="T">
    /// Type of DateTimeRange
    /// </typeparam>
    public abstract class DateTimeRangeModelBinder<T> : DefaultModelBinder
    {
        /// <summary>
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <returns>
        /// The bound object.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="bindingContext "/>parameter is null.</exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            bool hasPrefix = bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName);
            string searchPrefix = hasPrefix ? bindingContext.ModelName + "." : string.Empty;

            string beginsString = this.GetValue(bindingContext, searchPrefix, "Begins");
            DateTime? begins = string.IsNullOrEmpty(beginsString) ? (DateTime?)null : DateTime.Parse(beginsString);

            string endsString = this.GetValue(bindingContext, searchPrefix, "Ends");
            DateTime? ends = string.IsNullOrEmpty(endsString) ? (DateTime?)null : DateTime.Parse(endsString);

            return this.Create(begins, ends);
        }

        /// <summary>
        /// Creates DateRange Implementation
        /// </summary>
        /// <param name="begins">Range begins.</param>
        /// <param name="ends">Range ends.</param>
        /// <returns>Instance of DataTimeRange struct</returns>
        protected abstract T Create(DateTime? begins, DateTime? ends);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="key">The binding key.</param>
        /// <returns>String representation</returns>
        private string GetValue(ModelBindingContext bindingContext, string prefix, string key)
        {
            string searchedKey = string.Concat(prefix, key);
            ValueProviderResult vpr = bindingContext.ValueProvider.GetValue(searchedKey);
            if (vpr != null)
            {
                bindingContext.ModelState.SetModelValue(searchedKey, vpr);
            }

            return vpr == null ? null : vpr.AttemptedValue;
        }
    }
}
